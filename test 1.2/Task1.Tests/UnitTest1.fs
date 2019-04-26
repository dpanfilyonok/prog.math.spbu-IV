namespace Tests

open NUnit.Framework
open FsUnit
open Task1.AlternatingSequence

[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.``First value should be 1`` () =
        alternatingNumberSequence |> Seq.take 1 |> Seq.toList |> should equal [1]

    [<Test>]
    member this.``Smoke test with first 6`` () =
        alternatingNumberSequence |> Seq.take 6 |> Seq.toList |> should equal [1; -2; 3; -4; 5; -6]

    [<Test>]
    member this.``Taking first 100_000 of seq should not fail`` () =
        alternatingNumberSequence |> Seq.take 100_000 |> ignore