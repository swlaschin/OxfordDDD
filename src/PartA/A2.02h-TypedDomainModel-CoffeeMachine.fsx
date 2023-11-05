// define a domain model for a your own domain
module rec CoffeeMachine =


// ---->^  The "rec" means the definitions in this file can be out of order
// In this case, you must highlight the entire file to evalulate the code


(*
Need help with syntax?
See A2.03f-TypedDomainModel-Help.fsx for syntax you might need
*)


//============================================
// Your code starts here
// Indent everything one tab so that the "module rec" above works
//============================================

    // TIP define a "undefined" type to use when you don't know the answer yet
    type undefined = exn

    type MyWorkflow = MyInputData -> MyOutputData

    type MyInputData = undefined
