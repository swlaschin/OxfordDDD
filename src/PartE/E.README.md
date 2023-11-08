# Part E: Designing for errors

1. Topics covered in this session
2. Extra material and references

## Topics covered in this session

### Errors

* Why domain errors should be first-class citizens in the design process.
* How to model and compose error-generating functions in an elegant way (using bind, map, etc).
* Practice: exercise in building a pipeline with error handling
* Kinds of errors (domain errors vs. panics vs. infrastructure errors) and different ways to handle them.

### Validation

* Validation (“error handling in parallel”).
* Practice: exercise in validating a data model

## Extra material and references

* [Railway Oriented Programming](https://www.youtube.com/watch?v=fYo3LN9Vf_M) (covers same topic as this session if you want to rewatch!)
  * Search for "Railway Oriented Programming" + your favourite programming language! There's a lot of stuff available.
* [FsToolkit.ErrorHandling](https://github.com/demystifyfp/FsToolkit.ErrorHandling) an error handling library for F# if you want to see more complex examples