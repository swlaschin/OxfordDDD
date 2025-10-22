// ================================================
// Exercise: Implement the API of a Microwave oven
//
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