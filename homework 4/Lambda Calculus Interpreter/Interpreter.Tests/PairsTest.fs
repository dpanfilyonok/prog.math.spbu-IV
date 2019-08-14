namespace Tests

open NUnit.Framework
open FsUnit
open LambdaCalculus.Interpreter
open Samples.LambdaPairs

[<TestFixture>]
type PairsTestClass () =

    [<Test>]
    member this.``fst (pair a b) 'should be' a`` () =
        let term = fst' |@ (pair |@ v"a" |@ v"b")
        term |> reduceToNormalForm |> should equal <| v"a"

    [<Test>]
    member this.``snd (pair a b) 'should be' b`` () =
        let term = snd' |@ (pair |@ v"a" |@ v"b")
        term |> reduceToNormalForm |> should equal <| v"b"
        