// ====================================
// Intro to F#, Exercises
// ====================================

(*
How to do these exercises:

1. The exercise files come in pairs:
* one has the questions with blanks for you to fill in
* the other has the answers

2. Try writing code to fill in the blanks in the "question" file

3. Look at the answer file whenever you like:
* to check your own answers, or
* to look at working code if you get stuck, or
* to look at working code if you run out of time


The exercises in this file should be straightforward,
just to get you started on how this works.
*)


// =============================================
// Question: Define a function that multiplies its argument by two.
// What is its signature?

let multipliedByTwo x = ??

// Can you make a similar function with floats?
// What is its signature?
let floatMultipliedByTwo x = ??


// =============================================
// Q. Create a `sayHello` function that uses `sprintf` instead
// of `printfn`.
// If you pass in "Alice" as the name,
// the result should be "Hello Alice".

let sayHello aName = ??

// What is its signature?

// test it
sayHello "Alice"


// =============================================
// Q. Write a `sayGreeting` function that takes two
// parameters: `greeting` and `name`, separated by spaces.
// If you pass in "Hello" as the greeting and
// "Alice" as the name, the result should be "Hello Alice".

let sayGreeting ?? ?? = ??

// What is the signature of this function?


// test it
sayGreeting "Hello" "Alice"  // "Hello Alice"


// =============================================
// Q. Create an `average` function that
// takes two ints and returns their average as a float
// E.g. If you pass in 2 and 3, the result should 2.5
//
// TIP you may need to use "float" to convert from ints to floats

let average int1 int2  = ??

// What is its signature?

// test it
average 2 3   // expect 2.5
