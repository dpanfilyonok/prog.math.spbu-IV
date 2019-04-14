namespace Tests

open NUnit.Framework
open FsUnit
open LambdaCalculus.Interpreter
open LambdaCalculus.LambdaCombinators

[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.``Statement that 'SKK is beta equal to I' should be proved`` () =
        let skk = Application(Application(s, k), k)
        reductToNormalForm skk |> should equal i

    [<Test>]
    member this.``Statement that 'S(KS)K is beta equal to B' should be proved`` () =
        let sksk = Application(Application(s, Application(s, k)), k)
        reductToNormalForm sksk |> should equal b

    [<Test>]
    member this.``Statement that 'II is beta equal to I' should be proved`` () =
        let ii = Application(i, i)
        reductToNormalForm ii |> should equal i
      
    [<Test>]
    member this.``Statement that 'KI is beta equal to K with asterisk' should be proved`` () =
        let ki = Application(k, i)
        reductToNormalForm ki |> should equal k'

    // проверить комбинатор неподвижной точки