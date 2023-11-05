(*
======================================
Event Sourced Turtle
======================================

Event sourcing -- Building state from a list of past events

In this design, the client sends a `Command` to a `CommandHandler`.
The CommandHandler converts that to a list of events and stores them in an `EventStore`.

In order to know how to process a Command, the CommandHandler builds the current state
from scratch using the past events associated with that particular turtle.

Neither the client nor the command handler needs to track state.  Only the EventStore is mutable.

====================================== *)

// load mock event store
#load "EventStore.fsx"

// load utility code for turtle graphics
#load "TurtleGraphicsLib.fsx"
open TurtleGraphicsLib

// ===============================
// Turtle domain model
// ===============================

// A synonym for now
type Angle  = float

// Enumeration of available pen states
type PenState = Up | Down

// just a synonym for now
type Distance = float

type Turtle = {
    penState : PenState
    angle : Angle
    position : Position
}

// ====================================
// Turtle events and commands
// ====================================

/// A desired action, sent by the client
type TurtleCommand =
    | Move of Distance
    | Turn of Angle
    | PenUp
    | PenDown

/// An event representing a state change that happened
type TurtleEvent =
    | Moved of Distance * startPos:Position * endPos:Position
    | Turned of angleTurned:Angle * finalAngle:Angle
    | PenStateChanged of PenState

// ======================================
// Logger
// ======================================

module Logger =

    let info msg = printfn "INFO %s" msg
    let warn msg = printfn "WARN %s" msg
    let error msg = printfn "ERROR %s" msg

// ======================================
// Mock canvas to draw on
// ======================================

module Canvas =

    /// Clear the canvas
    let init() =
        () // not needed for a mock one

    /// Draw a line on the canvas
    let draw pos1 pos2 =
        printfn $"[Canvas] ({pos1.x},{pos1.y}) --> ({pos2.x},{pos2.y})"

// ====================================
// Helpers
// ====================================

// global singleton event store
let eventStore = EventStore.EventStore<TurtleEvent>()

// the initial state is used every time when reconstituting the state from the events
let initialTurtleState = {
    position = {x=0.0; y=0.0}
    angle = 0.0
    penState = PenState.Up
}

// ====================================
// Reminder of the three important event sourcing functions
// ====================================

type ApplyEvent<'state,'event> =
     'state -> 'event -> 'state
//       ^current           ^new

type ExecuteCommand<'command,'state,'event> =
    'command -> 'state -> 'event list

type HandleCommand<'command> =
     'command -> unit


// ====================================
// CommandHandler
// ====================================

module CommandHandler =

    /// Apply an event to the current state and return the new state of the turtle
    let applyEvent (state:Turtle) (event:TurtleEvent) :Turtle =
        match event with
        | Moved (distance,startPosition,endPosition) ->
            {state with position = endPosition }
        | Turned (angleTurned,finalAngle) ->
            {state with angle = finalAngle}
        | PenStateChanged penState ->
            {state with penState = penState}

    // Do the command (with side effects)
    // Also determine what events to generate, based on the command and the current state.
    let executeCommand (command:TurtleCommand) (state:Turtle) : TurtleEvent list =

        match command with
        | Move distance ->
            Logger.info (sprintf "Move %0.1f" distance)
            let startPosition = state.position
            // calculate new position
            let endPosition = calcNewPosition distance state.angle startPosition
            // draw line if needed - A SIDE EFFECT
            if state.penState = Down then
                Canvas.draw startPosition endPosition
            let actualDistanceMoved = pythagDistance startPosition endPosition

            //return list of events
            if actualDistanceMoved > 0.0 then
                [ Moved (actualDistanceMoved,startPosition,endPosition) ]
            else
                []

        | Turn angleToTurn ->
            Logger.info (sprintf "Turn %0.1f" angleToTurn)
            // calculate new angle
            let newAngle = (state.angle + angleToTurn) % 360.0
            //return list of events
            [ Turned (angleToTurn,newAngle) ]

        | PenUp ->
            Logger.info "Pen up"
            [ PenStateChanged Up ]

        | PenDown ->
            Logger.info "Pen down"
            [ PenStateChanged Down ]


    /// main function : process a command
    let handleCommand (command:TurtleCommand) :unit =

        /// First load all the events from the event store
        let eventHistory = eventStore.GetEvents()

        /// Then, recreate the state before the command
        let stateBeforeCommand =
            eventHistory |> List.fold applyEvent initialTurtleState

        /// Construct the events arising from the command
        let events = executeCommand command stateBeforeCommand

        // store the events in the event store
        events |> List.iter (eventStore.SaveEvent)

// ====================================
// Test the code
// ====================================

open CommandHandler

let drawTriangle() =
    handleCommand (PenDown)
    handleCommand (Move 100.0)
    //eventStore.PrintEvents()
    handleCommand (Turn 120.0)
    handleCommand (Move 100.0)
    handleCommand (Turn 120.0)
    handleCommand (Move 100.0)
    handleCommand (Turn 120.0)

(*
Canvas.init()
eventStore.Clear()
drawTriangle()
*)

let drawPolygon n =
    let angle = 180.0 - (360.0/float n)
    let angleDegrees = angle * 1.0

    // define a function that draws one side
    let drawOneSide sideNumber =
        handleCommand (Move 100.0)
        handleCommand (Turn angleDegrees)

    // repeat for all sides
    for i in [1..n] do
        drawOneSide i

(*
Canvas.init()
eventStore.Clear()
handleCommand PenDown
drawPolygon 4
drawPolygon 5
*)



