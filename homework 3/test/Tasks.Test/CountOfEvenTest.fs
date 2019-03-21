namespace Tests

open NUnit.Framework
open FsUnit

open Tasks

[<TestFixture>]
type CountOfEvenTest () =

    let testCases = [
        // empty list
        [], 0

        // list with only zero which is even
        [0], 1

        // list with only one which is odd
        [1], 0

        // list with only minus two which even 
        [-2], 0

        // list with only two which even 
        [2], 1

        // list with same count of even and odd nums
        [1;2;3;4;5;6], 3

        // list with same count of negative even and odd nums
        [-1;-2;-3;-4;-5;-6], 3

        // list with only odd nums
        [1;-3;5], 0

        // list in which even > odd nums
        [0;1;2;3;4;5;6], 4 
    ]

    [<Test>]
    [<TestCaseSource("testCases")>]
    member this.``usingFilter should work properly`` list expectedValue =
        CountOfEven.usingFilter list |> should equal expectedValue 

    [<Test>]
    [<TestCaseSource("testCases")>]
    member this.``usingMap should work properly`` list expectedValue =
        CountOfEven.usingMap list |> should equal expectedValue 

    [<Test>]
    [<TestCaseSource("testCases")>]
    member this.``usingFold should work properly`` list expectedValue =
        CountOfEven.usingFold list |> should equal expectedValue 