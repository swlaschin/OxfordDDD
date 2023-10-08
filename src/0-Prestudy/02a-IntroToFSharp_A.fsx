// ================================
// Intro to F#, Part A -- My first F# program
// ================================

// It's traditional for tutorials to start with "hello world", so here it is in F#

printfn "Hello World"

(*

Even with such a simple bit of code, there are some interesting things to note:

* This snippet does not need a containing class.
* It can be run directly in an interactive environment
* There is a space between the `printfn` function and its parameter,
  rather than parentheses.
  We'll see why this is important soon!
*)


// ======================================
// TIP
// ======================================
(*
TIP: Highlight and evaluate small chunks of code at a time
rather than highlighting and running the whole file!
That way, any errors that happen are localised.
*)

// ======================================
// Defining simple values
// ======================================

// for the code below, try highlighting
// each non-comment line
// and executing it -- one line at a time

// -------------------
// declare a immutable value (not a "variable")
let myName = "Scott"
// and use it
printfn "my name is %s" myName

// -------------------
// Defined values are global in memory and can be reused.
// For example, you can run this again and it will remember
// that "myName" is already defined.
printfn "my name is %s" myName

// ======================================
// TIP
// ======================================
(*
TIP: Sometimes the interactive session gets confused when you
have redefined the same code many times over in different ways.
In that case, best to kill everything and start again!
- in VS code, kill the F# terminal
- in Visual Studio, do "Reset Interactive Session"
*)

// ======================================
// Defining a one parameter function
// ======================================

// define a function with one parameter called "aName"
let printName aName =
    printfn "Hello %s" aName

// Now highlight the entire function (both lines) and evaluate it.
// You MUST highlight the entire function -- you can't do one line at a time!



(*
A few things to note about this code:

* The "let" keyword is used for functions too.

  The `printName` function is defined using the "let" keyword,
  just like the `name` was. This is not a coincidence!
  In functional programming, functions are things just like strings and ints.

* Spaces again. `printName` has one parameter ("aName")
  and we use spaces rather than parentheses and commas.

* No curly braces! Instead F# uses indentation to indicate blocks of code,
  in this case, the body of the printName function.

*)

// now test the function
let name = "Alice"
printName name


// ======================================
// Defining a two parameter function
// ======================================


// Now define a function with two parameters
let add x y =
   x + y

// now call it with 1 and 2 as parameters
// notice that spaces are used to separate the parameters
add 1 2


(*
Note that you do NOT need a "return" keyword.
The return value is the last expression in the function body.
*)

let complexAdd x y =
   let z = x + 1
   let w = y + 2
   let total = z + w
   total     // <== this is the return value



// ====================================
// Function values vs simple values
// ====================================
(*
We said that the "let" keyword is used to define both simple values (ints, strings)
and function values. How can we tell the difference?
*)

// --------------------
// Simple Values

let x = 1
// val x : int = 1             // <======= look at the signature

let y = "hello"
// val y : string = "hello"    // <======= look at the signature

(*
For simple values, the signature is

val [name] : [type] = [value]
*)

// --------------------
// Function Values

let add1 x = x + 1
// val add1 : x:int -> int   // <======= look at the signature.
                             //          It has an arrow in it!


let sayHello aName = "hello " + aName
// val sayHello : aName:string -> string   // <======= the signature has an arrow.


(*
For function values, the signature is

val [functionName] : [inputType] -> [outputType]

In some cases the parameter name is also in the signature

val [functionName] : [parameterName:inputType] -> [outputType]
*)

// ======================================
// Function types
// ======================================
(*
Every time you see a colon ":" it is normally followed by a type

val x : int = 1
      ^ followed by a type

val add1 : int -> int
         ^ followed by a type

Just like simple values, functions have a type.
The type is everything after the first colon (ignoring the parameter names).

For example, the type of the function "add1"
   val add1 : x:int -> int
is
   (int -> int)

The type of the function "sayHello"
   val sayHello : aName:string -> string
is
   (string -> string)

*)


// ====================================
// Functions with multiple arguments
// ====================================
(*
When a function has multiple arguments, the function signature has an arrow between each parameter.
The type after the last arrow is the return or output type.

val [functionName] : [param1Type] -> [param2Type] -> [outputType]

The type of this function is:
  (param1Type -> param2Type -> outputType)

*)

// example of a two parameter function
// Note that the parameters are separated by spaces in the definition.
let add x y =
   x + y
// val add : x:int -> y:int -> int   // <======= two arrows

// and another one
let concat first last =
   first + " " + last
// val concat : first:string -> last:string -> string   // <======= two arrows

// example of a three parameter function
let add3 x y z =
   x + y + z
// val add3 : x:int -> y:int -> z:int -> int   // <======= three arrows


