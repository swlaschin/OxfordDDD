(*
// =================================
// Demo: Units of Measure and ConstrainedTypes
// =================================

F# is unusual in that it supports Units Of Measure (metres, kilograms, etc)
as part of the type system.

We won't use it in the rest of the course because it is not available in
other programming languages.

However, it might be useful to see how it works in practice
and how it fits into Domain Driven Design

We create unit of measures to:
(a) help with documentation
(b) avoid errors caused by mixing up different units
    E.g. this expensive error: https://www.simscale.com/blog/nasa-mars-climate-orbiter-metric/


*)

type undefined = exn

// For example, let's say we want to document whether a timeout is in
// seconds or milliseconds.

// to define a unit of measure, use the "Measure" attribute on a type
[<Measure>] type second
[<Measure>] type millisecond

// use it directly
type NetworkAction = undefined
type SetTimeout = NetworkAction -> int<second> -> NetworkAction
type GetTimeout = NetworkAction -> int<second>

// or define a type
type TimeInSecs = int<second>
type Alarm = undefined
type SetTimeRemaining = Alarm -> TimeInSecs -> Alarm
type GetTimeRemaining = Alarm -> TimeInSecs



// --------------------------------
// Avoiding mixups with type checking
// --------------------------------

[<Measure>] type cm
[<Measure>] type inch


1<cm> = 1<inch>
(*
Expecting a
    'int<cm>'
but given a
    'int<inch>'
The unit of measure 'cm' does not match the unit of measure 'inch'
*)


let oneCm = 1<cm>    // int
let oneInch = 1.0<inch>  // float
let twoCm = 2.0m<cm> // decimal

oneCm = oneInch
(*
This expression was expected to have type
    'int<cm>'
but here has type
    'float<inch>'
*)

// even in the same measure, ints vs floats are still checked as usual
oneCm = twoCm
(*
This expression was expected to have type
    'int<cm>'
but here has type
    'decimal<cm>'
*)

let convertInchToCm (i:float<inch>) =
    i * 2.54<cm/inch>

convertInchToCm oneInch
// val it : float<cm> = 2.54

// --------------------------------
// compound units
// --------------------------------

// NOTE: All the SI Units are available in F# in the FSharp.Data.UnitSystems.SI namespace
// but we will define only the ones we need here for our demo

[<Measure>] type m
[<Measure>] type sec
[<Measure>] type kg

let distance = 1.0<m>
let time = 2.0<sec>
let speed = 2.0<m/sec>
let acceleration = 2.0<m/sec^2>
let force = 5.0<kg m/sec^2>

// derived units are created automatically
distance / time
// val it : float<m/sec> = 0.5

distance / time / time
// val it : float<m/sec> = float<m/sec ^ 2> = 0.25