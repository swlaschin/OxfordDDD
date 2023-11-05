(*
Turtle graphics provides a representation of a physical “turtle”
(a little robot with a pen) that draws on a sheet of paper on the floor.


A turtle knows:
* whether the pen is up or down
* its direction as an angle in degrees counterclockwise (using float)
* its position as an x,y coordinate (using floats)

You can send the following commands to the turtle:

* PenUp -- changes the state of the pen from down to up
* PenDown -- changes the state of the pen from up to down
* TurnLeft N -- rotates the turtle N degrees left (increases angle)
* MoveForward N -- moves the turtle N centimetres forward
*)

// load utility code for turtle graphics
#load "TurtleGraphicsLib.fsx"
open TurtleGraphicsLib


// A synonym for now
type Angle  = float

// Enumeration of available pen states
type PenState = Up | Down

// A structure to store the (x,y) coordinates
type Position = {x:float; y:float}
// QUESTION: how does this design compare with just using a tuple (float * float)

// just a synonym for now
type Distance = float

// ===============================
// 1. Create an interface to model the turtle's behaviour (state and methods)
// ===============================

// a. Define three public read-only properties that expose the state
// b. Define four public methods that expose the behaviour

type ITurtle =
    // public read-only properties
    abstract member PenState: PenState
    abstract member Angle: Angle
    abstract member Position: Position

    abstract member PenUp: unit -> unit
    abstract member PenDown: unit -> unit
    abstract member TurnLeft: Angle -> unit
    abstract member MoveForward: Distance -> unit



// ===============================
// 2. Create an implementation of the interface
// ===============================

// a. Define three private internal fields to store the state
// b. Implement the public properties and methods
// c. Create a static property "NewTurtle" which creates a new turtle with pen up, 0 angle, 0 position

type Turtle() =
    // a. internal fields (Note, they have to be initialized to something)
    let mutable penState : PenState = Up
    let mutable angle : Angle = 0.0
    let mutable position : Position = {x=0.0; y=0.0}

    // b. implement the interface
    interface ITurtle with
        member this.PenState = penState
        member this.Angle = angle
        member this.Position = position

        member this.PenUp() =
            penState <- Up  // mutate the internal field

        member this.PenDown() =
            penState <- Down  // mutate the internal field

        member this.TurnLeft leftAngle =
            let newAngle = angle + leftAngle
            let boundedAngle = if newAngle >= 360.0 then newAngle - 360.0 else newAngle
            angle <- boundedAngle  // mutate the internal field

        member this.MoveForward distance =
            let newX,newY = calcNewPosition distance angle (position.x,position.y)
            position <- {x=newX; y=newY}

    // c. create a static method which returns a new ITurtle with pen up, 0 angle, 0 position
    static member NewTurtle() : ITurtle =
        Turtle() :>  ITurtle

// QUESTION: Why does NewTurtle create a new turtle each time, rather than reusing
// the same constant turtle?



// ===============================
// 4. Write a method "drawTriangle" that, given a ITurtle interface,
// draws a triangle and leaves the turtle facing the original direction
// back at the starting point.
//
// The turtle instructions to use are:
//   penDown, forward 10.0
//   turnLeft 120.0, forward 10.0
//   turnLeft 120.0, forward 10.0
//   turnLeft 120.0
//
// QUESTION: Should this be a method on the Turtle? Or on a new class?
// ===============================

type TurtleShapes() =

    static member drawTriangle (turtle:ITurtle) =
        turtle.PenDown()
        turtle.MoveForward 10.0
        turtle.TurnLeft 120.0
        turtle.MoveForward 10.0
        turtle.TurnLeft 120.0
        turtle.MoveForward 10.0
        turtle.TurnLeft 120.0
        // QUESTION: how can I make this more pipeline like?

// ===============================
// 5. write a test that checks that the angle logic works
//
// Given a new turtle
// Then using "drawTriangle"
// Expect final angle is same as initial angle (to nearest int)
// ===============================

// helper function for testing
let assertEqualFloat (val1:float) (val2:float) (context:string) =
    let val1Rounded = System.Math.Round(val1,2)
    let val2Rounded = System.Math.Round(val2,2)
    if val1Rounded <> val2Rounded then failwith $"[{context}] {val1Rounded} not equal to {val2Rounded}"

type TurtleTest1() =

    // TIP use "int" to round a float to an int
    member this.TestDrawTriangle() =
        // arrange
        let turtle = Turtle.NewTurtle()
        let angInitial = turtle.Angle   // we have to cache the angle because it will mutate later

        // act
        TurtleShapes.drawTriangle turtle
        let angFinal = turtle.Angle

        // assert
        assertEqualFloat angInitial angFinal "TestDrawTriangle"


(*
// try it
TurtleTest1().TestDrawTriangle()
*)

// ===============================
// 6. write a test that checks that the distance logic works
//
// Given a turtle at position (4,3) with angle -143.13
// Then using "moveForward 5"
// Expect final position to be (0,0) (to nearest integer)
// ===============================

type TurtleTest2() =

    member this.Test345Triangle() =
        // arrange
        let turtle = Turtle.NewTurtle()
        turtle.MoveForward 4.0
        turtle.TurnLeft 90.0
        turtle.MoveForward 3.0
        turtle.TurnLeft -90.0  // back to beginning
        turtle.TurnLeft -143.13

        // act
        turtle.MoveForward 5.0

        // assert
        assertEqualFloat turtle.Position.x 0.0 "Test345Triangle: x Position"
        assertEqualFloat turtle.Position.y 0.0 "Test345Triangle: y Position"

(*
// try it
TurtleTest2().Test345Triangle()
*)
