// ====================================
// Intro to F#, Part C -- F# is different
// ====================================

(*
F# is different from mainstream languages
like Javascript, Python, Java, C#

This can take some getting used to!
*)

// ====================================
// Difference 1: F# does not allow implicit casting
// ====================================


// try evaluating each of these lines in turn
1 + 1.5        // error
1 + int 1.5    // OK
float 1 + 1.5  // OK

1 + "2"        // error
1 + int "2"    // OK
string 1 + "2" // OK

1 = true        // error
1 = System.Convert.ToInt32(true)  // OK

// ====================================
// Difference 2: F# does not have mutability by default
// ====================================

let x = 10
x = 11        // this means "does x equal 11?"

// the assignment operator in F# is the backwards arrow "<-"
x <- 11    // try this but you will still get an error

// In F# values are NOT mutable by default, you have
// to ask for it using the "mutable" keyword
let mutable z = 10  // ask for a mutable value
z <- 11             // OK

// TIP: We almost NEVER need mutability
// You can write whole programs without
// a single mutable value

// ====================================
// Difference 3: F# syntax gotchas
//
// There are a few places where F# syntax is different
// than you might expect.
// ====================================

// Equality	is "=" not "=="
// 1==2   // C-style syntax
1=2       // F# syntax

// Inequality is "<>" not "!="
// 1 != 2     // C-style syntax
1 <> 2        // F# syntax

// Negation	is "not" not "!"
// !(1==2)    // C-style syntax
not (1=2)     // F# syntax

// Declaration is "let" not "var"
// var x = 1;    // JS/Java/C# syntax
let x = 1        // F# syntax

// Mutation is "<-"
// x = 2;    // C-style syntax
// x <- 2    // F# syntax

// Function parameter separator is space not comma
// int f(int x,int y) {...}   // C-style syntax to define a function
// let f x y = ...            // F# syntax to define a function

// f(x,y);   // C-style syntax to call a function
// f x y     // F# syntax to call a function

// List separator is semicolon not comma
// [ 1; 2; 3 ]              // F# syntax for a new list
// { name="Scott"; age=27}  // F# syntax for a new record

// If you see a comma, it's probably part of a tuple
// let x = (2,3)   // construct
// let (y,z) = x   // deconstruct

// Colon is normally something to do with types
// val x:int = 1
//      ^---type
// type MyRecord = {x:int}
//                   ^---type

// Curly braces are NOT used for blocks -- indentation is
let x =
   let y = 1
   y + 1

// Curly braces ARE used for records
// { name: string; age:int} // F# syntax for a record definition
// { name="Scott"; age=27}  // F# syntax for a record constructor


