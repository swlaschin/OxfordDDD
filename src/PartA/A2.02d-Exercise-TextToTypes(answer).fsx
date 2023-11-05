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

type PersonalName = {
    FirstName : string
    LastName : string
    }

// The tuple version is not readable!
type PersonalName2 = string * string

(*
------------------------------------------
data OrderLine =
    OrderId
    AND Product
    AND OrderQuantity
*)

type OrderId = int
type ProductId = int
type OrderQuantity = int

type OrderLine = {
    OrderId : OrderId
    Product : ProductId
    OrderQuantity : OrderQuantity
    }

(*
------------------------------------------
data Blog =
  BlogName
  AND list of BlogPosts

data BlogName = all printable chars, maxlen = 100
*)

type BlogName = string // all printable chars, maxlen = 100
type BlogPost = undefined

type Blog = {
    BlogName : BlogName
    BlogPosts: BlogPost list
    }

// The tuple version might also be acceptable
type Blog2 = BlogName * BlogPost list

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

type TShirtColor = Black | Red | Blue
type TShirtSize = Large | Small
type TShirt = {
  Size: TShirtSize
  Color: TShirtColor
}

type HoodieColorSmall = Black | White
type HoodieColorLarge = Red | Blue
type Hoodie =
  | Large of HoodieColorLarge
  | Small of HoodieColorSmall


(*
------------------------------------------
data ExpressShippingType = OneDay OR TwoDay
data ShippingMethod =
  Standard
  OR Express (with ExpressShippingType)
*)

type ExpressShippingType = OneDay | TwoDay
type ShippingMethod =
  | Standard
  | Express of ExpressShippingType


(*
------------------------------------------
workflow PlayMove =
    inputs: Move AND GameState
    outputs: MoveResult AND (new)GameState
*)

type Move = undefined
type MoveResult = undefined
type GameState = undefined
type PlayMove = Move * GameState -> MoveResult * GameState
