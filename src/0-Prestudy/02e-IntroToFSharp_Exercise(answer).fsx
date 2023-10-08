// ====================================
// Intro to F#, Exercises (answers)
// ====================================

// =============================================
// Define a function that multiplies its argument by two.
// What is its signature?

let multiplyBy2 x = x * 2
// val multiplyBy2 :  x:int -> int

// Can you make a similar function with floats?
// What is its signature?

let floatMultipliedByTwo x = x * 2.0
// val floatMultipliedByTwo : x:float -> float

// =============================================
// Q. Create a `sayHello` function that uses `sprintf` instead
// of `printfn`.
// If you pass in "Alice" as the name,
// the result should be "Hello Alice".

let sayHello aName =
    sprintf "Hello %s" aName

// The type signature is:
// val sayHello :  aName:string -> string

// test it
sayHello "Alice"

// =============================================
// Q. Write a `sayGreeting` function that takes two
// parameters: `greeting` and `name`, separated by spaces.
// If you pass in "Hello" as the greeting and
// "Alice" as the name, the result should be "Hello Alice".

let sayGreeting greeting aName =
    sprintf "%s %s" greeting aName

// The type signature is:
// val sayGreeting : greeting:string -> aName:string -> string

// test it
sayGreeting "Hello" "Alice"  // "Hello Alice"

// =============================================
// Q. Create an `average` function that
// takes two ints and returns their average as a float
// E.g. If you pass in 2 and 3, the result should 2.5
//
// TIP you may need to use "float" to convert from ints to floats

let average int1 int2  =
    float(int1 + int2) / 2.0

// What is its signature?
// val average : int1:int -> int2:int -> float

// test it
average 2 3   // expect 2.5
