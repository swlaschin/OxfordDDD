# Part F: Purity, I/O , and testing

1. Topics covered in this session
2. Extra material and references

## Topics covered in this session


* Why purity and hiding I/O is important
* Why you should move IO to the edges of the domain and keep the core design pure.
* Dependency rejection: pure business logic that outputs a decision
* Dependency injection: passing a function for the I/O
* Interpreter pattern to separate instructions from implementation

## Extra material and references

* My blog post goes into this whole topic in more detail.
  * [Link](https://fsharpforfunandprofit.com/posts/dependencies/)
* My "13 ways of looking at a turtle also includes many of these ideas"
  * [Blog post](https://fsharpforfunandprofit.com/posts/13-ways-of-looking-at-a-turtle/)
  * [Talk](https://www.youtube.com/watch?v=AG3KuqDbmhM)
* Dependency Rejection:
  * This term was coined by Mark Seemann.
    He has written blogs and talks.
    * [Here's one](https://www.youtube.com/watch?v=oJYRXVl6LWc)
* Dependency Injection with partial application:
  * [Blog post](https://mcode.it/blog/2020-12-11-fsharp_composition_root/)
* Interpreters:
  * Lots of material on OO-style interpreter pattern
  * There's not much for FP-style interpreter pattern. I like [this talk](https://www.youtube.com/watch?v=hmX2s3pe_qk) -- it uses Scala!