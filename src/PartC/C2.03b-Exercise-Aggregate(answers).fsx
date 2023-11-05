// =================================
// Aggregrate exercise
// =================================

(*
// ------------------------------------
1. LaptopSpec
// ------------------------------------

A LaptopSpec contains a number of components such as a CPU, memory, etc

1. Do the LaptopSpec + components form an Aggregate?
2. Is the LaptopSpec an Aggregate root?

*)

// Answers:
// 1. Yes
// 2. Yes. If you change the CPU, you have also changed the LaptopSpec

(*
// ------------------------------------
2. Course
// ------------------------------------

A Course contains a number of student Enrolments

1. Do the Course + Enrolments form an Aggregate?
2. Is the Course an Aggregate root?

*)

// Answers:
// 1. No.
// 2. No. E.g. You could be able to change the grade of an Enrolment without changing the Course


(*
// ------------------------------------
3. List
// ------------------------------------

A generic List collection contains a list of entities

1. Does the List + list items form an Aggregate?
2. Is the List an Aggregate root?

*)

// Answers:
// 1. No. The list items are not part of the list in a domain sense
// 2. No. You can change an enity in the list without going through the list


(*
// ------------------------------------
4. Order
// ------------------------------------

An Order has a Customer associated with it
A Customer has a list of Orders associated with it

1. Are either of these Aggregates
2. How can we store a Customer inside an Order without causing problems?

*)

// Answers:
// 1. No
// 2. Just don't do that. Store a "reference" to a Customer instead!