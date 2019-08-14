namespace Tests

open NUnit.Framework
open FsCheck
open FsCheck.NUnit
open PointFree.PointFreeFuncs

[<TestFixture>]
type PointFreeTestClass () =

    [<Property>]
    member this.``Original and in point-free should be the same`` (x: int) (l: int list) =
        func x l = func'5 x l
