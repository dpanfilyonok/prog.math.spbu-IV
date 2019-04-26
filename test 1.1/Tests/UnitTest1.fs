namespace Tests

open NUnit.Framework
open FsUnit
open Tasks.MyImmutableQueue

[<TestFixture>]
type TestOfMyImmutableQueue () =

    [<Test>]
    member this.``Enqueue to empty que`` () =
        let emptyQueue = Empty
        let expected = ImmutableQueue(1, Empty)
        emptyQueue.Enqueue 1 |> should equal expected

    [<Test>]
    member this.``Enqueue to 1 element  que`` () =
        let queue = ImmutableQueue(1, Empty)
        let expected = ImmutableQueue(1, ImmutableQueue(2, Empty))
        queue.Enqueue 2 |> should equal expected

    [<Test>]
    member this.``Enqueue to 2 element que`` () =
        let queue = ImmutableQueue(1, ImmutableQueue(2, Empty))
        let expected = ImmutableQueue(1, ImmutableQueue(2, ImmutableQueue(3, Empty)))
        queue.Enqueue 3 |> should equal expected

    [<Test>]
    member this.``First test`` () =
        let queue = ImmutableQueue(1, ImmutableQueue(2, Empty))
        queue.First |> should equal 1

    [<Test>]
    member this.``Dequeue from empty stack should raise exception`` () =
        let queue = Empty
        (fun () -> queue.Dequeue |> ignore) |> should throw typeof<System.Exception>
