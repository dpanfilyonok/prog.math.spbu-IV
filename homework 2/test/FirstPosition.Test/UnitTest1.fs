module FirstPositionTest
    
open NUnit.Framework
open FsUnit
open FirstPosition

[<TestFixture>]
type TestClass () = 

    [<Test>]
    member this.``firstPosition from empty list should return None`` () = 
        firstPosition [] 0 |> should equal None

    [<Test>]
    member this.``firstPosition should return first entry of element in list`` () = 
        firstPosition [0 .. 9] 5 |> should equal (Some 5)
    
    [<Test>]
    member this.``firstPosition should return first entry of element in list with repetitions`` () = 
        firstPosition [for i in 1 .. 10 -> 5] 5 |> should equal (Some 0)

    [<Test>]
    member this.``firstPosition should return None if element not exists in list`` () = 
        firstPosition [0 .. 9] 10 |> should equal None