// ======================================
// Crude in-memory implementation of an EventStore
// ======================================

// Using a class because we have private mutable state
// and we want encapsulation
type EventStore<'a>() =

    let events = ResizeArray<'a>()  // private mutable data

    /// Remove all events
    member this.Clear() =
        events.Clear()

    /// save an event to storage
    member this.SaveEvent event =
        events.Add event

    /// get all events
    member this.GetEvents() =
        events |> List.ofSeq

    /// for debugging
    member this.PrintEvents() =
        for e in events do
            e |> printfn "%A"

