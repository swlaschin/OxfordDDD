// =============================================
// Get a number *from* Roman numerals
// =============================================

(*

Design is the exact reverse of the previous approach
*)

/// Helper to convert the built-in .NET library method
/// to a pipeable function
let replace (oldValue:string) (newValue:string) (inputStr:string) =
    inputStr.Replace(oldValue, newValue)


// ======================================
// Exercise:
// ======================================

// Implement this logic using a piping model.
// Use the code below as a starting point

// Tip: use String.length to get the length of a string

let fromRomanNumerals input =
    input
    |> ?
    |> ?

// test it
fromRomanNumerals "IV"     // 4
fromRomanNumerals "CCXVIII"  // 218
fromRomanNumerals "XIX"    // 19
fromRomanNumerals "XLIX"   // 49
fromRomanNumerals "MXCIV"  // 1094
