// ================================================
// Example: Implementation of the Microwave API
//
// To use, see comment at bottom of file
// ================================================

(*
This domain is all about using a Microwave oven.

The oven has:

* a keypad for entering times
* a start button
* a button to open the door


The microwave can be in the following states:
    Door Open and Idle
    Door Closed and Idle
    Running
    Door Open and Paused

How it works:
   1. User sets the TimeRemaining and then pushes Start
      Note this can only been done when the oven is in "Door Closed and Idle" state
   2. If the user opens the door before the timer has finished:
      the timer pauses and the oven is in "Door Open and Paused" State
   3. When the timer has finished:
      the timer is reset and the oven is in "Door Closed and Idle" State

Business rules:

* The microwave cannot be running if the door is open
* The microwave cannot be running if the TimeRemaining = 0
* Once the microwave is running, you can't change the time (keep it simple!)

*)

// ===========================
// 1. Define a type to track the TimeRemaining
// How can you enforce this rule:
//   * The microwave cannot be running if the TimeRemaining = 0
// ===========================

type NonZeroInteger = private NonZeroInteger of int
    // Use this to ensure that the TimeRemaining is never 0

type TimeRemaining = private TimeRemaining of NonZeroInteger
    // Note: the constructor is private and can only be changed from inside the oven


// ===========================
// 1. Define a type for information stored in each state
//    and a type for the four states combined
// ===========================



/// A place holder for state associated with the door being open.
type DoorOpenIdleState = DoorOpenIdleState
type DoorClosedIdleState = DoorClosedIdleState
    // Note: even though there is no data, we want to keep these two states separate,
    // so we define a new type for each one.
    // We can do that by using the name of the type.

/// The running state needs to keep track of the time remaining
type RunningState = {
    TimeRemaining : TimeRemaining
    // TimeRemaining cannot be 0 in this state!
    }

/// The paused state also needs to keep track of the time remaining
type DoorOpenPausedState = {
    TimeRemaining : TimeRemaining
    }

type State =
    | DoorClosedIdle of DoorClosedIdleState
    | DoorOpenIdle of DoorOpenIdleState
    | Running of RunningState
    | DoorOpenPaused of DoorOpenPausedState


// ===========================
// 2. Define a function type for each action:
//    Start/OpenDoorWhileRunning/CloseDoorWhilePaused etc
// ===========================

type Start =
    TimeRemaining * DoorClosedIdleState -> State
type OpenDoorWhileIdle =
    DoorClosedIdleState -> State
type OpenDoorWhileRunning =
    RunningState -> State
type CloseDoorWhilePaused =
    DoorOpenPausedState -> State
type CloseDoorWhileIdle =
    DoorOpenIdleState -> State

// alternative design with overly general approach
module AlternativeDesignWithGeneralStateChange =

    type OpenDoor =
        State -> State   // what happens if door is already open
    type CloseDoor =
        State -> State

// alternative design with very explicit states
module AlternativeDesignWithExplicitState =

    type Start =
        TimeRemaining * DoorClosedIdleState -> RunningState
    type OpenDoorWhileIdle =
        DoorClosedIdleState -> DoorOpenPausedState
    type OpenDoorWhileRunning =
        RunningState -> DoorOpenPausedState
    type CloseDoorWhilePaused =
        DoorOpenPausedState -> RunningState
    type CloseDoorWhileIdle =
        DoorOpenIdleState -> DoorClosedIdleState

// ===========================
// 3. How does the TimeRemaining change over time?
//    Define a "timer tick" function that is called every second
//    by a timer inside the oven
// ===========================

type TimerTick =
    State -> State
    // The oven could be in any state
    // Only when "Running" will the TimeRemaining actually change


// ===========================
// 4. Add a new feature!
//    The user can stop the microwave at any time using the "Stop" button
// ===========================

// Define functions that represent this transition

type StopWhileRunning =
    RunningState -> State
type StopWhilePaused =
    DoorOpenPausedState -> State

// NOTE there are two cases. Pressing Stop while Idle does nothing

// alternative design with overly general approach
module AlternativeStopDesignWithGeneralStateChange =

    type Stop =
        State -> State   // what happens if already stopped?

// alternative design with very explicit states
module AlternativeStopDesignWithExplicitState =

    type StopWhileRunning =
        RunningState -> DoorClosedIdleState
    type StopWhilePaused =
        DoorOpenPausedState -> DoorOpenIdleState


// ===========================
// 5. Business rules:
//
// Check that these rules are met in your design
// * The microwave cannot be running if the door is open
// * The microwave cannot be running if the TimeRemaining = 0
// ===========================



//==================================================================
// Implementation
//
// To use, see comment at bottom of file
//
//==================================================================

module NonZeroInteger =

    let create i =
        if i = 0 then
            None
        else
            Some (NonZeroInteger i)

    let value (NonZeroInteger i) = i


module TimeRemaining =

    let create i =
        match (NonZeroInteger.create i) with
        | Some nzi ->
            Some (TimeRemaining nzi)
        | None ->
            None

    let seconds (TimeRemaining (NonZeroInteger seconds)) =
        seconds

    let decrement (TimeRemaining (NonZeroInteger seconds)) =
        create (seconds - 1)

// test interactively
(*
let tr10 = TimeRemaining.create 10 |> Option.get
let tr9 = TimeRemaining.decrement tr10
let tr1 = TimeRemaining.create 1 |> Option.get
let tr0 = TimeRemaining.decrement tr1
*)

