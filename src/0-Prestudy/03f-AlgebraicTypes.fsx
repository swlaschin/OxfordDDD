// =============================
// Algebraic Types
// =============================

(*
In functional programming languages,
new types are built from smaller types using COMPOSITION.

The two main ways are combining using AND and OR.

For example, using AND we might have:
    "A Meal consists of a Starter AND Main AND Dessert"

In F#, we would model this as a "product type".

It could be a unnamed type -- a "tuple" -- like this:
   type Meal = Starter * Main * Dessert

or it could be a named type with a named field for each component,
called a "record" type:

    type Meal = {
        starter : Starter
        main : Main
        dessert: Dessert
        }

    The syntax is:
        main : Main
        ^label  ^type for this component


An example using OR might be:
    "A Snack consists of a Fruit OR Pastry OR Nothing"

In F#, we would model this as a "discriminated union" or "sum type".
I like to call them "choice types" because they represent choices in the domain.

    type Snack =
        | Fruit of Fruit
        | Pastry of Pastry
        | Nothing

    The syntax is:
        | Fruit of Fruit
          ^case/tag  ^type for this case
*)


(*
This file demonstrates constructing and destructuring
    * records
    * tuples
    * unions/choices
    * wrappers
    * functions
*)


// ----------------------------------
// 1. Constructing and Destructuring records
// ----------------------------------

// Here's a record type used for modeling
// FirstName "AND" LastName
type PersonalName = {
    FirstName: string
    LastName: string
    }

// To create a record value, use a similar syntax,
// but use "=" for assignment
let aliceSmith = {
    FirstName="Alice"
    LastName="Smith"
    }

// To extract a component from the record, use dot syntax
let first = aliceSmith.FirstName


// ----------------------------------
// 2. Constructing and Destructuring tuples
// ----------------------------------

// Here's a record type used for modeling
// FirstName "AND" LastName as a tuple
type FirstName = string
type LastName = string
type PersonalNameTuple = FirstName * LastName

// To create a tuple, use a comma
let bobSmith = ("Bob","Smith")

// To deconstruct a tuple, use a comma on the left-hand side
let (first,last) = bobSmith


// ----------------------------------
// 3. Constructing and Destructuring Choices
// ----------------------------------

// a helper type for the example below
type CardInfo = {
    CardNumber:string
    // etc
    }

// a helper type for the example below
type EmailAddress = string

// Here's a choice type used for modeling
// Cash OR Card OR PayPal
type PaymentMethod =
  | Cash
  | Card of CardInfo
  | PayPal of EmailAddress

(*
The "Cash" case has no data associated with it
The "Card" case has "CardInfo" data associated with it
The "PayPal" case has "EmailAddress" data associated with it
*)

// To construct the Cash case of PaymentMethod,
// use "Cash" as a constructor function.
// No extra data is needed
let paymentMethod1 = Cash                // no extra data needed


// To construct the Card case of PaymentMethod,
// use "Card" as a constructor function,
// with a "cardInfo" as the parameter to the function
let cardInfo = {CardNumber="123"}
let paymentMethod2 = Card cardInfo

// To construct the PayPal case of PaymentMethod,
// use "PayPal" as a constructor function,
// with an "emailAddress" as the parameter to the function
let emailAddress = "abc@example.com"
let paymentMethod3 = PayPal emailAddress


// to destructure a choice type, use pattern matching
let printMethod paymentMethod =
  match paymentMethod with
   // each match is a bit like a lambda, with an ->
  | Cash ->
        printfn "Cash"
  | Card cardInfo ->
        // cardInfo is available only in this pattern match
        printfn "Card with %A" cardInfo
  | PayPal emailAddress ->
        // emailAddress is available only in this pattern match
        printfn "PayPal with %A" emailAddress

// test
printMethod paymentMethod1
printMethod paymentMethod2
printMethod paymentMethod3

// ----------------------------------
// 4. Constructing and Destructuring Wrappers
// ----------------------------------

(*
A "wrapper" type is a wrapper around another type,
used to make a distinction between it and the inner type.
*)

// A "wrapper" around an int
type OrderId = OrderId of int

// to create a wrapper, use the case as a function
let orderId = OrderId 99

// to deconstruct a wrapper, there are a number of ways
// Approach 1: use pattern matching with one case
let value1 =
    match orderId with
    | OrderId i -> i // return the inner value

// Approach 2: use pattern matching on the LEFT hand side
let (OrderId value2) = orderId
// value2 is now 99

// Approach 3: in functions you can use the pattern matching directly in the parameter
let printOrderId (OrderId value) =
    printfn "OrderId = %i" value

// test
printOrderId orderId   // output is "OrderId = 99"


// ----------------------------------
// 5. Constructing Function types
// ----------------------------------

(*
A function type can be used to model a process or action.
For example:

type ValidateCreditCard = CardNumber -> bool
type ProcessOrder = UnprocessedOrder -> ProcessedOrder

*)

// To implement a function based on a function type
// 1.  define a value in the normal way, but use the function type as the type annotation
//     let placeOrder : PlaceOrder = ...
// 2.  for the implementation, use a lambda with however many parameters are needed.

// here's an example of adding two numbers

// a type definition
type AddTwoNumbers = int -> int -> int

// an implementation of AddTwoNumbers
let addTwoNumbers : AddTwoNumbers =
    fun n1 n2 ->
        n1 + n2

// a type definition
type ValidateEmailAddress = string -> bool

// an implementation of ValidateEmailAddress
let validateEmailAddress : ValidateEmailAddress =
    fun emailAddr ->
        emailAddr.Contains("@")
