// ======================================
// Intro to F#, Part B -- The Basics of F#
//
//   * literals
//   * comments
//   * printing to the console
//   * the "unit" type
//   * sprintf
//   * modules
// ======================================

// ======================================
// Literal values
//
// Literal values are ints, strings, booleans, etc
// ======================================

"hello"  // string has double quotes
42       // int
3.141    // float
3.0M     // decimal/money
true     // bool
[1;2;3]  // list (immutable -- note the use of semicolons not commas)
()       // unit (like void -- see discussion below)

// ======================================
// Comments
// ======================================

// a one line comment
(* a multi line comment has round brackets *)
//    /* not slashes like C */

// ======================================
// Printing to the console using "printfn"
// ======================================

// equivalent to printfn in other languages:
//   C#: Console.WriteLine(...)
//   Java: System.Out(...)
//   Kotlin: printLn(...)
//   JS: console.log(...)
//   Python: print(...)

// To use "printfn", pass a template string followed by parameters
// printfn [templateString] [param1] [param2] ...

printfn "hello %s" "Bob"       // %s for string
printfn "the answer is %i" 42  // %i for int

printfn "pi is %f" 3.141       // %f for fixed representation of a float
printfn "pi is %g" 3.141       // %g for shortest representation of a float
printfn "pi is %0.1f" 3.14159  // with formatting
printfn "pi is %0.9f" 3.14159  // with formatting

printfn "the result is %b" false  // %b for bool
printfn "The list is %A" [1..3]   // %A for anything -- very useful

// for multiple parameters, use multiple placeholders in the template
printfn "%s is %i years old" "Alice" 42


// ======================================
// Printing example and introduction to "unit" type
// ======================================

// Define a function that prints squares
// up to N. Notice the use of indentation!
let printSquares n =
   for i in [1..n] do
      let sq = i*i
      printfn "%i" sq

// call it with 5
printSquares 5

(*
Look at the type signature
   val printSquares : n:int -> unit

The input is an "int" and the output is "unit"

"Unit" is an important concept in FP. It represents the type
of "nothing". If there is no output (or input), that is
represented by the unit type.

Printing to the console is an example of "no output".
The printing is a side effect. The function itself does not
return anything useful.

*)


// for example
let sayHello aName = printfn "Hello %s" aName

(*
The signature is
   val sayHello : aName:string -> unit
In other words:
   the input is a string and the output is nothing
*)

// another example
let theMeaningOfLife()  = 42
(*
The signature of this function is:
   val theMeaningOfLife : unit -> int

There is no input ( represented by the empty "()" )
and the output is an int.
*)

// Another example
let printHello()  = printfn "hello"
(*
The signature of this function is:
   val printHello : unit -> unit

There is no input ( represented by the empty "()" )
and there is no output.

TIP: Functions with no input or output are considered Bad Things in functional programming

*)


// ======================================
// sprintf is like printfn except that it returns a string
// ======================================
(*
Unlike "printfn", "sprintf" returns a string rather than printing it.
That means you can assign something to the result, rather than the result
being "unit"

Otherwise, it works the same way with the same format string placeholders.
*)

let x = sprintf "%i" 42     // x = "42"
let y = sprintf "%f" 3.15   // y = "3.150000"

// ====================================
// Modules
// ====================================
(*
How do you group code together in F#?

Answer: a "module"
*)

// the "module" keyword defines a module like this
module MyModule =
   // everything indented afterwards is now part of the module

   // IMPORTANT: when you want to evaluate it interactively,
   // you must highlight *everything* in the module
   // from the "module" keyword to the last function.

   // this is a function defined in "MyModule"
   let add2 x =
      x + 2

   // this is a value defined in "MyModule"
   let pi = 3.141

   // this is another function defined in "MyModule"
   let add3 x =
      x + 3

// this function is NOT part of "MyModule"
// because it is not indented
let add4 x =
   x + 4


(*
Exercise: highlight *everything* in the module and evaluate it
interactively. You should see a result like this:

module MyModule = begin
  val add2 : x:int -> int
  val pi : float = 3.141
  val add3 : x:int -> int
end
*)

(*
There are a number of ways to access things in a module.

First way: use the module as a prefix
*)

MyModule.add2 40
// val it : int = 42
MyModule.pi
// val it : float = 3.141

// Without the module prefix, the functions are not in scope
add2 40
// error FS0039: The value or constructor 'add2' is not defined.

(*
Second way: bring the entire module into scope with the
"open" keyword
*)

// highlight and run this line
open MyModule  // "open" is same as "using" or "import" in other languages

// now you can access the code directly without a prefix
add2 40
pi
add3 30


