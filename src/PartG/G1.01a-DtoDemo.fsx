// ================================================
// Demo: Convert a domain object to and from a DTO object
// ================================================

open System

// Load a file with validation functions
#load "Validation.fsx"

//===========================================
// Part 1 - The domain
//===========================================

module Domain =

    // String10 must be not empty AND len < 10
    type String10 =  private String10 of string

    type PersonalName = {
        First: String10
        Last: String10
        }

    module String10 =
        // pass in a field name so that we know which field had the error
        let create fieldName s =
            if System.String.IsNullOrEmpty(s) then
                Error (sprintf "%s is null or empty" fieldName)
            else if (s.Length > 10) then
                Error (sprintf "%s is too long" fieldName)
            else
                Ok (String10 s)

        let value (String10 s) = s



//===========================================
// Part 2 - Define the DTO
//===========================================


/// This is what will be serialized as JSON
type PersonalNameDto = {
    first: string
    last: string
    }

//===========================================
// Validation logic
//===========================================

// bring the domain into scope
open Domain

/// Convert a PersonalName into a DTO for outputing as JSON.
/// This always succeeds.
let toDto (domainObj:PersonalName) :PersonalNameDto =
    {
    first = domainObj.First |> String10.value
    last = domainObj.Last |> String10.value
    }


/// Create a constructor for PersonalName to be used by validation
let createName first last :PersonalName =
    {First=first; Last=last}

/// Create a PersonalName from a DTO deserialized from JSON.
/// This might fail if the DTO is invalid
let fromDto (dto:PersonalNameDto) :Validation<PersonalName,string> =

    // validate each field from the DTO
    let firstOrError =
        dto.first
        |> String10.create "first name"
        |> Validation.ofResult
    let lastOrError =
        dto.last
        |> String10.create "last name"
        |> Validation.ofResult

    // use map2 here
    let nameOrError =
        Validation.map2 createName firstOrError lastOrError

    nameOrError// return

// -------------------------------
// test these functions with some example data
// -------------------------------

let goodDto = {
    first = "Alice"
    last = "Adams"
    }

let badDto = {
    first = "Jean-Claude"
    last = ""
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


let toJsonDto (name:PersonalName) :string =
    name
    |> toDto
    |> serializeJson

let fromJsonDto (json:string) =
    json
    |> deserializeJson
    |> fromDto

// test with some JSON
let goodJson  = """{"first":"Alice","last":"Adams"}"""
let badJson  = """{"first":"12345678901","last":""}"""

goodJson |> fromJsonDto
badJson |> fromJsonDto

goodDto |> serializeJson
badDto |> serializeJson

// -------------------------------
// a web API
// -------------------------------

let myCoreWorkflow (domainObject:PersonalName) =
    // The core workflow does something with the domain object
    // It does not know about IO or JSON

    // get the data out of result that you know is good
    // (never do this in production!)
    let resultGet result =
        match result with
        | Ok data -> data
        | Error _ -> failwith "this should never happen"

    // lets just upper case it :)
    let newFirst =
        domainObject.First
        |> String10.value
        |> (fun str -> str.ToUpper() )
        |> String10.create "first"
        |> resultGet

    let newLast =
        domainObject.Last
        |> String10.value
        |> (fun str -> str.ToUpper() )
        |> String10.create "last"
        |> resultGet

    let newName : PersonalName = {
        First = newFirst
        Last = newLast
        }

    newName


// this is a complete web api workflow
let webApi (json:string) =
    json
    |> deserializeJson
    |> fromDto
    |> Result.map // handle ok/error branch
        (fun validDomainObject ->
            myCoreWorkflow validDomainObject
            |> toJsonDto
        )
    |> fun result ->
        match result with
        | Ok json ->
            printfn $"[200] {json}"
        | Error errs ->
            printfn $"[400] {errs}"


goodJson |> webApi
badJson |> webApi