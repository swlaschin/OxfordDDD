// =============================================
// Exercise:
// Implement FizzBuzz in parallel
// =============================================


// helper function
let isDivisibleBy divisor n =   // question: why put the divisor first?
    (n % divisor) = 0

// a "choice" data structure to pass between the tests for 3,5,7 etc
type FizzBuzzData =
    | Handled of string
    | Unhandled of int

/// Test whether a number is divisible by divisor
/// If true, return the label in the Handled choice
/// BUT only do this if not already handled
let handle divisor label fizzBuzzData =
    match fizzBuzzData with
    // if it is already handled
    | Handled _ ->
        fizzBuzzData // leave alone

    // if it is not handled
    | Unhandled number ->
        // is it divisible?
        if not (number |> isDivisibleBy divisor) then
            fizzBuzzData // leave alone
        // ok, handle this case
        else
            // create a new value which is handled
            Handled label

// If still unhandled at the end,
// convert number into a string,
// else return the Handled value
let finalStep fizzBuzzData =
    match fizzBuzzData with
    // if it is already handled
    | Handled str ->
        str // use the string

    // if it is not handled
    | Unhandled number ->
        string number // convert to string


// ============================================
// Working in parallel!
// ============================================

// by breaking down the logic into small composable functions,
// we can mix and match them in new ways.

// For example, we can run that same handle function in "parallel"
// and then we can eliminate the need for the "15" handler

/// Combine two fizzbuzz results
let combineData fizzBuzzData1 fizzBuzzData2 =

    match fizzBuzzData1,fizzBuzzData2  with
    // if it is already handled
    | Handled str1, Handled str2 ->
        Handled (str1 + str2)
    | Handled str1, Unhandled _ ->
        Handled str1
    | Unhandled _ , Handled str2 ->
        Handled str2
    // if it is not handled
    | Unhandled number1, Unhandled _ ->
        Unhandled number1

// something else to put in the pipeline
let log message data =
    printfn $"[{message}]: {data}"
    data

// The main fizzBuzz function using parallel handling
let fizzBuzzParallelHandler (n:int) :string =

    let initialData = Unhandled n

    let pairsToHandle = [
        handle 3 "Fizz"
        handle 5 "Buzz"
        ]

    pairsToHandle
    |> List.map (fun handler -> handler initialData)  // this gives us a list of FizzBuzzData
    |> List.reduce combineData  // combine each element of the list into a single value
    |> finalStep



// try it
let fizzBuzzParallel =
    [1..35]
    |> List.map fizzBuzzParallelHandler
    |> String.concat ","
    |> printfn "%s"


