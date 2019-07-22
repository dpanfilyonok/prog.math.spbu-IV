namespace Tests

open NUnit.Framework
open FsUnit
open LambdaCalculus.Interpreter
open Samples.LambdaBooleans
open Samples.LambdaArithmetic
open Samples.LambdaCombinators

[<TestFixture>]
type ArithmeticTestClass () =

    [<Test>]
    member this.``iszro zero 'should be' tru`` () =
        let term = iszro |@ zero 
        term |> reduceToNormalForm |> should equal <| tru

    [<Test>]
    member this.``iszro one 'should be' fls`` () =
        let term = iszro |@ one 
        term |> reduceToNormalForm |> should equal <| fls

    [<Test>]
    member this.``plus one zero 'should be' one`` () =
        let term = plus |@ one |@ zero
        term |> reduceToNormalForm |> should equal <| one

    [<Test>]
    member this.``plus one one 'should be' two`` () =
        let term = plus |@ one |@ one
        term |> reduceToNormalForm |> should equal <| two

    [<Test>]
    member this.``mult one zero 'should be' zero`` () =
        let term = mult |@ one |@ zero
        term |> reduceToNormalForm |> should equal <| zero

    [<Test>]
    member this.``pow one two 'should be' one`` () =
        let term = pow |@ one |@ two
        term |> reduceToNormalForm |> areAlphaEquivalent one |> should be True

    // [<Test>]
    // member this.``Factorial 3 should be equal 6`` () =
    //     let six = mult |@ two |@ three
    //     let fac = y |@ fac'
    //     fac |@ three |> reduceToNormalForm |> should equal <| six
    