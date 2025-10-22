// =============================================
// Some different implementations of converting a number to Roman numerals
// =============================================

// -------------------------------
// My version
// -------------------------------

module StringReplacementVersion =

    let replace (oldValue:string) (newValue:string) (inputStr:string) =
        inputStr.Replace(oldValue, newValue)

    let toRomanNumerals number =
        String.replicate number "I"
        |> replace "IIIII" "V"
        |> replace "VV" "X"
        |> replace "XXXXX" "L"
        |> replace "LL" "C"
        |> replace "CCCCC" "D"
        |> replace "DD" "M"
        // TIP: additional special forms should be
        // done highest to lowest
        |> replace "DCCCC" "CM"
        |> replace "CCCC" "CD"
        |> replace "LXXXX" "XC"
        |> replace "XXXX" "XL"
        |> replace "VIIII" "IX"
        |> replace "IIII" "IV"

// test it
StringReplacementVersion.toRomanNumerals 1
StringReplacementVersion.toRomanNumerals 19
StringReplacementVersion.toRomanNumerals 194
StringReplacementVersion.toRomanNumerals 1947

// -------------------------------
// Javascript version, converted to F#
// from https://stackoverflow.com/a/9083076/1136133
// -------------------------------

module JavascriptVersion =

    let toRomanNumerals num =
        let mutable digits = num |> string |> List.ofSeq |> List.map (string >> int) |> List.rev
        let key =
            [
            "";"I";"II";"III";"IV";"V";"VI";"VII";"VIII";"IX"
            "";"X";"XX";"XXX";"XL";"L";"LX";"LXX";"LXXX";"XC";
            "";"C";"CC";"CCC";"CD";"D";"DC";"DCC";"DCCC";"CM";
            ""; "M"; "MM"; "MMM"; "MMMM"
            ]
        let mutable roman = ""
        let mutable i = 0

        while digits.IsEmpty |> not do
            match digits with
            | first::rest ->
                roman <- key[first + (i * 10)] + roman
                digits <- rest // pop
                i <- i + 1
            | [] ->
                // no more digits
                ()
        roman

JavascriptVersion.toRomanNumerals 1
JavascriptVersion.toRomanNumerals 19
JavascriptVersion.toRomanNumerals 105
JavascriptVersion.toRomanNumerals 194
JavascriptVersion.toRomanNumerals 1005
JavascriptVersion.toRomanNumerals 1947

// -------------------------------
// Java version, converted to F#
// from https://stackoverflow.com/questions/12967896/converting-integers-to-roman-numerals-java
// -------------------------------

module JavaVersion =

    let toRomanNumerals num =
        if (num  < 1 || num > 3999) then
            failwith "Invalid Roman Number Value"

        let mutable s = "";
        let mutable input = num;

        while (input >= 1000) do
            s <- s + "M"
            input <- input - 1000
        while (input >= 900) do
            s <- s + "CM"
            input <- input - 900
        while (input >= 500) do
            s <- s + "D"
            input <- input - 500
        while (input >= 400) do
            s <- s + "CD"
            input <- input - 400
        while (input >= 100) do
            s <- s + "C"
            input <- input - 100
        while (input >= 90) do
            s <- s + "XC"
            input <- input - 90
        while (input >= 50) do
            s <- s + "L"
            input <- input - 50
        while (input >= 40) do
            s <- s + "XL"
            input <- input - 40
        while (input >= 10) do
            s <- s + "X"
            input <- input - 10
        while (input >= 9) do
            s <- s + "IX"
            input <- input - 9
        while (input >= 5) do
            s <- s + "V"
            input <- input - 5
        while (input >= 4) do
            s <- s + "IV"
            input <- input - 4
        while (input >= 1) do
            s <- s + "I"
            input <- input - 1
        // final result
        s

JavaVersion.toRomanNumerals 1
JavaVersion.toRomanNumerals 19
JavaVersion.toRomanNumerals 105
JavaVersion.toRomanNumerals 194
JavaVersion.toRomanNumerals 1005
JavaVersion.toRomanNumerals 1947

// -------------------------------
// Haskell version, converted to F#
// and https://web.archive.org/web/20040119181605/http://www.cs.york.ac.uk/ftpdir/pub/haskell/contrib/Roman.hs
// referenced by https://wiki.haskell.org/Roman_numerals
// -------------------------------

(*
module HaskellVersion =

    let toRomanNumerals =
        // Each numeral has a decimal value.
        let numerals = Seq.zip "IVXLCDM" [1;5;10;50;100;500;1000] |> Map.ofSeq

        // For each numeral, there is a single permitted prefix digit for subtraction.
        let subnums = [ ('V','I'); ('X','I');  ('L','X'); ('C','X'); ('D','C'); ('M','C') ] |> Map.ofSeq

        // Each numeral could potentially appear many times (case 1), and we must
        // also handle (case 2) where a numeral *nearly* fits so we use a subtractive prefix.
        let rec toNumeral (rdigit, b) (n,s) =
            let tdigit = Map.tryFind rdigit subnums |> Option.defaultValue ' '
            let k = List.tryFind tdigit numerals |> Option.defaultValue 0

            if n >= b then toNumeral st (n - b, rdigit::s)
            elif n + k >= b then (n - b + k, rdigit::tdigit::s)
            else (n,s)


        // Traverse the numeral list with an accumulator consisting of the
        // string built so far (in reverse order) and the remaining value to be converted.
        let toRoman n  = (reverse . snd) (List.fold toNumeral (n,"") numerals)

HaskellVersion.toRomanNumerals 1
HaskellVersion.toRomanNumerals 19
HaskellVersion.toRomanNumerals 105
HaskellVersion.toRomanNumerals 194
HaskellVersion.toRomanNumerals 1005
HaskellVersion.toRomanNumerals 1947

*)