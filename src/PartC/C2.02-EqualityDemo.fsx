// =================================
// Equality demo
// =================================

// utility method
let checkEquality obj1 obj2 =
    let isEqual = obj1 = obj2
    printfn $"{obj1} = {obj2} is {isEqual}"

let checkEqualityWithFn obj1 obj2 eq =
    let isEqual = eq obj1 obj2
    printfn $"{obj1} = {obj2} is {isEqual}"

// -------------------------------
// Structural equality
// -------------------------------

type EmailAddress = EmailAddress of string

let emailAddress1 = EmailAddress "me@example.com"
let emailAddress2 = EmailAddress "me@example.com"
let emailAddress3 = EmailAddress "you@example.com"

checkEquality emailAddress1 emailAddress2
checkEquality emailAddress1 emailAddress3



type PersonalName = {
    First: string
    Last: string
}
let name1 = {First="Alice"; Last="Adams"}
let name2 = {First="Alice"; Last="Adams"}
let name3 = {First="Bob"; Last="Adams"}

checkEquality name1 name2
checkEquality name1 name3

// -------------------------------
// Reference equality
// -------------------------------

// use a OO-style class this time
type NameClass(first,last) =
    member this.First = first
    member this.Last = last

let nameClass1 = NameClass("Alice","Adams")
let nameClass2 = NameClass("Alice","Adams")

checkEquality nameClass1 nameClass2   // False :(

// -------------------------------
// Identity equality for Entity records
// -------------------------------

type Contact = {
    ContactId : int
    EmailAddress: EmailAddress
}

// a custom equality checker
let contactEq con1 con2 =
    con1.ContactId = con2.ContactId

let contact1 = {ContactId=42; EmailAddress=EmailAddress "me@example.com"}
// Then they changed their email
let contact2 = {ContactId=42; EmailAddress=EmailAddress "me@earthlink.com"}

// without the custom tester
checkEquality contact1 contact2

// without the custom tester
checkEqualityWithFn contact1 contact2 contactEq



// -------------------------------
// Identity equality for Entity classes
// -------------------------------

type ContactClass(contactId:int,email:EmailAddress) =
    member this.ContactId = contactId
    member this.EmailAddress = email

    // New syntax! This overrides members from a parent class (Object in this case)
    override this.ToString() =
        sprintf "Contact(%i)" contactId
    override this.Equals(obj) =
        match obj with
        | :? ContactClass as c -> this.ContactId = c.ContactId
        | _ -> false
    override this.GetHashCode() =
        hash this.ContactId

let conClass1 = ContactClass(42,EmailAddress "me@example.com")
// Then they changed theie email
let conClass2 = ContactClass(42,EmailAddress "me@earthlink.com")

// this works
conClass1 = conClass2
// custom tester is now built-in to the class
checkEquality conClass1 conClass2

// -------------------------------
// QUESTION - why did we override GetHashCode as well?
// -------------------------------

// reason is that if they are equal, they should have the same hash!
let mySet = System.Collections.Generic.HashSet<ContactClass>()
mySet.Add(conClass1)
mySet.Add(conClass2)
mySet.Count  // should be 1
mySet |> List.ofSeq // print contents



// -------------------------------
// Forcing no equality for Entity records
// -------------------------------

// OK but wrong -- how can we stop this?
contact1 = contact2
checkEquality contact1 contact2



[<NoComparison; NoEquality>]
// --------------^  Don't allow equality on whole record
type Person = {
    PersonId : int
    EmailAddress: EmailAddress
}

let person1 = {PersonId=42; EmailAddress=EmailAddress "me@example.com"}
// Then they changed their email
let person2 = {PersonId=42; EmailAddress=EmailAddress "me@earthlink.com"}



// Not allowed
person1 = person2
checkEquality person1 person2

// OK
person1.PersonId = person2.PersonId


(*
I like this!

The benefit of the “NoEquality” approach is that it removes any ambiguity
about what equality means at the object level, and *forces* us to be explicit.
*)