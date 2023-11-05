﻿// ================================================
// Exercise: Model a card game
//
// IMPORTANT: This is NOT a "rec" module, so the types will need to be in order of declaration!
// ================================================

(*
The domain model is:

* A card is
  * a combination of a Suit (Heart, Spade) and a Rank (Two, Three, ... King, Ace)
  * OR a Joker
* A hand is a list of cards
* A deck is a list of cards
* A shuffled deck is also a list of cards, but different from a normal deck
* A player has a name and a hand
* A game consists of a deck and list of players

Actions:
* To deal, remove a card from a shuffled deck and put it on the table
* To pick up a card, add a card to a hand
* To shuffle, start with a normal deck and create a shuffled deck from it

Exercise:
* 1. Define types to represent the domain data and actions
* 2. (optional) Implement any functions that you can (look at the answer file if you get stuck)

*)


// ================================================
// Exercise 1: Model the domain
// ================================================


module CardGame =

    type Suit = ??
    type Rank = ??
    type Card = ??

    type Hand = ??
    type Deck = ??
    type ShuffledDeck = ??

    type Player = ??
    type Game = ??

    type Deal = ??
    type PickUp = ??
    type Shuffle = ??

    (*
    // Question: How do you document the rules of the game using types?
    // Answer: You can't -- you can only document the key concepts.
    //         For example, the algorithm used to shuffle cards or to score hands.
    /          Just add a comment such as "See algorithm.doc for details"
    *)


    (*
    // Question: How do you model extra behavior,
    //           such as calculating the scoring of a Hand?
    //
    //           In OO you would add methods to the Card and Hand
    // Answer: In FP we would do a "transform" to a new kind of thing,
    //         such as a CardScore type
    *)

    /// Aces High rule is used in Poker,
    /// or must be agreed at the beginning
    type AreAcesHigh = bool

    // You might want to score cards and hands.
    // For example Ace=13 Two=2
    type Score = int // no constraint -- can be large when adding up a hand

    // calculate a number for one single card
    type ScoreCard = Card * AreAcesHigh -> Score
    // calculate a number for a complete hand
    type ScoreHand = ??

// =====================================
// Exercise 2: Optional. Implement the Card Game actions
// =====================================


// uncomment this after you fix the errors above
(*
// helper function for shuffling implementation
module Util =

    // helper function for shuffling implementation
    let fisherYatesShuffle aList =
        let n = List.length aList
        let rand = System.Random()

        // convert to a mutable array! This an example of when mutation is OK because it's hidden
        let a = aList |> List.toArray

        // Fisher-Yates shuffle
        for i in [0..n-1] do
            // pick j, random integer such that i ≤ j < n
            let j = rand.Next(i,n)
            // exchange
            let x = a.[j]
            a.[j] <- a.[i]
            a.[i] <- x

        // convert back to an immutable list
        List.ofArray a
*)


// start implementation here ====>

module CardGameImplementation =
    open CardGame

    // optionally implement some functions here
    // uncomment them after you fix the errors above

    (*
    let pickup : PickUp =
        fun (card,hand) ->
            // the "::" operator prepends the card to the hand
            let newHand = card::hand
            newHand
    *)

    (*
    let deal : Deal =
        fun (ShuffledDeck deck) ->
            // the "::" pattern deconstructs a non-empty list into head/tail
            match deck with
            | first::rest -> first, ShuffledDeck rest
            | [] -> // what goes here?
    *)


    (*
    let shuffle : Shuffle =
        fun deck ->
            // use the utility function from above
            let shuffledCards = Util.fisherYatesShuffle ??
            ??
    *)

    (*
    let scoreCard : ScoreCard =
        fun (card,areAcesHigh) ->
            match card with
            | Ace -> if areAcesHigh then ??
            | King -> ??
    *)

    (*
    let scoreHand : ScoreHand =
        fun (hand,areAcesHigh) ->
            // use List.map to apply a function to each element of a list
            // use List.sum to add up a list
    *)
