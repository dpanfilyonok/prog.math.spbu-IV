namespace Tests

open NUnit.Framework
open FsUnit
open LambdaCalculus.Interpreter
open Samples.LambdaBooleans

[<TestFixture>]
type BooleansTestClass () =

    [<Test>]
    member this.``iif tru a b 'should be' a`` () =
        let term = iif |@ tru |@ v"a" |@ v"b"
        term |> reduceToNormalForm |> should equal <| v"a"

    [<Test>]
    member this.``iif fls a b 'should be' b`` () =
        let term = iif |@ fls |@ v"a" |@ v"b"
        term |> reduceToNormalForm |> should equal <| v"b"

    [<Test>]
    member this.``and tru w 'should be' w`` () =
        let term = and' |@ tru |@ v"w"
        term |> reduceToNormalForm |> should equal <| v"w"

    [<Test>]
    member this.``and fls w 'should be' fls`` () =
        let term = and' |@ fls |@ v"w"
        term |> reduceToNormalForm |> should equal <| fls

    [<Test>]
    member this.``or tru w 'should be' tru`` () =
        let term = or' |@ tru |@ v"w"
        term |> reduceToNormalForm |> should equal <| tru

    [<Test>]
    member this.``or fls w 'should be' w`` () =
        let term = or' |@ fls |@ v"w"
        term |> reduceToNormalForm |> should equal <| v"w"

    [<Test>]
    member this.``not tru 'should be' fls`` () =
        let term = not' |@ tru
        term |> reduceToNormalForm |> should equal <| fls