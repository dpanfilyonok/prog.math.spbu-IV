namespace Tests

open NUnit.Framework
open FsUnit
open LambdaCalculus.Interpreter
open LambdaCombinators

[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.``Statement that 'SKK is beta equal to I' should be proved`` () =
        let skk = s |@ k |@ k
        reduceToNormalForm skk |> should equal i

    [<Test>]
    member this.``Statement that 'S(KS)K is beta equal to B' should be proved`` () =
        let sksk = s |@ (k |@ s) |@ k
        reduceToNormalForm sksk |> areAlphaEquivalent b |> should be True

    [<Test>]
    member this.``Statement that 'II is beta equal to I' should be proved`` () =
        let ii = i |@ i
        reduceToNormalForm ii |> should equal i
      
    [<Test>]
    member this.``Statement that 'KI is beta equal to K with asterisk' should be proved`` () =
        let ki = k |@ i
        reduceToNormalForm ki |> areAlphaEquivalent k' |> should be True

    [<Test>]
    member this.``Statement that ssssssssual to K with asterisk' should be proved`` () =
        let term = ("x" ^/ "x" ^/ v"x") |@ v"y"
        reduceToNormalForm term |> should equal <| "x" ^/ v"x"

    // проверить комбинатор неподвижной точки