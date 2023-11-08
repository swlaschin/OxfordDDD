(*
=================================
Railway oriented programming pipeline for turtle errors

Use the turtle functions from our ealier example,
but now they might return errors!

Exercise:
   1. Fix the "drawTriangle" pipeline in section 4 using Result.bind and Result.map
   2. Amend the pipeline in as described section 5

TIP: evaluate the code in small chunks, rather than evaluating whole file at once

=================================
*)

// IMPORTANT - if you get errors such as
//   Could not load type 'ErrorMessage' from assembly 'FSI-ASSEMBLY,
// then try:
// 1. Reset the FSI interactive
// 2. Load code in small chunks

// Load a file with library functions for Result
#load "Result.fsx"  // TIP evaluate this import separately before evaluating the rest of the code below


(*
NOT NEEDED FOR THIS EXAMPLE
// load utility code for turtle graphics
#load "TurtleGraphicsLib.fsx"
open TurtleGraphicsLib
*)


// ===============================
// 1. data structure to model the turtle's state
// ===============================

// VERY SIMPLIFIED DESIGN  -- we don't care about the implementation here

type Angle  = int
type Distance  = int
type PenState = Up | Down | NoInk   // NOTE new state!

type Turtle = {
    penState : PenState
    angle : int
    position : int  // simplified to one dimension!
}

let newTurtle = {
    penState=Up
    angle=0
    position=0
    }

type TurtleErrorReason =
    | CannotDraw
    | HitWall

type TurtleError =
    {
    turtle : Turtle
    reason: TurtleErrorReason
    }


// ===============================
// 2. function types to model the actions on the turtle
// ===============================

type PenUp = Turtle -> Turtle

// will fail if PenState = NoInk
type PenDown = Turtle -> Result<Turtle,TurtleError>
type TurnLeft = Angle -> Turtle -> Turtle

// will fail if position  > 200
type MoveForward = Distance -> Turtle -> Result<Turtle,TurtleError>

// ===============================
// 3. implementations of those function types
// ===============================

let penUp: PenUp =
    fun turtle -> {turtle with penState=Up}

let penDown: PenDown =
    fun turtle ->
        if turtle.penState = NoInk then
            Error {turtle=turtle; reason=CannotDraw}
        else
            Ok {turtle with penState=Down}

let turnLeft: TurnLeft =
    fun angle turtle ->
        let newAngle = turtle.angle + angle
        let boundedAngle = if newAngle >= 360 then newAngle - 360 else newAngle
        {turtle with angle=boundedAngle}


let moveForward: MoveForward =
    fun distance turtle ->
        let newPosition = turtle.position + distance
        if newPosition > 200 then
            Error {turtle=turtle; reason=HitWall}
        else
            Ok {turtle with position = newPosition}


// ===============================
// 4. Fix this pipeline so there are no errors
// Use Result.bind and Result.map
// ===============================

(*
let drawTriangle turtle =
    turtle
    |> penDown
    |> moveForward 10
    |> turnLeft 120
    |> moveForward 10
    |> turnLeft 120
    |> moveForward 10
    |> turnLeft 120.0
*)


let drawTriangle turtle =
    turtle
    |> penDown
    |> Result.bind(moveForward 10)
    |> Result.map(turnLeft 120)
    |> Result.bind(moveForward 10)
    |> Result.map(turnLeft 120)
    |> Result.bind(moveForward 10)
    |> Result.map(turnLeft 120)



// ===============================
// 5. Amend this pipeline
// ===============================

(*
The problem with this pipeline is that when something goes wrong
the turtle gets stuck and can't change.

1. Create some new functions that fix the error so the turtle can continue
2. Create new versions of moveForward, penDown  that have the fix appended to them so
   that drawTriangle looks clean again
*)


// A function that fixes the errors
type FixTurtleError = TurtleError -> Turtle
let fixTurtleError :  FixTurtleError =
    fun turtleError ->
        match turtleError.reason with
        // set the penstate to Up
        | CannotDraw -> {turtleError.turtle with penState=PenState.Up}
        // reset to position 0
        | HitWall -> {turtleError.turtle with position=0}


// Create a function that, given a Result<Turtle,TurtleError>
// turns it into a Turtle
let fixTurtleResult (result:Result<Turtle,TurtleError>) :Turtle =
    match result with
    | Ok turtle ->
        // what goes here?
        turtle
    | Error turtleError ->
        // what goes here?
        // fix turtle error by calling fixTurtleError
        fixTurtleError turtleError


// Create a new version of moveForward that fixes any errors and always returns a Turtle
let moveForward' distance (turtle:Turtle) :Turtle =
    turtle
    |> moveForward distance
    |> fixTurtleResult


// Create a new version of penDown that fixes any errors and always returns a Turtle
let penDown' (turtle:Turtle) :Turtle =
    turtle
    |> penDown
    |> fixTurtleResult


//  This is the final version will the error handling hidden
let drawTriangle' turtle =
    turtle
    |> penDown'
    |> moveForward' 10
    |> turnLeft 120
    |> moveForward' 10
    |> turnLeft 120
    |> moveForward' 10
    |> turnLeft 120
