(*
======================================
Event Sourced TicTacToe/Noughts and Crosses
======================================
*)

// load mock event store
#load "EventStore.fsx"


// ===============================
// TicTacToe domain model (TTT for short)
// ===============================

// A synonym for now
type Player =
    | X
    | O

type Position =
    | TopLeft | TopCentre | TopRight
    | MiddleLeft | MiddleCentre | MiddleRight
    | BottomLeft | BottomCentre | BottomRight

let winningLines = [
    // rows
    [TopLeft; TopCentre; TopRight]
    [MiddleLeft; MiddleCentre; MiddleRight]
    [BottomLeft; BottomCentre; BottomRight]
    // columns
    [TopLeft; MiddleLeft; BottomLeft]
    [TopCentre; MiddleCentre; BottomCentre]
    [TopRight; MiddleRight; BottomRight]
    // diagonals
    [TopLeft; MiddleCentre;  BottomRight]
    [BottomLeft; MiddleCentre; TopRight]
]

type Move = {Player:Player; Position: Position}

type MoveResult =
    | KeepPlaying
    | Win
    | Draw
    | InvalidMove

type BoardState = {
    filledCells : Map<Position,Player>
}

// ====================================
// Events and commands
// ====================================

/// A desired action on a turtle
type TTTCommand =
    | Play of Move

/// An event representing a state change that happened
type TTTEvent =
    | Played of Move
    | BadMove of Position
    | WonBy of Player
    | Drawn

// ======================================
// Logger
// ======================================

module Logger =

    let info msg = printfn "INFO %s" msg
    let warn msg = printfn "WARN %s" msg
    let error msg = printfn "ERROR %s" msg

// ======================================
// Implementation of game
// ======================================

module TicTacToeImplementation =

    let threeInARow (state:BoardState) (player:Player) positionList : bool =

        let playersCellCount =
            positionList
            |> List.choose state.filledCells.TryFind // get all the cells for the given positions
            |> List.filter (fun p -> p = player) // count how many for this player
            |> List.length

        playersCellCount = 3

    let calculateMoveResult (state:BoardState) (player:Player) :MoveResult =
        let win =
            winningLines
            |> List.map (threeInARow state player)
            |> List.exists id
        if win then
            Win
        elif state.filledCells.Count = 9 then
            // no more cells
            Draw
        else
            KeepPlaying

    let play (state:BoardState) (move:Move) :MoveResult =
        if state.filledCells.ContainsKey move.Position then
            InvalidMove
        else
            let newCells = state.filledCells.Add (move.Position,move.Player)
            let newState = {state with filledCells = newCells}
            calculateMoveResult newState move.Player

// ====================================
// CommandHandler
// ====================================

// global singleton event store
let eventStore = EventStore.EventStore<TTTEvent>()

let initialState = {
    filledCells = Map.empty
}

module CommandHandler =

    /// Apply an event to the current state and return the new state of the game
    let applyEvent (state:BoardState) (event:TTTEvent) :BoardState =
        match event with
        | Played move ->
            // update the state
            {state with filledCells = state.filledCells.Add (move.Position,move.Player) }
        | BadMove position ->
            // doesnt affect the state
            state
        | WonBy player ->
            // doesnt affect the state
            state
        | Drawn  ->
            // doesnt affect the state
            state

    // Determine what events to generate, based on the command and the state.
    let executeCommand (command:TTTCommand) (state:BoardState) : TTTEvent list =

        // helper for nice logging
        let moveToString move = sprintf "%A->%A" move.Player move.Position

        match command with
        | Play move ->
            let moveStr = moveToString move
            Logger.info (sprintf "Move %A" moveStr)
            // calculate moveResult
            let moveResult = TicTacToeImplementation.play state move
            let listOfEvents =
                match moveResult with
                    | KeepPlaying ->
                        [Played move]
                    | InvalidMove ->
                        Logger.error (sprintf "InvalidMove %A" moveStr)
                        [Played move; BadMove move.Position]
                    | Win ->
                        Logger.info (sprintf "Won by %A" move.Player)
                        [Played move; WonBy move.Player]
                    | Draw ->
                        Logger.info (sprintf "Draw")
                        [Played move; Drawn]

            //return list of events
            listOfEvents


    /// main function : process a command
    let handleCommand (command:TTTCommand) :unit =

        /// First load all the events from the event store
        let eventHistory = eventStore.GetEvents()

        /// Then, recreate the state before the command
        let stateBeforeCommand =
            eventHistory |> List.fold applyEvent initialState

        /// Construct the events arising from the command
        let events = executeCommand command stateBeforeCommand

        // store the events in the event store
        events |> List.iter (eventStore.SaveEvent)

// ====================================
// Test the code
// ====================================

open CommandHandler

(*
// Game 1
eventStore.Clear()
handleCommand (Play {Player=X; Position=TopLeft})
handleCommand (Play {Player=O; Position=TopLeft})
//eventStore.PrintEvents()
handleCommand (Play {Player=X; Position=TopCentre})
handleCommand (Play {Player=X; Position=TopRight})
//eventStore.PrintEvents()
*)


(*
// Game 2
eventStore.Clear()
handleCommand (Play {Player=X; Position=TopLeft})
handleCommand (Play {Player=O; Position=TopRight})
//eventStore.PrintEvents()
handleCommand (Play {Player=X; Position=BottomRight})
handleCommand (Play {Player=O; Position=BottomLeft})
handleCommand (Play {Player=O; Position=MiddleCentre})
//eventStore.PrintEvents()
*)
