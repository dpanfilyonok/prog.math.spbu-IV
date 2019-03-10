namespace Tests

open NUnit.Framework
open FsUnit
open MergeSort

[<TestFixture>]
type TestClass () =

    let isSorted (list: 'a list) = list = List.sort list 
    let isSortedViaMergeSort = mergeSort >> isSorted

    [<Test>]
    member this.``empty list should be sorted`` () =
        isSortedViaMergeSort [] |> should be True

    [<Test>]
    member this.``single element list should be sorted`` () =
        isSortedViaMergeSort [1] |> should be True
   
    [<Test>]
    member this.``list of repeated elements should be sorted`` () =
        isSortedViaMergeSort [for i in 1 .. 10 -> 5] |> should be True
    
    [<Test>]
    member this.``list with negative numbers should be sorted`` () =
        isSortedViaMergeSort [1;2;3;4;5;-5;-4;-3;-2;-1] |> should be True

    [<Test>]
    member this.``reversed list should be sorted`` () =
        isSortedViaMergeSort [6;5;4;3;2;1] |> should be True
    
    [<Test>]
    member this.``couple elements list should be sorted`` () =
        isSortedViaMergeSort [1;-1] |> should be True

    [<Test>]
    member this.``triple elements list should be sorted`` () =
        isSortedViaMergeSort [0;1;-1] |> should be True
       