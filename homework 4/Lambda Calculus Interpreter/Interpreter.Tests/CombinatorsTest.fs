namespace Tests

open NUnit.Framework
open FsUnit
open LambdaCalculus.Interpreter
open Samples.LambdaCombinators

[<TestFixture>]
type CombinatorsTestClass () =

    [<Test>]
    member this.``Statement that 'SKK is beta equal to I' should be proved`` () =
        let skk = s |@ k |@ k
        skk |> reduceToNormalForm |> should equal i

    [<Test>]
    member this.``Statement that 'S(KS)K is beta equal to B' should be proved`` () =
        let sksk = s |@ (k |@ s) |@ k
        sksk |> reduceToNormalForm |> areAlphaEquivalent b |> should be True

    [<Test>]
    member this.``Statement that 'II is beta equal to I' should be proved`` () =
        let ii = i |@ i
        ii |> reduceToNormalForm |> should equal i
      
    [<Test>]
    member this.``Statement that 'KI is beta equal to K with asterisk' should be proved`` () =
        let ki = k |@ i
        ki |> reduceToNormalForm |> areAlphaEquivalent k' |> should be True

    // Need to realize this functionality
    // [<Test>]
    // member this.``OmegaBig3 reduction should fail with stack overflow exception`` () =
    //     (fun () -> reduceToNormalForm omegaBig3) |> should throw typeof<System.StackOverflowException>

    [<Test>]
    member this.``(\xx.x)y 'shouuld be' \x.x`` () = 
        let term = ("x" ^/ "x" ^/ v"x") |@ v"y"
        term |> reduceToNormalForm |> should equal <| "x" ^/ v"x"

    