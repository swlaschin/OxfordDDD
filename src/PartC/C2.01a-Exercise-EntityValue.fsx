// =================================
// Entity/Value exercise
// =================================

type undefined = exn  // if you need it

(*
// ------------------------------------
0. Water bottles!
// ------------------------------------

1. How could you distinguish between a bottle I drank out of, and unopened ones?
2. What if I drank from multiple water bottles? How would you distinguish them?
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



(*
// ------------------------------------
2. Address
// ------------------------------------

A UK street address has the following properties: {street, town, postcode}

1. Are these value objects or entities?
2. How would you model it as a type?
   (just a sketch -- don't spend too much time)
*)


(*
// ------------------------------------
3. Contact
// ------------------------------------

A Contact has the following properties: {personalName, address}

1. Are these value objects or entities?
2. How would you model it as a type?
*)


(*
// ------------------------------------
4a. Tyre manufacture
// ------------------------------------

Tyres are manufactured in a plant
They have the following properties: size:int, description:string

1. Are these value objects or entities?
2. How would you model it as a type?
*)


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



(*
// ------------------------------------
4c. Tyres at a disposal plant
// ------------------------------------

Tyres are discarded to a disposal plant

1. Are these value objects or entities?
2. Do we care about the properties from the previous case?
3. How would you model it as a type?
*)



(*
// ------------------------------------
5a. Laptop manufacture
// ------------------------------------

Laptops are manufactured in a plant and are given a unique serial number

1. Are these value objects or entities?
*)

(*
// ------------------------------------
5b. Laptop purchase
// ------------------------------------

You go to buy a laptop at the store and see 10 of the same model, all with same spec

1. Are these value objects or entities?
*)


(*
// ------------------------------------
5c. Laptop post-purchase
// ------------------------------------

You have used a laptop for a while and replaced the battery and screen

1. Is this a value object or an entity?
2. The battery and screen are different -- is it the same laptop?
*)

