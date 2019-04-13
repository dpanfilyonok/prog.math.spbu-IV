namespace Tests

open NUnit.Framework
open FsUnit
open RoundingWorkflow.RoundingWorkflow

[<TestFixture>]
type TestClass () =

    let rounding = RoundingBuilder

    [<Test>]
    member this.``Smoke test should passed`` () =
        let res  = rounding 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
        res |> should (equalWithin 0.001) 0.048

    [<Test>]
    member this.``Calculation with int should be correct`` () =
        let res  = rounding 3 {
            let! a = 1.
            let! b = 2.
            return a + b
        }
        res |> should (equalWithin 0.001) 3. 

    [<Test>]
    member this.``Calculation with zero accuracy should be correct`` () =
        let res  = rounding 0 {
            return 3. / 2. + 1.
        }
        res |> should equal 2.