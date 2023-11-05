// ===================================
// Object-oriented F# syntax
// ===================================


(*
=====================================================
An abstract "interface" focuses on behavior with no implementation
=====================================================
*)

type IExample =
//              ^--- note: no parameters here

    // public read-only properties
    abstract member MyNumber: int

    // public setters (return nothing)
    abstract member SetMyNumber: int -> unit

    // public method
    abstract member AddToMyNumber: int -> int
        // add the input my number and return it


// An empty ("marker") interface needs the "interface end" to terminate
type MarkerInterface =
    interface end

// An interface can inherit/extend another interface
type Vehicle =
    interface end

type Car =
    inherit Vehicle

(*
=====================================================
A concrete class has private state and public behavior
=====================================================
*)


type Person(name,age) =
//              ^--- Constructor parameters passed in here.
//                   These are accessible as local immutable fields

    // private state (must be initialized to some value)
    let mutable isMarried = false
    let mutable numberOfChildren = 0

    // ----------------------------------------
    // public read-only properties
    // ----------------------------------------

    member this.Name = name
//         ^--- self reference needed here, even if not used

    member this.Age =
        age
//      ^--- returns field from constructor

    member this.IsMarried =
        isMarried
//      ^--- returns private field

    // ----------------------------------------
    // public setters (returns nothing/unit)
    // ----------------------------------------

    member this.SetNumberOfChildren input =
        numberOfChildren <- input
//                       ^--- updates private field using mutation

    // ----------------------------------------
    // public instance methods
    // ----------------------------------------

    member this.Info =
        sprintf "%s has age %i and %i children" this.Name this.Age numberOfChildren
//                                              ^--- self reference used here

    // ----------------------------------------
    // public static method (not attached to any instance)
    // ----------------------------------------

    static member ConstructNewAlice() =
//                ^--- no self reference needed here
//                     because it is static
        Person("Alice",29)
//      ^--- constructor is called like a function


// try it
let person1 = Person("Bob", 24)
//            ^--- constructor is called like a function
person1.Name
person1.Info

let person2 = Person.ConstructNewAlice()
//            ^--- call static method
person2.Name
person2.SetNumberOfChildren 2
person2.Info


(*
=====================================================
A concrete class can implement an interface
=====================================================
*)

type Example() =
//          ^--- Constructor parameters are needed for classes
//               even if not used (otherwise it's an interface!)

    // local state
    let mutable theNumber = 0

    // implement the interface
    interface IExample with

        // property
        member this.MyNumber =
            theNumber

        // setter
        member this.SetMyNumber input =
            theNumber <- input

        // method
        member this.AddToMyNumber input =
            theNumber +  input


// try it
let example = Example()
example.SetMyNumber 42  // ERROR because not an IExample
example.AddToMyNumber 5

let example2 = Example() :> IExample
//                       ^--- Safe cast to IExample
example2.SetMyNumber 42
example2.AddToMyNumber 5
