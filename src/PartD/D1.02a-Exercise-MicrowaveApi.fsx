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
      the timer pause and the oven is in "Door Open and Paused" State
   2. When the timer has finished:
      the timer is reset and the oven is in "Door Closed and Idle" State

Business rules:

* The microwave cannot be running if the door is open
* The microwave cannot be running if the TimeRemaining = 0


*)

type undefined = exn

// ===========================
// 1. Define a type to track the TimeRemaining
// How can you enforce this rule:
//   * The microwave cannot be running if the TimeRemaining = 0
// ===========================

type TimeRemaining = undefined


// ===========================
// 1. Define a type for information stored in each state
//    and a type for the four states combined
// ===========================



type DoorOpenIdleState = ??
type DoorClosedIdleState = ??
type RunningState = ??
// what else
type State =
    // DoorClosedIdle etc


// ===========================
// 2. Define a function type for each action:
//    Start/OpenDoorWhileRunning/CloseDoorWhilePaused etc
// ===========================

type Start = ??
type OpenDoorWhileIdle = ??
// what other actions?


// ===========================
// 3. How does the TimeRemaining change over time?
//    Define a "timer tick" function that is called every second
//    by a timer inside the oven
// ===========================

type TimerTick = ??


// ===========================
// 4. Add a new feature!
//    The user can stop the microwave at any time using the "Stop" button
// ===========================

// Define functions that represent this transition



// ===========================
// 5. Business rules:
//
// Check that these rules are met in your design
// * The microwave cannot be running if the door is open
// * The microwave cannot be running if the TimeRemaining = 0
// ===========================
