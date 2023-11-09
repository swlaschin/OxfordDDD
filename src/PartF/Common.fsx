(*
Common.fsx

Part of "Thirteen ways of looking at a turtle"
Talk and video: http://fsharpforfunandprofit.com/turtle/

*)

open System

// ======================================
// Constants
// ======================================

let canvasSize = 100

// ======================================
// Common types and helper functions
// ======================================

/// An alias for a float
type Distance = float

/// Use a unit of measure to make it clear that the angle is in degrees, not radians
type [<Measure>] Degrees

/// An alias for a float of Degrees
type Angle  = float<Degrees>

/// Enumeration of available pen states
type PenState = Up | Down

/// A structure to store the (x,y) coordinates
type Position = {x:float; y:float}


// ======================================
// Common helper functions
// ======================================

// round a float to two places to make it easier to read
let round2 (flt:float) = Math.Round(flt,2)

/// Calculate a new position from the current position given an angle and a distance
/// calcNewPosition :   Distance -> Angle -> currentPos:Position -> Position
let calcNewPosition (distance:Distance) (angle:Angle) currentPos =
    // Convert degrees to radians with 180.0 degrees = 1 pi radian
    let angleInRads = angle * (Math.PI/180.0) * 1.0<1/Degrees>
    // current pos
    let x0 = currentPos.x
    let y0 = currentPos.y
    // new pos
    let x1 = x0 + (distance * cos angleInRads)
    let y1 = y0 + (distance * sin angleInRads)
    // return a new Position
    {x=round2 x1; y=round2 y1}


let boundedAxis x =
    let max = float canvasSize
    let min = -max
    if x > max then
        max
    else if x < min then
        min
    else x

let bounded pos =
    {x = boundedAxis pos.x; y = boundedAxis pos.y}

let pythagDistance pos1 pos2 =
    let sq x = x * x
    sq (pos1.x - pos2.x) + sq (pos1.y - pos2.y) |> Math.Abs |> Math.Sqrt

let calcNewPositionBounded distance angle position =
    calcNewPosition distance angle position
    |> bounded

/// Default initial state
let initialPosition,initialPenState =
    {x=0.0; y=0.0}, Down

/// Emulating a real implementation for drawing a line
let dummyDrawLine log oldPos newPos =
    // for now just log it
    log (sprintf "...Draw line from (%0.1f,%0.1f) to (%0.1f,%0.1f)" oldPos.x oldPos.y newPos.x newPos.y)

/// trim a string
let trimString (str:string) = str.Trim()

// ======================================
// Canvas
// ======================================

module Canvas =
    open System.Windows.Forms
    open System.Drawing

    let mutable canvas : Form = null

    // constants
    let scale = 2.0
    let bgColor = Color.AntiqueWhite
    let boundary = 5

    let scaled (x,y) =
        let x = int (scale * x)
        let y = int (scale * y)
        x,y

    let toScreen (x,y) =
        let x = x + float (canvasSize + boundary)
        let center = float (canvasSize + boundary)
        let y = center - y // change direction
        let x,y = (x,y) |> scaled
        x,y

    // toScreen (0.,0.)

    let createCanvas() =
        let windowSize = (canvasSize + boundary) * 2 |> float
        let scaledSize = (windowSize,windowSize) |> scaled |> fun (x,y) -> x+15, y+40
        canvas <- new Form(Width=fst scaledSize,Height=snd scaledSize)

    let drawBoundary() =
        use g = canvas.CreateGraphics()
        use pen = new Pen(Color.Red)
        pen.Width <- 1.0f
        pen.StartCap <- Drawing2D.LineCap.Round
        pen.EndCap <- Drawing2D.LineCap.Round
        let leftBottom = (float -canvasSize,float -canvasSize) |> toScreen
        let leftTop = (float -canvasSize,float canvasSize) |> toScreen
        let rightTop = (float canvasSize,float canvasSize) |> toScreen
        let rightBottom = (float canvasSize,float -canvasSize) |> toScreen
        g.DrawLine(pen, fst leftBottom, snd leftBottom, fst leftTop, snd leftTop)
        g.DrawLine(pen, fst leftTop, snd leftTop, fst rightTop, snd rightTop)
        g.DrawLine(pen, fst rightTop, snd rightTop, fst rightBottom, snd rightBottom)
        g.DrawLine(pen, fst rightBottom, snd rightBottom,fst leftBottom, snd leftBottom)

    let clear() =
        use g = canvas.CreateGraphics()
        g.Clear(bgColor)
        drawBoundary()

    open System.Runtime.InteropServices
    [<DllImport("user32.dll")>]
    extern IntPtr GetForegroundWindow()

    [<DllImport("user32.dll")>]
    extern bool SetForegroundWindow(IntPtr hWnd)

    let init() =
        if canvas=null || canvas.IsDisposed then
            createCanvas()

        let screens = Screen.AllScreens
        let offsetX = 
            if screens.Length > 1 then                
                screens.[1].Bounds.X
            else
                screens.[0].Bounds.X
        
        canvas.TopMost <- true
        canvas.StartPosition <- FormStartPosition.CenterScreen
        canvas.StartPosition <- FormStartPosition.Manual
        canvas.Location <- Point(offsetX+600,100)
        canvas.Text <- "Turtle Canvas"
        canvas.MaximizeBox <- false
        canvas.MinimizeBox <- false
        canvas.BackColor <- bgColor

        let currWindow = GetForegroundWindow()
        canvas.Show()
        drawBoundary()
        SetForegroundWindow(currWindow) |> ignore

    // Canvas.init()

    let draw pos1 pos2 =
        use g = canvas.CreateGraphics()
        use pen = new Pen(Color.Black)
        pen.Width <- 4.0f
        pen.StartCap <- Drawing2D.LineCap.Round
        pen.EndCap <- Drawing2D.LineCap.Round
        let startPos = (pos1.x,pos1.y) |> toScreen
        let endPos = (pos2.x,pos2.y) |> toScreen
        g.DrawLine(pen, fst startPos, snd startPos, fst endPos, snd endPos);


    (*
    // test
    Canvas.init()
    Canvas.draw {x=  0.0; y=  0.0} {x= 10.0; y=20.}
    Canvas.draw {x= 10.0; y= 20.0} {x= 50.0; y=50.}
    Canvas.draw {x= 50.0; y= 50.0} {x= -50.0; y=50.0}
    Canvas.draw {x= -50.0; y=50.0} {x=  0.0; y=  0.0}
    Canvas.clear()
    *)


// ======================================
// Logger
// ======================================

module Logger =

    let info msg = printfn "INFO %s" msg
    let warn msg = printfn "WARN %s" msg
    let error msg = printfn "ERROR %s" msg

