// =================================
// Type inference examples
//
// =================================

(*
Demonstrates:
  * how type inference works
  * types in format strings
  * type annotations
  * generic types

Exercise: Execute each chunk of code in this file and
and make sure you understand how it works.
*)

// ===============================================
// Part 1: How type inference works
// ===============================================

// Here's a contrived example of a function
let doSomething f x =
   let y = f (x + 1)
   "hello" + y

(*
Without explicitly specifying types, the F# compiler has inferred
that the function signature is:

    val doSomething : f:(int -> string) -> x:int -> string

How did it do that?

First it saw:
   (x + 1)
which means that "x" must be an int because it is
being added to 1 (no automatic casting!)

Then it saw:
   "hello" + y
which means that "y" must be an string because it is
being added to a string

Next:
    let y = f (x + 1)

"f" is a function, and the input is an int and the output is a string
so the signature of "f" is "int->string"

So now the compiler has inferred that:
  * "f" parameter is of type "int->string"
  * "x" parameter is of type "int"
  * the return type ("hello" + y) is a string

Thus the overall signature is:
    val doSomething : f:(int -> string) -> x:int -> string
*)


// Exercise: if you change 1 to 1.0 below,
// what do you expect the new signature to be?
// Try it and see!
let doSomething_v2 f x =
   let y = f (x + 1)
   "hello" + y

// Exercise: if you change ("hello"+y) to ("hello"=y) below,
// what do you expect the new signature to be?
// Try it and see!
let doSomething_v3 f x =
   let y = f (x + 1)
   "hello" + y

// ===============================================
// Part 2: type inference in format strings
// ===============================================

// what is the type of "aName"
let printName aName =
    printfn "Hello %s" aName

// Exercise: change "%s" to "%i"
// What is the type of "aName" now?
// Try it and see!
let printName_v2 aName =
    printfn "Hello %s" aName

// test
let name = "Alice"
printName name
printName_v2 name  // you might get a type error here!

// Example - defining a function with two int parameters
let printIntAndString anInt aStr =
    printfn "int=%i str=%s" anInt aStr

// the signature is:
//   val printIntAndString : x:int -> y:string -> unit

// test
printIntAndString 1 "hello"

(*
Try passing the wrong type of parameter!

printIntAndString 1 2
// -----------------^--
// error FS0001: This expression was expected to have type "string"
//               but here has type "int"
*)

// ====================================
// Part 3: Helping the compiler with type annotations
// ====================================

(*
//TODO -- uncomment this to see the error
let toUpper x =
    x.ToUpper()
    // => error FS0072: Lookup on object of indeterminate type

// This is because we are calling into the object-oriented
// system library. "x" could be any type -- the compiler cannot tell which
*)

// the same definition but with a type annotation. Now the compiler is happy.
let toUpper (x:string) =
    x.ToUpper()


// Here's how to do type annotations
let aFunction (param1:string) (param2:bool) :string =
//             ^1st param      ^2nd param    ^return type
    // etc
    "" // dummy

(*
IMPORTANT: the parameter types MUST be in parentheses.
If the type is not in parentheses it is the return type!

let aFunction (param1:string) =
//             ^GOOD 1st param is a string.

let aFunction param1:string =
//             ^BAD 1st param is untyped. Return type is string!

*)

// You can choose to have type annotations or not.
// Here's some different ways of writing the same function...

// version 1: no type annotations
let helloInt_v1 anInt =
    sprintf "Hello %i" anInt

// version 2: annotation on parameter only
let helloInt_v2 (anInt:int) =
    sprintf "Hello %i" anInt

// version 3: annotation on return value only
let helloInt_v3 anInt :string =
    sprintf "Hello %i" anInt

// version 4: annotation on parameter and return value
let helloInt_v4 (anInt:int) :string =
    sprintf "Hello %i" anInt

// TIP: Use type annotations when you are getting started,
// and the type inference is not doing what you expect.
// This will help you narrow down your errors.


// ====================================
// Part 4: generic types
// ====================================

// Question, what type is "x"
let returnSameThing x = x

(*
Answer: "x" could be a string, an int, a list, anything!

When the type could be anything, F# infers
a "generic" type rather than a specific type like int or string.

A "generic" type is indicated by a letter with a tick in front,
like this:

    val returnSameThing : x:'a -> 'a
*)

(*
// NOTE in Java/C# this would be written as...
T returnSameThing<T>(T x) {
  return x;
  }
*)

// A useful example of generics is to swap a tuple
let swap (x,y) = (y,x)

(*
The signature is
    val swap : 'a * 'b -> 'b * 'a

The input is a pair with some generic type 'a and another generic type 'b.
The output is the pair swapped.

The use of  generic types 'a and 'b shows that they could be different
but that the output use the same types, reversed.
*)
//

// If two values are compared, they must BOTH be the same type
let compare x y = (x=y)
(*
The signature shows this:
    val compare : 'a -> 'a -> bool
*)


// Example: these functions ignore their input,
// and so the input is inferred to be a generic type
let ignoreTheInput x = ()
let ignoreTwoInputs x y = ()
let ignoreThreeInputs x y z = ()

(*
val ignoreTheInput : x:'a -> unit
val ignoreTwoInputs : x:'a -> y:'b -> unit
val ignoreThreeInputs : x:'a -> y:'b -> z:'c -> unit
*)

