namespace Tests

open NUnit.Framework
open FsUnit
open Phonebook.Logic

[<TestFixture>]
type TestClass () =

    [<SetUp>]
    member this.Setup () =
        ()

    [<Test>]
    member this.Test1 () =
        Assert.Pass()
