namespace Tests

open NUnit.Framework
open FsUnit
open StringCalculationWorkflow.StringCalculationWorkflow

[<TestFixture>]
type TestClass () =

    let calculate = CalculateBuilder()

    [<Test>]
    member this.``Smoke test with int should return correct value`` () =
        let res = calculate {
            let! x = "1"
            let! y = "2"
            let z = x + y
            return z
        }
        res |> should equal <| Some 3

    [<Test>]
    member this.``Smoke test with not int should return None`` () =
        let res = calculate {
            let! x = "1"
            let! y = "Ъ"
            let z = x + y
            return z
        }
        res |> should equal <| None

    [<Test>]
    member this.``Smoke test with float should return None`` () =
        let res = calculate {
            let! x = "1.2"
            let! y = "2"
            let z = x + y
            return z
        }
        res |> should equal <| None

    [<Test>]
    member this.``Division by zero in workflow should raise exception`` () =
        let res () = calculate {
            let! x = "1"
            let! y = "0"
            let z = x / y
            return z
        }
        (fun () -> res () |> ignore) |> should throw typeof<System.DivideByZeroException>
