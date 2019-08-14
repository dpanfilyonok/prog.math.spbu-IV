namespace Tests

open NUnit.Framework
open FsUnit

open Tasks.SeqOfPrimes

[<TestFixture>]
type SeqOfPrimesTest () =

    [<Test>]
    member this.``empty seq`` () =
        primes |> Seq.take 0 |> Seq.toList |> should equal []

    [<Test>]
    member this.``first 10 primes be correct`` () =
        primes |> Seq.take 10 |> Seq.toList |> should equal [2;3;5;7;11;13;17;19;23;29]

    [<Test>]
    member this.``first prime should be 2`` () =
        primes |> Seq.take 1 |> Seq.toList |> should equal [2]

    [<Test>]
    member this.``-2 should not be prime`` () =
        -2 |> isPrime |> should be False