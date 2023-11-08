(*
=================================
Compensations and retries in Railway oriented programming
=================================
*)

// IMPORTANT - if you get errors such as
//   Could not load type 'ErrorMessage' from assembly 'FSI-ASSEMBLY,
// then try:
// 1. Reset the FSI interactive
// 2. Load code in small chunks

// Load a file with library functions for Result
#load "Result.fsx"  // TIP evaluate this import separately before evaluating the rest of the code below

// A library of utility functions for railway oriented programming
// which are not specific to this workflow and could be reused.
#load "RopUtil.fsx"


// =========================================
// The code from the previous example
// =========================================

type Request = {
    UserId: int
    Name: string
    Email: string
}

//--------------------------------
// Step 1 of the pipeline: validation

let nameNotBlank input =
  if input.Name = "" then
    Error "Name must not be blank"
  else
    Ok input

let name50 input =
  if input.Name.Length > 50 then
    Error "Name must not be longer than 50 chars"
  else
    Ok input

let emailNotBlank input =
  if input.Email = "" then
    Error "Email must not be blank"
  else
    Ok input

/// Combine all the smaller validation functions into one big one
let validateRequest input =
  input
  |> nameNotBlank
  |> Result.bind name50
  |> Result.bind emailNotBlank


//--------------------------------
// Step 2 of the pipeline: lowercasing the email

// trim spaces and lowercase
let canonicalizeEmail (input:Request) =
   { input with Email = input.Email.Trim().ToLower() }

let canonicalizeEmailR twoTrackInput =
  twoTrackInput |> Result.map canonicalizeEmail

//--------------------------------
// Step 3 of the pipeline: Update the database

let updateDb (request:Request) =
    // do something
    // return nothing at all
    printfn "Database updated with userId=%i email=%s" request.UserId request.Email
    ()

let updateDbR twoTrackInput =
  twoTrackInput
  |> Result.map (RopUtil.tee updateDb)


//--------------------------------
// Step 4 of the pipeline: Send an email

let sendEmail (request:Request) =
    if request.Email.EndsWith("example.com") then
        failwithf "Can't send email to %s" request.Email
    else
        printfn "Sending email=%s" request.Email
        request // return request for processing by next step

let sendEmailR twoTrackInput =
    // define a handler for exceptions
    let exnConverter (ex:exn) = ex.Message
    // convert the exception-throwing "sendEmail"
    // into something useful.
    RopUtil.catchR sendEmail exnConverter twoTrackInput


//--------------------------------
// Step 5 of the pipeline: Log the errors

let loggerR twoTrackInput =
    match twoTrackInput with
    | Ok (req:Request) ->
        printfn "LOG INFO Name=%s EMail=%s" req.Name req.Email
    | Error err ->
        printfn "LOG ERROR %s" err
    twoTrackInput // return same input for use in the next step of the pipeline


//--------------------------------
// Last step of the pipeline: return the response

let returnMessageR result =
    match result with
    | Ok obj ->
        sprintf "200 %A" obj
    | Error msg ->
        sprintf "400 %s" msg



(*
=========================================
Part A -- Working inside database transactions
=========================================

Let's say that you want to do part of the pipeline within a database
transaction.

Define a helper function that takes in a pipeline and runs it in a transaction
*)

type Pipeline = Result<Request, string> -> Result<Request, string>

let updateDbWithTransaction (pipeline:Pipeline) : Pipeline =
   fun reqOrError ->
      let result = pipeline reqOrError
      match result with
      | Ok data ->
         printfn "Commit Database Transaction"
      | Error e ->
         printfn "Abort Database Transaction"
      result


// =========================================
// Test
// =========================================

let goodRequest = {
  UserId=0
  Name= "Alice"
  Email="   ABC@gmail.COM   "   // note: this has spaces and some uppercase
}

let unsendableRequest = {
  UserId=0
  Name= "Alice"
  Email="ABC@example.COM"
}

unsendableRequest
|> validateRequest
|> canonicalizeEmailR
|> updateDbWithTransaction (fun reqOrError ->
    reqOrError
    |> updateDbR
    |> sendEmailR
    |> loggerR
    )
|> returnMessageR


(*
=========================================
Part B -- Working with retries
=========================================

Let's say that you want to repeat a section of the pipeline in case of error

Define a helper function that takes in a pipeline and runs N number of times
*)

let rec repeatN n (pipeline:Pipeline) : Pipeline =
   fun reqOrError ->
      let result = pipeline reqOrError
      match result with
      | Ok data ->
         printfn "Succeeded"
         result
      | Error e ->
         let triesLeft = n - 1
         if triesLeft = 0 then
            // give up
            printfn "Giving up"
            result
         else
            // retry
            printfn $"Failed. {triesLeft} tries left"
            repeatN triesLeft pipeline reqOrError


unsendableRequest
|> validateRequest
|> canonicalizeEmailR
|> repeatN 3 (fun reqOrError ->
    reqOrError
    |> sendEmailR
    |> loggerR
    )
|> returnMessageR


(*
=========================================
Part C -- Working with compensations
=========================================

Let's say that you want to undo a action if there is a failure later

*)

let compensateWith compensatingAction : Pipeline =
  fun reqOrError ->
      match reqOrError with
      | Ok data ->
        // do nothing
        ()
      | Error err ->
        compensatingAction err
      reqOrError


let compensatingActionForEmailError (err:string) =
  if err.StartsWith("Can't send email") then
      printfn "Writing compensation to database after email failure"

unsendableRequest
|> validateRequest
|> canonicalizeEmailR
|> updateDbR
|> sendEmailR
|> loggerR
|> compensateWith compensatingActionForEmailError
|> returnMessageR
