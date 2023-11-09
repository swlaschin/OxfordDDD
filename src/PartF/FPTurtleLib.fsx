(* ======================================
Part of "Thirteen ways of looking at a turtle"
Related blog post: http://fsharpforfunandprofit.com/posts/13-ways-of-looking-at-a-turtle/
======================================

Common code for FP-style immutable turtle functions.

====================================== *)


// requires Common.fsx to be loaded by parent file
// Uncomment to use this file standalone
// #load "Common.fsx"

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

    // note that state is LAST param in all these functions
    let move distance state =
        Logger.info (sprintf "Move %0.1f" distance)
        let startPosition = state.position 
        // calculate new position 
        let endPosition = calcNewPosition distance state.angle startPosition 
        // draw line if needed
        if state.penState = Down then
            Canvas.draw startPosition endPosition 
        // update the state
        {state with position = endPosition}

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

