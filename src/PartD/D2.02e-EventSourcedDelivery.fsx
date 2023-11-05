(*
======================================
Event Sourced Delivery System

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
//    D1.03c-Exercise-DeliveryApi (answers).fsx
// Except: The function types (actions) are removed. They are converted to Commands instead

type Package = string // placeholder for now
type TruckId = int
type DeliveryTimestamp = System.DateTime
type Signature = string

/// Information about a package in an Undelivered state
type UndeliveredData = {
    Package : Package
    }

/// Information about a package in an OutForDelivery state
type OutForDeliveryData = {
    Package : Package
    TruckId : TruckId
    AttemptedAt : DeliveryTimestamp
    }

/// Information about a package in a Delivered state
type DeliveredData = {
    Package : Package
    Signature : Signature
    DeliveredAt : DeliveryTimestamp
    }

/// Information about a package in an FailedDelivery state
type FailedDeliveryData = {
    Package : Package
    FailedAt : DeliveryTimestamp
    }

// Create a "state" type that represents the union of all the states
type ShipmentState =
    | UndeliveredState of UndeliveredData
    | OutForDeliveryState of OutForDeliveryData
    | DeliveredState of DeliveredData
    | FailedDeliveryState of FailedDeliveryData


// ====================================
// 2. EXERCISE: Define the events and commands for event sourcing
// ====================================


// These are based on the function types (actions) from previous State Machine
// Note that the state is NOT sent along with the action


/// A desired action/
type DeliveryCommand =
    // fill these in
    ??

/// An event representing a state change that happened
type DeliveryEvent =
    // fill these in
    ??
