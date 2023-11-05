// ================================================
// State machine exercises:
//
// For a typical state machine diagram, see
//   D1.01a-StateMachine(diagram).png
//
// Here is a general template for doing these.
//
// 1. Create a type for each state's data
// 2. Create a choice covering all states
//    Each choice contains the data for that state
// 3. Create "API" functions representing each transition
//    * The input to the function is a specific state
//    * The output of the function is the union/choice type
//
// See D1.01c-StateMachineTemplate.fsx for a complete template for API and Client
//
// ================================================

type undefined = exn

// -----------------------------------------------
// Example: Unverified and Verified email
// -----------------------------------------------

// 1. Create a type for each state's data
type UnverifiedEmail = UnverifiedEmail of string
type VerifiedEmail = VerifiedEmail of string

// 2. Create a choice covering all states
//    Each choice contains the data for that state
type Email =
    | Unverified of UnverifiedEmail
    | Verified of VerifiedEmail


// 3. Create "API" functions representing each transition
type Verify = UnverifiedEmail -> Email

// See D1.01d-EmailStateTransition.fsx for a complete example

// -----------------------------------------------
// Example: Chess
// -----------------------------------------------

// 1. Create a type for each state's data
type WhitePlayer = WhitePlayer of string
type BlackPlayer = BlackPlayer of string  // don't want to mix them up


// 2. Create a choice covering all states
//    Each choice contains the data for that state
type ChessState =
    | WhiteToPlay of WhitePlayer
    | BlackToPlay of BlackPlayer
    | GaveOver

// 3. Create "API" functions representing each transition
type PlayWhite = WhitePlayer -> ChessState
type PlayBlack = BlackPlayer -> ChessState

(*
Questions?

Q: Why return the entire state?
      type PlayWhite = WhitePlayer -> ChessState
                                       ^-- the entire state

A: Because the function may return any of the states.
   For example, the chess play may transition to BlackToPlay OR GameOver

      type PlayWhite = WhitePlayer -> BlackToPlay
                                       ^-- wrong output! not always
      type PlayWhite = WhitePlayer -> GameOver
                                       ^-- wrong output! not always

-------------------------

Q: Why pass in one case as input rather than the entire union?
     type PlayWhite = ChessState -> ChessState
                        ^----- pass the entire state

A: Because the function may not be valid to be called in another state.
   Passing in the data for a particular state is "proof" that you are in that state.

     type PlayWhite = WhitePlayer -> ChessState
                        ^----- proof that you are in the white player state

     type PlayWhite = ChessState -> ChessState
                        ^----- could be in any state
                               need to return an error or ignore if you call from the wrong state

-------------------------

Q: What happens if the number of states is too big?

A: Use a different technique! E.g. use a lookup table.

*)