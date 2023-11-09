(* ======================================
Part of "Thirteen ways of looking at a turtle"
Related blog post: http://fsharpforfunandprofit.com/posts/13-ways-of-looking-at-a-turtle/
======================================

Common code for FP-style immutable turtle functions WITH BOUNDS

====================================== *)


// requires Common.fsx to be loaded by parent file
// Uncomment to use this file standalone
//#load "Common.fsx"

open System
open Common

// ======================================
// Turtle module
// ======================================

type TurtleState = {
    position : Position
    angle : float<Degrees>
    penState : PenState
}

module Turtle = 

    let initialTurtleState = {
        position = initialPosition
        angle = 0.0<Degrees>
        penState = initialPenState
    }                

    // return distance moved as well as state
    let move distance state =
        Logger.info (sprintf "Move %0.1f" distance)
        // calculate new position 
        let startPosition = state.position
        let endPosition = calcNewPositionBounded distance state.angle startPosition 
        // draw line if needed
        if state.penState = Down then
            Canvas.draw startPosition endPosition 
        let distanceMoved = pythagDistance startPosition endPosition 
        // return distanceMoved and new state
        distanceMoved, {state with position = endPosition}


    let turn angleToTurn state =
        Logger.info (sprintf "Turn %0.1f" angleToTurn)
        // calculate new angle
        let newAngle = (state.angle + angleToTurn) % 360.0<Degrees>
        // update the state
        {state with angle = newAngle}

    let penUp state =
        Logger.info "Pen up" 
        {state with penState = Up}

    let penDown state =
        Logger.info "Pen down" 
        {state with penState = Down}


