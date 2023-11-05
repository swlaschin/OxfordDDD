// =================================
// Exercise : Converting textual domain model to types
// =================================

(*
Exercise

For each of the textual domain models below, convert them into a corresponding F# type

It doesn't matter what the exact definitions are, as long at this file compiles :)

// IMPORTANT:  types must be defined BEFORE they are referenced
// (e.g. earlier in the file )

*)

(*
Need help with syntax?
See A2.02a-TypedDomainModel-Help.fsx for the syntax you might need
*)

// TIP: this is a useful helper definition for unknown types
type undefined = exn

(*
------------------------------------------
data PersonalName = FirstName AND LastName
*)

type PersonalName = ??

(*
------------------------------------------
data OrderLine =
    OrderId
    AND Product
    AND OrderQuantity
*)

type OrderId = ??
type Product = ??
type OrderQuantity = ??
type OrderLine = ??

(*
------------------------------------------
data Blog =
  BlogName
  AND list of BlogPosts

data BlogName = all printable chars, maxlen = 100
*)

type BlogName = ??
type BlogPost = ??
type Blog = ??


(*
------------------------------------------
data TShirtColor = Black OR Red OR Blue
data TShirtSize = Large OR Small
data TShirt = TShirtSize AND TShirtColor

data HoodieColorSmall = Black OR White
data HoodieColorLarge = Red OR Blue
data Hoodie =
  Large (with HoodieColorLarge)
  OR Small (with HoodieColorSmall)
*)

type TShirtColor = ??
type TShirt = ??

type Hoodie = ??


(*
------------------------------------------
data ExpressShippingType = OneDay OR TwoDay
data ShippingMethod =
  Standard
  OR Express (with ExpressShippingType)
*)

type ExpressShippingType = ??
type ShippingMethod = ??


(*
------------------------------------------
workflow PlayMove =
    inputs: Move AND GameState
    outputs: MoveResult AND (new)GameState
*)

type Move = undefined
type MoveResult = undefined
type GameState = undefined

type PlayMove = Move * GameState -> ??
            // ^pair of inputs      ^output

// could also be written in curried style like this
type PlayMove_v2 = Move -> GameState -> ??
                // ^param1 ^param2      ^output
