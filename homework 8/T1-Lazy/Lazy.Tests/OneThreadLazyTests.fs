namespace Tests

open NUnit.Framework
open FsUnit
open Lazy
open System.Threading

[<TestFixture>]
type OneThreadLazyTestClass () =

    [<Test>]
    member this.``Get should return the same object`` () =
        let lazyCalc = LazyFactory.createOneThreadLazy (fun () -> obj ())
        let obj1 = lazyCalc.Get ()
        let obj2 = lazyCalc.Get ()

        obj1 |> should equal obj2

    [<Test>]
    member this.``Supplier should be calculated once`` () =
        let mutable i = 0
        let lazyCalc = LazyFactory.createOneThreadLazy (fun () -> i <- i + 1; i)
        [ lazyCalc.Get (); lazyCalc.Get () ] |> ignore

        i |> should equal 1

    [<Test>]
    member this.``Supplier returning None should work correctly`` () =
        let lazyCalc = LazyFactory.createOneThreadLazy (fun () -> None)
        let obj1 = lazyCalc.Get ()
        let obj2 = lazyCalc.Get ()

        obj1 |> Option.isNone |> should be True
        obj2 |> Option.isNone |> should be True