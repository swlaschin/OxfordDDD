// ================================================
// DDD Exercise: Model a Contact management system
//
// IMPORTANT: This is NOT a "rec" module, so the types will need to be in order of declaration!
// ================================================

(*
REQUIREMENTS

The Contact management system stores Contacts

A Contact has
* a personal name
* an optional email address
* an optional postal address
* Rule: a contact must have an email or a postal address

A Personal Name consists of a first name, middle initial, last name
* Rule: the first name and last name are required
* Rule: the middle initial is optional
* Rule: the first name and last name must not be more than 50 chars
* Rule: the middle initial is exactly 1 char, if present

A postal address consists of four address fields plus a country

Rule: An Email Address can be verified or unverified

*)



// ----------------------------------------
// Helper module
// ----------------------------------------
module StringTypes =

    type String1 = String1 of string
    type String50 = String50 of string

// ----------------------------------------
// Main domain code
// ----------------------------------------

open StringTypes

// this is what we DON'T want to do!
type BadContactDesign = {

  FirstName: string
  MiddleInitial: string
  LastName: string

  EmailAddress: string
  IsEmailVerified: bool
  }



// ----------------------------------------
// Do you want a ConstrainedTypes module?
// ----------------------------------------
module ConstrainedTypes =

    type String1 =
    type String50 =
    type EmailAddress =

// ----------------------------------------
// and a VerificationService module?
// ----------------------------------------

module VerificationService =

    type VerifiedEmail = ??
    type VerificationService = ??

    // Optional if you want to do implementation of verification service
    (*
    Suggested logic:
        if hash = "OK" then verify, else don't verify
    *)
    let verificationService : VerificationService =
        failwith "not implemented"


// ----------------------------------------
// Main domain
// ----------------------------------------
module ContactDomain =

    type Contact = ?? // you take it from here!


module ContactImplementation =
    open ConstrainedTypes
    open VerificationService
    open ContactDomain

    // Optional if you want to do the implementation
    (*
    Suggested logic:
    1. extract email address from verified email
    2. print a message saying "sending password reset to %s" email address
    *)
    let sendPasswordReset : SendPasswordReset =
        failwith "not implemented"
