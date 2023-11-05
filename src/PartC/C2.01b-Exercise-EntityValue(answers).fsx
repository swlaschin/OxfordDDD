// =================================
// Entity/Value exercise
// =================================

type undefined = exn  // if you need it

(*
// ------------------------------------
0. Water bottles!
// ------------------------------------

1. How could you distinguish between a bottle I drank out of, and unopened ones?
2. What if I drank from multiple water bottles?
*)


(*
1. Put a label on it saying "Scott's water"
2. Put a label on them saying "Scott's water #1", "Scott's water #2" etc
   NOTE: A simple label won't help. You need to start using numbers or similar!

*)

(*
// ------------------------------------
1. PersonalName
// ------------------------------------

A Personal name has the following properties: {first, last}

1. Are these value objects or entities?
2. How would you model it as a type?
   (just a sketch -- don't spend too much time)
*)


// Answers:
// PersonalName is a Value
type PersonalName = {
    First: string
    Last: string
}


(*
// ------------------------------------
2. Address
// ------------------------------------

A UK street address has the following properties: {street, town, postcode}

1. Are these value objects or entities?
2. How would you model it as a type?
   (just a sketch -- don't spend too much time)
*)

// Answers:
// Address is a Value Object
type PostCode = undefined // for now! This should be a constrained type
type Address = {
    Street: string
    Town: string
    Postcode: PostCode
}

(*
// ------------------------------------
3. Contact
// ------------------------------------

A Contact has the following properties: {personalName, address}

1. Are these value objects or entities?
2. How would you model it as a type?
*)


// Answers:
// Contact is probably a Entity
// (although you could argue that contacts with the same data are interchangable)

type ContactId = undefined
type Contact = {
    Id : ContactId
    Name: PersonalName
    Address: PersonalName
}

(*
// ------------------------------------
4a. Tyre manufacture
// ------------------------------------

Tyres are manufactured in a plant
They have the following properties: size:int, description:string

1. Are these value objects or entities?
2. How would you model it as a type?
*)


// Answers:
// Tyre is a Value Object
type Tyre = {
    Size: int
    Description: string
}

(*
// ------------------------------------
4b. Tyres on a car
// ------------------------------------

Four tyres are fitted to a car.
They are supposed to be rotated between wheels to reduce wear.

1. Are these value objects or entities?
2. Do we care about the properties from the previous case?
3. How would you model it as a type?

*)


// Answers:
// 1. Tyre is a Entity Object. My tyre is not your tyre even if they were
// originally indistinguishable
// 2. At least the size and maybe the desciption

type CarId = undefined
type WheelLocation = FrontLeft | FrontRight | RearLeft | RearRight
type CarTyre = {
    Car: CarId
    WheelLocation : WheelLocation
    Size: int
    Description: string
}


(*
// ------------------------------------
4c. Tyres at a disposal plant
// ------------------------------------

Tyres are discarded to a disposal plant

1. Are these value objects or entities?
2. Do we care about the properties from the previous case?
3. How would you model it as a type?
*)


// Answers:
// 1. Tyre is a Value Object. The disposal place does not care
// 2. If they are going to shred it, they may not care about anything

// 3. I don't think they even track individual tyres,
//    so a structure for individual tyres might not even need to exist


(*
// ------------------------------------
5a. Laptop manufacture
// ------------------------------------

Laptops are manufactured in a plant and are given a unique serial number

1. Are these value objects or entities?
*)


// Answers:
// 1. Probably entities.
//    The factory might need to track the serial numbers for quality control.
//    How to find out the correct answer? Ask!

(*
// ------------------------------------
5b. Laptop purchase
// ------------------------------------

You go to buy a laptop at the store and see 10 of the same model, all with same spec

1. Are these value objects or entities?
*)


// Answers:
// 1. Value Objects
//    You don't care about the serial number when you buy it -- they are all the same to you

(*
// ------------------------------------
5c. Laptop post-purchase
// ------------------------------------

You have used a laptop for a while and replaced the battery and screen

1. Is this a value object or an entity?
2. The battery and screen are different -- is it the same laptop?
*)


// Answers:
// 1. Entity. Not interchangable with another one with same specs
// 2. Ship of Theseus! But from your point of view it's the same laptop
