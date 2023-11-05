// ============================================
// Common utility code for turtle graphics
// ============================================

open System

// round a float to two places to make it easier to read
let round2places (flt:float) = Math.Round(flt,2)

/// Calculate a new position from the current position given an angle and a distance
/// calcNewPosition :   Distance -> Angle -> currentPos:Position -> Position
let calcNewPosition (distance:float) (angle:float) (currentX,currentY) =
    // Convert degrees to radians with 180.0 degrees = 1 pi radian
    let angleInRads = angle * (Math.PI/180.0)
    // new pos
    let newX = currentX + (distance * cos angleInRads)
    let newY = currentY + (distance * sin angleInRads)
    // return a new Position
    round2places newX, round2places newY
