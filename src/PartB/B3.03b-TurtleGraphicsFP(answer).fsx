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

// QUESTION: how does this design of Position compare with just using a tuple (float * float)

// just a synonym for now
type Distance = float

// ===============================
// 1. Create a data structure to model the turtle's state
// ===============================

type Turtle = {
    penState : PenState
    angle : Angle
    position : Position
}

// and create a value which is a new turtle with pen up, 0 angle, 0 position
let newTurtle = {
    penState=Up
    angle=0.0
    position={x=0.0;y=0.0}
    }

// ===============================
// 2. Create function types to model the actions on the turtle
// ===============================

// what are the input(s) and what are the output(s)
type PenUp = Turtle -> Turtle
    // the input is the original turtle and the output is the updated turtle

type PenDown = Turtle -> Turtle

// QUESTION: Should Turtle or Angle come first in the parameter list?
type TurnLeft = Angle -> Turtle -> Turtle
   // Answer: turtle comes last, so we can use pipelines

type MoveForward = Distance -> Turtle -> Turtle

// ===============================
// 3. Create implementations of those function types
// ===============================

(*
// TIP -- this is how to do an implementation
let penUp : PenUp =
    fun input(s) -> function body

// TIP -- this is how to update a record
{myRecord with [fieldName]=newValue}

*)

let penUp: PenUp =
    fun turtle -> {turtle with penState=Up}

let penDown: PenDown =
    fun turtle -> {turtle with penState=Down}

(*
TIP: You should keep the angle between >= 0 and < 360
You can use code like:
   let boundedAngle = if newAngle >= 360.0 then newAngle - 360.0 else newAngle

*)

let turnLeft: TurnLeft =
    fun angle turtle ->
        let newAngle = (turtle.angle + angle)
        let boundedAngle = if newAngle >= 360.0 then newAngle - 360.0 else newAngle
        {turtle with angle=boundedAngle}


let moveForward: MoveForward =
    fun distance turtle ->
        let newX,newY = calcNewPosition distance turtle.angle (turtle.position.x,turtle.position.y)
        {turtle with position = {x=newX; y=newY}}


// ===============================
// 4. Write program "drawTriangle" that, given a turtle,
// draws a triangle and leaves the turtle facing the original direction
// back at the starting point.
//
// The turtle instructions to use are:
//   penDown, forward 10.0
//   turnLeft 120.0, forward 10.0
//   turnLeft 120.0, forward 10.0
//   turnLeft 120.0
// ===============================

let drawTriangle turtle =
    turtle
    |> penDown
    |> moveForward 10.0
    |> turnLeft 120.0
    |> moveForward 10.0
    |> turnLeft 120.0
    |> moveForward 10.0
    |> turnLeft 120.0

// ===============================
// 5. write a test that checks that the angle logic works
//
// Given a new turtle
// Then using "drawTriangle"
// Expect final angle is same as initial angle (to nearest int)
// ===============================

// helper function for testing whether floats are equal
let assertEqualFloat (val1:float) (val2:float) (context:string) =
    let val1Rounded = System.Math.Round(val1,2)
    let val2Rounded = System.Math.Round(val2,2)
    if val1Rounded <> val2Rounded then failwith $"[{context}] {val1Rounded} not equal to {val2Rounded}"


// test that, after calling "drawTriangle"
// the turtle is pointing the same direction
let testDrawTriangle() =
    let tInitial = newTurtle
    let tFinal = drawTriangle tInitial
    assertEqualFloat tInitial.angle tFinal.angle "testDrawTriangle"

(*
// try it
testDrawTriangle()
*)


// ===============================
// 6. write a test that checks that the distance logic works
//
// Given a turtle at position (4,3) with angle -143.13
// Then using "moveForward 5"
// Expect final position to be (0,0) (to nearest integer)
// ===============================

// test that, after doing a 3-4-5 triangle
// the turtle is back at the origin
let test345Triangle() =
    let tInitial = {newTurtle with angle = -143.13; position={x=4.0; y=3.0} }
    let tFinal = tInitial |> moveForward 5.0
    assertEqualFloat tFinal.position.x 0.0 "test345Triangle: x position"
    assertEqualFloat tFinal.position.y 0.0 "test345Triangle: y position"

(*
// try it
test345Triangle()
*)
