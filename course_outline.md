# Domain-Driven Design course

Domain-Driven Design (“DDD”) is a software design approach that focuses on the domain of the system rather than technology. There is an emphasis on building a shared mental model and representing that domain model in code in the simplest way possible. Technical details such as database storage, frameworks, etc., are considered to be secondary aspects of the design.

The module will focus on DDD and design in general and related topics, such as documentation and some aspects of software architecture.
This course uses basic functional programming concepts to express the domain and design: types as documentation, and functions (and composition of functions) for implementation. Object-oriented equivalents and OO design patterns may also be introduced when appropriate.

It is not intended to cover functional programming in any depth.  That is, this course is independent but complementary to the FRP course.

## Objectives

On completing the class, attendees will:

* Understand the principles of domain-driven design and how it differs from object-oriented design, database-driven design, etc.
* Understand when and why to use DDD instead of other approaches.
* Know how to interview domain experts to understand the key concepts and activities in a domain, with a strong emphasis on communication and developing shared mental models.
* Know how to document these concepts and activities using algebraic types and functions
* Know how to use types to capture as many details of the domain as possible, and how to “make illegal states unrepresentable” and reduce the need for unit tests.
* Know the principles of composition, applied to both functions and types, and how to compose an activity (modelled as a function) from smaller functions.
* Understand how to apply the DDD approach to error handling.
* Understand how to integrate non-pure elements into the design (I/O, databases, etc) and manage state within an application.
* Understand the DDD approach to partitioning a design (“bounded contexts”) and how a design might relate to software architecture.

----

## Requirements

The course will require basic programming skills, such as use of an editor, use of git, etc.
* No prior knowledge of DDD or functional programming is required – all concepts will be introduced as needed within the course.
* *However*, it is strongly recommended that you work through the [pre-study materials](prestudy.md) to make sure that you have some experience with the F# programming language before starting.

----

## Outline of the course

The course comprises 9 half-day sessions, delivered over five days.

### Part A: Understanding a domain

* Discussion of the software development process, and where requirements, analysis and design fit in.
* Why do so many software projects fail: bad requirements or bad code? Why is communication so important? How can we improve communication and feedback loops?
* What is domain-driven design and how does it differ from object-oriented design and database-driven design?
* How to interview domain experts and document a sketch of the design in real-time with rapid feedback.
* Use-cases vs. stories vs. workflows. Textual documentation vs. UML diagrams.
* Capturing functional vs. non-functional requirements.
* Practice: we will do group interviews and design sketching activities

### Part B: Modelling a domain

* Introduction to algebraic types and function types. Comparison of FP with OO equivalent (inheritance, interfaces).
* Using algebraic types and function types to document the nouns and verbs in the domain.
* Practice: We will convert a textual sketch to a typed domain
* Modelling workflows with functions and function composition. Comparison of FP with OO equivalent (request/response vs. input/output)
* Practice: Solving some simple challenges using function composition and pipelines

### Part C: Refining the domain

* Using types to capture constraints and why primitive types should be avoided
* Using optional types instead of null for missing values
* Using sum types to replace booleans and eliminate impossible states.
* Identity: entities vs. value objects.
* Refactoring towards greater insight: how domain specific types can lead to clearer designs, less defensive programming, and fewer potential errors
* Practice: exercises in the concepts above will be interspersed with the discussions

### Part D: Modelling state and time

* Modelling with simple state machines, and using types to represent the states and transitions
* Invariants: ensuring integrity and consistency in the domain.
* Commands and events.
* Event sourcing as a way to represent state over time ("Accountants don't use erasers").
* How to document temporal interactions in a domain model. Sequence diagrams and more.
* Practice: exercises in the concepts above will be interspersed with the discussions

### Part E: Designing for errors

* The importance of total functions and “honest” function signatures as part of a self documenting design.
* Why domain errors should be first-class citizens in the design process.
* How to model and compose error-generating functions in an elegant way (using bind, map, etc).
* Practice: exercise in building a pipeline with error handling
* Validation (“error handling in parallel”).
* Practice: exercise in validating a data model
* Kinds of errors (domain errors vs. panics vs. infrastructure errors) and different ways to handle them.

### Part F: Purity, I/O , and testing

* Database-centric vs. domain-centric architectures
* How to persist state within an application. Stateful vs. stateless designs.
* How to test a domain-driven design, and the importance of deterministic code to unit testing.
* How to move IO to the edges of the domain and keep the core design pure.
* Practice: refactoring a design to make it pure.
* Error recovery in conjunction with persistence: transactions, two phase commit, transactional outbox pattern, compensating actions. Working with immutable data and ledgers (“accountants don’t use erasers”).

### Part G: Domain-driven design and databases

* How to persist domain models to a database or document-store
* Command-query separation (“asking a question should not change the answer”) and CQRS
* Storing state machines in databases: single vs. multiple tables
* Time in databases: Snapshot storage vs. temporal db vs. event sourcing
* Reporting on a domain

### Part H: Partitioning and architecting a domain-driven design

* The importance of “Bounded Contexts” in DDD
* Context maps and other ways to communicate large domains. The politics of bounded contexts and the “reverse Conway manoeuvre.”
* How to communicate between bounded contexts. Public domain events vs. internal events. How to serialize domain objects to a wire-friendly format (JSON, etc).
* Practice: exercise in going from JSON to a domain model and back
* Various approaches to deploying a design (modular monolith, SOA/microservices, serverless). The C4 and UML approaches to documenting architecture.

### Part I: Other design approaches

We will briefly touch on some other design approaches

* User interface design and UX ("don't make me think")
* Test-driven design (TDD)
* Formal specification (e.g. TLA+)
* Designing for "Quality" and "Security"


### Part J: Putting it all together

* Review of all the techniques needed to understand, design and build a complete application.
* Practice: build a complete app including requirements gathering, design, implementation, error handling, and database storage.


----

## Assessment criteria

The assignment is intended to determine, in order of decreasing importance:

* Have you understood the key principles of domain driven design? Can you explain how DDD is different from other software design approaches?
* Have you demonstrated how to represent a domain using algebraic types that can be understood by non-technical domain experts and other stakeholders?
* Do you understand how to avoid primitives and how to represent simple constrained types, optional data, etc? Can you show how to refine a design to eliminate certain kinds of errors and the need for defensive programming?
* Do you understand the DDD approach to state management and databases?
* Do you understand to partition a design and plan an architecture around this?
