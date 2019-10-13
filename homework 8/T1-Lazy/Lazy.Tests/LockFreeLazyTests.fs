namespace Tests

open NUnit.Framework
open FsUnit
open Lazy
open System.Threading

[<TestFixture>]
type LockFreeLazyTestClass () =
    let threadCount = 100

    [<Test>]
    member this.``Get should return the same object`` () =
        let lazyCalc = LazyFactory.createLockFreeLazy (fun () -> obj ())
        let expected = lazyCalc.Get ()

        [1 .. threadCount]
        |> List.iter 
            (fun _ ->
                (fun _ -> lazyCalc.Get () |> should equal expected) 
                |> ThreadPool.QueueUserWorkItem 
                |> ignore
            )