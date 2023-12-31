﻿// ================================================
// Exercise: Convert non-total functions to total functions
//
// ================================================

// -----------------------------------
(*
Quiz:

Which of these are total functions
and which are partial functions?

// get first element of a list
firstElement : int list -> int

// get number of elements in a list
elementCount : int list -> int

// convert a str to an int
strToInt : string -> int

// format an int into a string
intToStr : int -> string

// convert a 32-bit int into a 16-bit int
longToInt : int32 -> int16

// convert a float into an int (assuming truncation of decimals)
floatToInt: float -> int

*)

// -----------------------------------


// -----------------------------------
// 1. Create a function that converts a string (e.g "Sunday")
// into an DayOfWeek type
// -----------------------------------

(*
1) Define a DayOfWeek type with a case for each day
2) Define a strToDayOfWeek function  which is total

TIP - you can pattern match on strings!

match x with
| "a" ->
| "b" ->
| etc
| _ ->  // wildcard matches anything

*)

let notImplemented() = failwith "not implemented"
type undefined = exn

module Calendar =
    type DayOfWeek = Sun | Mon | Tue | Wed | Thu | Fri | Sat

    let strToDayOfWeek_WithException str =
        match str with
        | "Sunday" | "Sun" -> Sun
        | "Monday" | "Mon" -> Mon
        | "Tuesday" | "Tue" -> Tue
        | "Wednesday" | "Wed" -> Wed
        | "Thursday" | "Thu" -> Thu
        | "Friday" | "Fri" -> Fri
        | "Saturday" | "Sat" -> Sat
        | _ -> failwith "input is not a DayOfWeek"

    // Exercise: convert the function to be total
    //           by extending the output.
    let strToDayOfWeek str = notImplemented()


(*
// test the exception throwing function
Calendar.strToDayOfWeek_WithException "Sunday"  // good
Calendar.strToDayOfWeek_WithException "Sun"     // good
Calendar.strToDayOfWeek_WithException "April"   // exception :(

// test the total function
Calendar.strToDayOfWeek "Sunday"  // good
Calendar.strToDayOfWeek "Sun"     // good
Calendar.strToDayOfWeek "April"   // nice error
*)


// -----------------------------------
// 2. Create a function that converts a string into an int
// -----------------------------------

module IntUtil =

    let strToInt_WithException (str:string) =
        // this is how to parse an int
        match System.Int32.TryParse str with
        // the Int32.TryParse method returns a tuple
        | (true,i) -> i
        | (false,_) -> failwith "input is not an int"

    // Exercise: Convert this function to be total
    //           by extending the output.
    // Reuse the "match System.Int32.TryParse" code
    // but return something different
    //
    // Question: Why is it not called "strToInt"
    // like the original function?
    let tryStrToInt s = notImplemented()

(*
// test the exception throwing function
IntUtil.strToInt_WithException "123"     // good
IntUtil.strToInt_WithException "hello"   // exception :(

// test the total function
IntUtil.tryStrToInt "123"     // good
IntUtil.tryStrToInt "hello"   // nice error

*)

// -----------------------------------
// 3. Create a function that gets the first item in a list
// -----------------------------------

module ListUtil =
    // In F#, lists are implemented as linked lists, NOT as arrays/vectors

    // to pattern match:
    // for empty list, use []
    // for non-empty list, use firstItem :: rest
    //    where "rest" is another linked list
    //
    // we normally work on the first element only,
    // not the nth one

    let firstItem_withException aList =
        match aList with
        // match an empty list
        | [] ->
            failwith "list does not have a first item"
        // match an non-empty list
        | first::rest ->
            first // return the first item

    // Exercise: Convert this function to be total
    //           by extending the output.
    // Suggestion: it should have a different name!
    let firstItem aList = notImplemented()

let emptyList :int list = []
// NOTE: the :int list is just to get around an issue working interactively!
// it is not normally needed

(*
// test the exception throwing function
ListUtil.firstItem_withException [1;2;3]         // good
ListUtil.firstItem_withException(emptyList)     // exception :(

// test the total function
ListUtil.tryFirstItem [1;2;3]      // good
ListUtil.tryFirstItem (emptyList)  // no exception!
*)
