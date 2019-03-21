namespace Tests

open NUnit.Framework
open FsUnit

open Tasks.ArithmeticParseTree

[<TestFixture>]
type ArithmeticParseTreeTest () =

    static member testExpressions = [
        Sub(Num 5., Num 3.), 2.
        Add(Mult(Num 2., Num 3.), Div(Num 2., Num 5.)), 6.4
        Add(Mult(Add(Mult(Add(Num -1., Num 4.), Num 2.), Num -10.), Num 2.), Num 8.), 0.
    ]

    [<Test>]
    [<TestCaseSource("testExpressions")>]
    member this.``parse tree tests`` (testData: TreeNode * 'a) =
        let (tree, expectedValue) = testData
        tree |> evaluateTree |> should (equalWithin 0.001) expectedValue

    [<Test>]
    member this.``division by 0 should raise exception`` () =
        let tree = Div(Num(1.), Num(0.))
        tree |> evaluateTree |> ignore |> should throw typeof<System.DivideByZeroException>