namespace Tests

open NUnit.Framework
open FsUnit
open Task2.MaxPalindrome

[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.``Max palindrome should be 906609`` () =
        maxThreeDigitPalindrome () |> should equal 906609