(*
======================================
Event Sourced Shopping Cart

Exercise:
* Define the commands and events for the event sourcing system in section 2
* Domain model and state (section 1) are done for you!
* You DONT have to do an implementation!

======================================
*)

// ===============================
// 1. Domain model (DONE)
// ===============================

// The domain is copied over from previous State Machine
//     D1.02c-Exercise-ShoppingCartApi (answers).fsx
// Except: The function types (actions) are removed. They are converted to Commands instead


type CartItem = string     // placeholder for now
type Payment = float     // placeholder for now

// Create types to represent the data stored for each state
type ActiveCartData =
    ActiveCartData of CartItem list

type PaidCartData = {
    Contents: CartItem list
    Payment : Payment
    }

// Create a "state" type that represents the union of all the states
type ShoppingCart =
    | EmptyCartState
    | ActiveCartState of ActiveCartData
    | PaidCartState of PaidCartData

// ====================================
// 2. EXERCISE: Define the events and commands for event sourcing
// ====================================

// These are based on the function types (actions) from previous State Machine
// Note that the state is NOT sent along with the action

/// A desired action
type CartCommand =
    | InitCart of CartItem
    | Add of CartItem
    | Remove of CartItem
    | Pay of Payment


/// An event representing a state change that happened
type CartEvent =
    | Created
    | AddedItem of CartItem
    | RemovedItem of CartItem
    | Paid of Payment

