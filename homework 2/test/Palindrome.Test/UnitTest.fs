module PalindromeTests

open NUnit.Framework
open FsUnit
open Palindrome

[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.``empty string is palindrome`` () =
        isPalindrome "" |> should be True
       
    [<Test>]
    member this.``one element string is palindrome`` () =
        isPalindrome "s" |> should be True

    [<Test>]
    member this.``even element palindrome should be palindrome`` () =
        isPalindrome "abba" |> should be True

    [<Test>]
    member this.``odd element palindrome should be palindrome`` () =
        isPalindrome "abcba" |> should be True
     
    [<Test>]
    member this.``palindrome with no only letters should be palindrome`` () =
        isPalindrome "a  b,c,b  a" |> should be True
      
    [<Test>]
    member this.``not palindrome should be not palindrome :)`` () =
        isPalindrome "djsjdasjl" |> should be False
