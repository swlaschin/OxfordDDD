﻿// ================================================
// Exercise: Convert a domain object to and from a DTO object
// ================================================

(*

There is a type Person with
* property Name of type PersonalName
* property Age of type Age
* property Email of type Email
See the file "PersonDomain.fsx" for definitions

Exercise:
1) create a function "toDto" that converts a Person into a DTO
2) create a function "fromDto" that converts a DTO into Person

*)

open System

// Load a file with validation functions
#load "Validation.fsx"

// Import the definition of the Person domain.
// PRO TIP: Open it so you can see what you need to work with!
#load "PersonDomain.fsx"
open PersonDomain.Domain


//===========================================
// A DTO to validate
//===========================================


/// This is what will be serialized as JSON
type PersonDto = {
    first: string
    last: string
    age: int
    email: string
    }

//===========================================
// Validation logic
//===========================================

/// Create a constructor for PersonalName
let createName first last :PersonalName =
    {First=first; Last=last}

/// Create a constructor for Person
let createPerson name age email :Person =
    {Name=name; Age=age; Email=email}

/// Exercise: Convert a person into a DTO for outputing as JSON.
/// This always succeeds.
let toDto (person:Person) :PersonDto =
    {
    first = String10.value person.Name.First
    last = String10.value person.Name.Last
    age = Age.value person.Age
    email = Email.value person.Email

    // getting the value could also be written using piping
    // first = person.Name.First |> String10.value
    // last = person.Name.Last |> String10.value
    // etc

    }

/// Exercise: Create a person from a DTO.
/// This might fail if the DTO is invalid
let fromDto (personDto:PersonDto) :Validation<Person,string> =
    let firstOrError =
        personDto.first
        |> String10.create "first name"
        |> Validation.ofResult
    let lastOrError =
        personDto.last
        |> String10.create "last name"
        |> Validation.ofResult
    let ageOrError =
        personDto.age
        |> Age.create
        |> Validation.ofResult
    let emailOrError =
        personDto.email
        |> Email.create
        |> Validation.ofResult

    let nameOrError =
        (Validation.map2 createName) firstOrError lastOrError
    let personOrError =
        (Validation.map3 createPerson) nameOrError ageOrError emailOrError

    personOrError // return


// -------------------------------
// test these functions with some example data
// -------------------------------

let goodDto = {
    first = "Alice"
    last = "Adams"
    age = 1
    email = "x@example.com"
    }

let badDto = {
    first = ""
    last = "Adams"
    age = 1000
    email = "xexample.com"
    }

// check that validation happens when creating a domain object from a DTO
goodDto |> fromDto
badDto |> fromDto

// roundtrip a DTO to a domain object and back
let roundTrip dto =
    let domainObjOrError = fromDto dto
    match  domainObjOrError with
    | Ok domainObj -> Ok (toDto domainObj)
    | Error e -> Error e

// check the round trip logic
goodDto |> roundTrip
badDto |> roundTrip




// -------------------------------
// full JSON example
// -------------------------------

#r "System.Text.Json"
open System.Text.Json
let serializeJson = JsonSerializer.Serialize
let deserializeJson<'a> (str:string) = JsonSerializer.Deserialize<'a>(str)


let toJsonDto (person:Person) :string =
    person
    |> toDto
    |> serializeJson

let fromJsonDto (json:string) =
    json
    |> deserializeJson
    |> fromDto

// test with some JSON
let goodJson  = """{"first":"Alice","last":"Adams","age":20,"email":"abc@gmail.com"}"""
let badJson  = """{"first":"12345678901","last":"","age":-1,"email":"gmail.com"}"""

goodJson |> fromJsonDto
badJson |> fromJsonDto

goodDto |> serializeJson
badDto |> serializeJson