// ================================================
// Function Composition and Piping
// ================================================

(*
A second key aspect to understanding functional programming
is the use of COMPOSITION everywhere:

* bigger functions are built by composing smaller functions
* bigger types are built by composing smaller types

This file demonstrates:
* composition of functions
* piping
* partial application in combination with piping

Exercise: Execute each chunk of code in this file and
and make you understand how it works.
*)


// ======================================
// Transformation-oriented programming
// ======================================

(*

In functional programming, the focus is on *transformation* of data.

Unlike the object-oriented approach, data structures are kept separate from the
functions that act on them.

Data structures are thus "dumb"; it is the functions that do all the work,
transforming input data structures to (possibly different) output data structures.

One of my favorite analogies for this is to think of functions as little bits of railroad track,
with a tunnel in the middle that tranforms the input into an output.

Here are some simple function definitions:
*)

let add1 x = x + 1
let double x = x * 2
let square x = x * x

(*
The compiler outputs the following signatures:

val add1 : int -> int
val double : int -> int
val square : int -> int

You can interpret the arrow as "transforms",
so
  add1 : x:int -> int
means that the function `add1` takes an `int` parameter called `x`
and transforms it to another `int`.

Obviously, this are trivial examples. But for more complex functions,
the concept of "transformation" is a powerful metaphor.
*)

// ================================================
// Composition of functions
// ================================================

(*
Question: How do you combine smaller functions into a bigger one?

Composition in most programming languages means nesting the function calls, like this
*)

add1(5)                   // = 6
double(add1(5))           // = 12
square(double(add1(5)))   // = 144


(*
In functional languages, new "railway track" is created by joining smaller pieces of "track".
In F#, combining "track" looks like this, using the ">>" operator to compose functions:
*)

// create a new function from two smaller functions...
let add1_double = add1 >> double

// ... and then call this new function
add1_double 5


// create a new function from three smaller functions...
let add1_double_square = add1 >> double >> square

// ... and call this new function
add1_double_square 5    // 144


// ================================================
// Piping examples
// ================================================

(*
Another common approach is to "chain" a set of transformations (that is, functions) together.

To do this we using a "pipe" model, in which the output of one function is sent
as the input to the next function in the chain.

This is, of course, similar to using pipes in UNIX.

In F# the pipe operator is written `|>` and piping works left to right.

Here's some examples of piping in use:
*)

5 |> add1                     // = 6
// five is sent into the input of "add1"

5 |> add1 |> double           // = 12
// five is sent into the input of "add1",
// then the output of "add1" is passed to the input of "double"

5 |> add1 |> double |> square // = 144
// five is sent into the input of "add1",
// then the output of "add1" is passed to the input of "double",
// then the output of "double" is passed to the input of "square"

(*
As the chains get longer, we often make it more readable by putting
each step on a new line, like this:
*)

5
|> add1
|> double
|> square // = 144

// TIP: you can easily transform a pipeline into a new function like this:

let addDoubleSquare x =
    x
    |> add1
    |> double
    |> square

// test it
addDoubleSquare 5 // 144

// ================================================
// Composition vs Piping
// ================================================

(*
What is the difference between piping and composition?

Composition combines two functions to make a new function
It doesn't need any extra data as a parameter.
*)
let add1_double_using_composition =
    add1 >> double

(*
Piping NEEDS an initial value to send down the pipe.
*)

let add1_double_using_piping x =
    x |> add1 |> double
//  ^ the "x" parameter is needed here

(*
TIP: In F# we almost always use piping instead of composition
*)


// ================================================
// Piping and partial application
// ================================================

(*
Piping works very nicely in combination with partial application.
Remember that in partial application we "bake in" many of the parameters, but leave some missing.
If we leave only one parameter missing, we can pass that extra parameter down the pipeline
*)

(*
For example
  List.filter (fun x -> x > 5)
has the filter function baked in, but is missing the list to process.

But we can pass the list in using the pipeline, like this:
*)

[1..10]
|> List.filter (fun x -> x > 5)  // missing list param comes from pipeline


// the output of the filtered list can be passed into another partially applied function
[1..10]
|> List.filter (fun x -> x > 5)  // missing list param comes from pipeline
|> List.map (fun x -> x + 100)   // missing list param comes from pipeline

// here's another example
["Alice"; "Bob"; "Carol"; "John"]
|> List.filter (fun x -> x.Length < 5)
|> List.map (fun x -> sprintf "Hello %s" x)





