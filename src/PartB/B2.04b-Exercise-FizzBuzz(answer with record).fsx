﻿// =============================================
// Exercise:
// Implement FizzBuzz as a pipeline
//
// Answered using a Record as an intermediate value
// =============================================


(*
Definition of FizzBuzz:
    Given a number
    If it is divisible by 3, return "Fizz"
    If it is divisible by 5, return "Buzz"
    If it is divisible by 3 and 5, return "FizzBuzz"
    Otherwise return the number as a string
    Do NOT print anything
*)

// helper function
let isDivisibleBy divisor n =   // question: why put the divisor first?
    (n % divisor) = 0



(*

Exercise:

Rewrite this using a piping model.

let fizzBuzz n =
    n
    |> handle15case
    |> handle3case
    |> handle5case
    |> finalStep


After getting this to work, see if you can define a
single "handle" function that can be reused for 3, 5, and 15.


*)



// Define a record structure to pass between the tests for 3,5,7 etc
type FizzBuzzData = {Output:string; Number:int}

/// Test whether a data.number is divisible by 15
/// If true, return the "FizzBuzz" in data.handled.
/// BUT only do this if not already handled (data.handled is empty)
let handle15case fizzBuzzData =

    // is it already handled?
    if fizzBuzzData.Output <> "" then
        fizzBuzzData // leave alone

    // is it divisible?
    else if not (fizzBuzzData.Number |> isDivisibleBy 15) then
        fizzBuzzData // leave alone

    // ok, handle this case
    else
        // create a new value which is handled
        {Output="FizzBuzz"; Number=fizzBuzzData.Number}
        // alternatively you can copy with update
        // {fizzBuzzData with resultString="FizzBuzz"}

/// A much more generic version of handle15case
/// --------------------------------------------
/// Test whether data.number is divisible by divisor
/// If true, return the label in data.handled.
/// BUT only do this if not already handled (data.handled is empty)
let handle divisor label fizzBuzzData =

    // is it already handled?
    if fizzBuzzData.Output <> "" then
        fizzBuzzData // // already processed, leave alone

    // is it divisible?
    else if not (fizzBuzzData.Number |> isDivisibleBy divisor) then
        fizzBuzzData // leave alone

    // ok, handle this case
    else
        // create a new value which is handled
        {Output=label; Number=fizzBuzzData.Number}
        // alternatively you can copy with update
        // {fizzBuzzData with resultString=label}


// If still unhandled at the end,
// convert data.number into a string,
// else return data.handled
let finalStep fizzBuzzData =
    if fizzBuzzData.Output = "" then
        string fizzBuzzData.Number // convert number to string
    else
        fizzBuzzData.Output

// something else to put in the pipeline
let log message data =
    printfn $"[{message}]: {data}"
    data

// Finally, the main fizzBuzz function!
let fizzBuzz (n:int) :string =
    let initialData = {Output=""; Number=n}

    initialData
    |> handle 15 "FizzBuzz"
    |> handle 3 "Fizz"
    |> handle 5 "Buzz"
    |> finalStep


// test it interactively
[1..30] |> List.map fizzBuzz





