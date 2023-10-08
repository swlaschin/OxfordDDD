//===================================
// Functions as things
//===================================

(*
One of the key aspects to understanding functional programming
is realizing that functions are treated as "things", just
like strings, ints, bools, etc.

This file demonstrates:
  * functions as values
  * named functions vs anonymous functions (lambdas)
  * functions as input parameters
  * functions as output
  * partial application
  * function transformers
*)

// ===============================================
// Part 1: Functions are "things"
// ===============================================

// define two simple functions
let add1 x = x + 1
let multiplyBy2 x = x * 2

// and test them
let three = add1 2 // result is 3
let six = multiplyBy2 3 // result is 6

// You can assign a function value to another value, like this
let q = add1
// q is a function AND a value -- a "function value"
// val q : (int -> int)

// Again we can assign a function value to another value, like this
let r = multiplyBy2
// val r : (int -> int)

// and now we can use these values as functions
let threeAgain = q 2 // result is 3
let sixAgain = r 3 // result is 6

// ===============================================
// Part 2: Anonymous functions/Lambdas
// ===============================================

// A "anonymous function" can be defined without a name
// using the "fun" keyword.
// A "anonymous function" is also known as a "lambda"

fun x -> x + 1

// lambdas are often used when passing a function into
// another function as a parameter

List.filter (fun x -> x > 4) [1..10]
//          ^the filter fn   ^the list to filter
// val it : int list = [5; 6; 7; 8; 9; 10]

// lambdas can be assigned a name, just like an int or string value
let myFilter = fun x -> x > 4
List.filter myFilter [1..10]

// ===============================================
// Part 3: Functions can be defined in multiple ways
// ===============================================

// Here's two different ways to define a one parameter function

let increment_v1 x = x + 1
// val increment_v1 : x:int -> int

let increment_v2 = fun x -> x + 1
// val increment_v2 : x:int -> int

// These two definitions are the SAME THING!

// -----------------------------------
// Four different ways to define a two parameter function
// -----------------------------------

// A two-parameter function defined in the usual way
let add_v1 x y = x + y
// val add_v1 : x:int -> y:int -> int

// A two-parameter function defined as a lambda
// Note: "add_v2" is a "thing" with no parameters.
let add_v2 = fun x y -> x + y
// val add_v2 : x:int -> y:int -> int

// A two-parameter function defined as a mixture of normal definition and lambda
// * "x" is a parameter of "add_v3"
// * "y" is a parameter of the lambda.
let add_v3 x = fun y -> x + y
// val add_v3 : x:int -> y:int -> int

// another alternative to v3
// returning an inner function rather than a lambda
let add_v4 x =
    let innerFn y = x + y
    innerFn
// val add_v4 : x:int -> (int -> int)

// These definitions are all the SAME THING!

(*
Question: Which way is better?

Answer: It depends!

* Named functions are easier to understand, especially if the
  body of the function is long
* Lambdas are easier for inline functions that are short
  and don't need names

// here's a good use of a named function
let myFunc x =
    lots of code
    lots of code
    lots of code
    lots of code

// here's a good use of a lambda
List.map (fun i -> i + 1) [1..10]

*)



// ===============================================
// Part 4: Functions are things, part 2
// ===============================================

// because they are things, functions can be put in lists, etc

let listOfFunctions =
    let add1 x = x + 1
    let multiplyBy2 x = x * 2
    let subtract3 x = x - 3
    [add1; multiplyBy2; subtract3]

// loop through the list and for each function, execute it
for fn in listOfFunctions do
    let result = fn 100
    printfn "If 100 is the input, the output is %i" result

    // Result =>
    // If 100 is the input, the output is 101
    // If 100 is the input, the output is 200
    // If 100 is the input, the output is 97

// ===============================================
// Part 5: Functions need to have parameters!
// ===============================================

// A function MUST have a parameter, even if it
// is only unit "()"

