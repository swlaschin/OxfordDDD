// Demonstrates the TicTacToe domain written as code
module rec TicTacToe =

// ---->^  The "rec" means the definitions in this file can be out of order
// In this case, you must highlight the entire file to evalulate the code


(*
Need help with syntax?
See A2.02a-TypedDomainModel-Help.fsx for the syntax you might need
*)


//============================================
// Your code starts here
// Indent everything one tab so that the "module rec" above works
//============================================


    // 1. The "PlayMove" workflow
    // The input is a pair: information from outside + game state loaded from storage
    //     This could be refactored to a single record with two fields.
    // The output is a pair: information to return to outside + game state to save to storage
    type PlayMove =
        // everything on this side is input
        MoveInformation * GameState -> MoveResult * GameState
        //                             everything on this side is output

    // you might also expose a version where the internal state is hidden
    type PlayMoveWebAPI =
        MoveInformation -> MoveResult


    // 2. MoveInformation is modeled as Player AND Position

    // MoveInformation could be implemented as a tuple
    (*
    type MoveInformation = Player * Position
    *)

    // Or, MoveInformation could be implemented as a record
    type MoveInformation = {
        Player : Player
        Position : Position
        }

    // 3. Player is modeled as X OR O
    (*
    type Player =
        | X
        | O
    *)
    type Player = X | O  // shorter version of above, like an enum

    // 4. Position is modeled as Row AND Column

    // Position could be implemented as a tuple
    (*
    type Position = Row * Column
    *)
    // Or, Position could be implemented as a record
    type Position = {
        Row: Row
        Column : Column
        }

    // 5. Row and Column are simple type with constraints
    type Row = int // between 1..3
    type Column = int // between 1..3

    // 6. MoveResult is Win OR Draw OR NextTurn
    type MoveResult =
        | Win of Player
        | Draw
        | NextTurn of Player

    // 7. The game state is the state of all the cells
    type GameState = CellState list

    type CellState =
        Position * Player option

