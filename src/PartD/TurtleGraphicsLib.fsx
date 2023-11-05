// ============================================
// Common utility code for turtle graphics
// ============================================

open System

// round a float to two places to make it easier to read
let round2places (flt:float) = Math.Round(flt,2)

// A structure to store the (x,y) coordinates
type Position = {x:float; y:float}

/// Calculate a new position from the current position given an angle and a distance
let calcNewPosition (distance:float) (angle:float) (pos:Position) : Position =
    // Convert degrees to radians with 180.0 degrees = 1 pi radian
    let angleInRads = angle * (Math.PI/180.0)
    // new pos
    let newX = pos.x + (distance * cos angleInRads)
    let newY = pos.y + (distance * sin angleInRads)
    // return a new Position
    {x=round2places newX; y=round2places newY}

let pythagDistance pos1 pos2 =
    let sq x = x * x
    sq (pos1.x - pos2.x) + sq (pos1.y - pos2.y) |> Math.Abs |> Math.Sqrt