// "hello1" below is NOT a function, just a unit value
let hello1 = printfn "Hello"
// the console output shows "Hello" immediately
// as soon as you evaluate it

// "hello2" IS a function value
let hello2() = printfn "Hello"
// the console output shows nothing when you evaluate it
// -- you need to run it to see anything.

// assign the function value to a new name
let z = hello2

// evaluate later
z     // doesnt do anything
z()   // runs the function
z()   // runs the function again

// ===============================================
// Part 6: Functions can be used as parameters
// ===============================================

(*
Because functions are things, they can be used as parameters
to another function, just like a string or int parameter
*)

// define a function with a function parameter
let evalWith5ThenAdd2 fn =
    fn 5 + 2     // same as fn(5) + 2
// val evalWith5ThenAdd2 : fn:(int -> int) -> int
//                         ^the function parameter

// define some simple functions
let add42 x = x + 42
let multiplyBy5 x = x * 5

// it will work with ANY int -> int function
evalWith5ThenAdd2 add42 // result => 49
evalWith5ThenAdd2 multiplyBy5 // result => 27
evalWith5ThenAdd2 (fun i -> i + 42) // result => 49

// ===============================================
// Part 7: Functions can be the output of a function
// ===============================================

// this function returns a lambda with numberToAdd baked in
let adderGenerator numberToAdd =
    fun x -> numberToAdd + x

// val adderGenerator :
//    int -> (int -> int)

// now test it
let add1 = adderGenerator 1
add1 2   // result => 3
add1 42   // result => 43

let add100 = adderGenerator 100
add100 2   // result => 102
add100 42   // result => 142

// This is an alternative way of doing the implementation
// using an inner function that is returned
let multiplierGenerator numberToMultiply =
    let innerFn x = numberToMultiply * x
    innerFn  // <=== return the inner function

let multiplyBy2 = multiplierGenerator 2
multiplyBy2 3   // result => 6

let multiplyBy100 = multiplierGenerator 100
multiplyBy100 2   // result => 200


// ===============================================
// Part 8: Partial application
// ===============================================

(*
Partial application is the technique of "baking in" some
but not ALL of the parameters
*)

// Say that we start with this:
printfn "Hello %s" "Alice"
printfn "Hello %s" "Bob"

// We see that (printfn "Hello %s") always occur together,
// so let's "bake" the first template parameter in
let printName = printfn "Hello %s" // no second parameter!

// to use "printName", we MUST supply the missing parameter
printName // error: missingParam
printName "Alice"
printName "Bob"

// ===============================================
// Part 9: Function transformers
// ===============================================

(*
It's common for functions to have a function as input AND a function as input

These kinds of functions "transform" the input function somehow.
*)

// Demonstration 1:
// Transform any 'int->int function into a 'string->string function
let toStrFunction f =
    fun (str:string) ->
        // 1. get an int from a string
        let i = int str
        // NOTE No error handling here. We will discuss that later!

        // 2. execute the int->int function and convert the result back into a string
        let j = f i

        // 3. convert the result back into a string
        string j

// test
let example1 = toStrFunction (fun i -> i + 1)
example1 "42"  // "43"

let example2 = toStrFunction (fun i -> i * i)
example2 "9"  // "81"

// --------------------------------
(*
The most common kind of function transforms start with a
function for "normal" values and convert it into
a function that works on more complex values like Option or List
*)

// Demonstration 2 -- for Lists:
// Transform any 'a->'b function into a 'a list -> 'b list function
let toListFunction f =
    fun (list: 'a list) ->
        [for x in list do yield (f x)]

// Test it: transform a "int->int" function into a  "int list -> int list" function
let listFunction1 = toListFunction (fun i -> i + 1)
// now test it
listFunction1 [1..5]


// Test it: Use partial application to define a "int -> string" function
let hello = sprintf "Hello %i"
// then transform it into a  "int list -> string list" function
let listFunction2 = toListFunction hello
// now test it
listFunction2 [1..5]