module MicrowaveEngine =

    // ------------------------------------
    // implementation of transition types
    // ------------------------------------

    let start : Start =
        fun (timeRemaining, doorState) ->
            Running {TimeRemaining = timeRemaining}

    let openDoorWhileIdle : OpenDoorWhileIdle =
        fun doorState ->
            DoorOpenIdle DoorOpenIdleState

    let openDoorWhileRunning : OpenDoorWhileRunning =
        fun doorState ->
            DoorOpenPaused {TimeRemaining = doorState.TimeRemaining}

    let closeDoorWhilePaused : CloseDoorWhilePaused =
        fun doorState ->
            Running {TimeRemaining = doorState.TimeRemaining}

    let closeDoorWhileIdle : CloseDoorWhileIdle =
        fun doorState ->
            DoorClosedIdle DoorClosedIdleState

    // ------------------------------------
    // Print messages to the UI
    // ------------------------------------
    let printState state =
        match state with
        | DoorClosedIdle doorState ->
            "Door Closed: Idle"
        | DoorOpenIdle doorState ->
            "Door Open: Idle"
        | Running doorState ->
            let seconds = TimeRemaining.seconds doorState.TimeRemaining
            $"Door Closed: Running with {seconds} remaining"
        | DoorOpenPaused doorState ->
            let seconds = TimeRemaining.seconds doorState.TimeRemaining
            $"Door Open: Paused with {seconds} remaining"

        |> printfn "%s"

    // ------------------------------------
    // Handle a keystroke from the UI
    // ------------------------------------
    let handleKeystroke keyStroke state =
        match keyStroke with

        // start with N seconds
        |"1" |"2" |"3" |"4" |"5" |"6" |"7" |"8" |"9" ->
            let timeRemainingMaybe = TimeRemaining.create (int keyStroke)
            match state,timeRemainingMaybe with
            | DoorClosedIdle doorState, Some timeRemaining ->
                start (timeRemaining,doorState)
            | DoorOpenIdle doorState, Some timeRemaining ->
                // this was a action not captured by the design. We should really go back and add it!
                DoorOpenPaused {TimeRemaining = timeRemaining }
            | _,_ ->
                // any other combination, ignore. Leave state alone.
                state

        // open
        |"o" ->
            match state with
            | DoorClosedIdle doorState ->
                openDoorWhileIdle doorState
            | Running doorState ->
                openDoorWhileRunning doorState
            | _ ->
                // any other combination, ignore. Leave state alone.
                state

        // close
        |"c" ->
            match state with
            | DoorOpenIdle doorState ->
                closeDoorWhileIdle doorState
            | DoorOpenPaused doorState ->
                closeDoorWhilePaused doorState
            | _ ->
                // any other combination, ignore. Leave state alone.
                state

        // cancel
        |"x" ->
            match state with
            | Running doorState ->
                DoorClosedIdle DoorClosedIdleState
            | DoorOpenPaused doorState ->
                DoorOpenIdle DoorOpenIdleState
            | _ ->
                // any other combination, ignore. Leave state alone.
                state

        | _ ->
            // any other keystroke, ignore. Leave state alone.
            state

    // ------------------------------------
    // Print menu
    // ------------------------------------
    let printMenu() =
        printfn """
=== Microwave example ===

Keystrokes must be followed by ENTER:
    1,2,3,4,5,6,7,8,9 : start with N seconds on clock
    o : open door
    c : close door
    x : cancel operation
        """

    // ------------------------------------
    // Handle a clock tick event
    // ------------------------------------
    let handleClockTick state =
        match state with
        | Running doorState ->
            let maybeTr = TimeRemaining.decrement doorState.TimeRemaining

            let newState =
                match maybeTr with
                | Some tr ->
                    // still some time left
                    Running {TimeRemaining=tr}
                | None ->
                    // no more time left
                    DoorClosedIdle DoorClosedIdleState
            printState newState
            newState

        | _ ->
            // any other state, ignore clock ticks.
            state

    // ------------------------------------
    // Define a type for data on the message queue, and a handler
    // ------------------------------------
    type Msg =
        | Keystroke of string
        | ClockTick

    type Engine = MailboxProcessor<Msg>

    let handleMsg msg state =
        match msg with
        | Keystroke keystroke ->
            let newState = handleKeystroke keystroke state
            printState newState
            newState
        | ClockTick ->
            handleClockTick state


    let createEngine() = MailboxProcessor.Start(fun inbox ->
        // the message processing function
        let rec messageLoop state = async{

            // read a message
            let! msg = inbox.Receive()

            // process a message
            let newState = handleMsg msg state

            // loop to top
            return! messageLoop newState
            }

        // start the loop
        let initialState = DoorClosedIdle DoorClosedIdleState
        messageLoop initialState
        )


    // ---------------------------------
    // timer
    // ---------------------------------

    let startTimer (engine:Engine) =
        let timer = new System.Timers.Timer()
        timer.Interval <- 1000
        timer.Elapsed.Add (fun args -> engine.Post ClockTick)
        timer.Start()
        timer

    // ---------------------------------
    // keystroke UI
    // ---------------------------------

    let rec keystrokeLoop (engine:Engine) =
        let keystroke = System.Console.ReadLine()
        engine.Post (Keystroke keystroke)
        if keystroke <> "x" then
            keystrokeLoop engine

    // ---------------------------------
    // top level start
    // ---------------------------------
    type MicrowaveData = {
        engine: Engine
        timer: System.Timers.Timer
        }

    let runMicrowave() =
        printMenu()
        let engine = createEngine()
        let timer = startTimer(engine)
        keystrokeLoop(engine)
        {engine=engine; timer=timer}





(*
==================================================================

To use
1. open a console 
2. run this script using
     dotnet fsi MicrowaveApi_Implementation.fsx

When ready, type "9" and ENTER to start
IMPORTANT: Keystrokes must be followed by ENTER key

==================================================================
*)


MicrowaveEngine.runMicrowave()