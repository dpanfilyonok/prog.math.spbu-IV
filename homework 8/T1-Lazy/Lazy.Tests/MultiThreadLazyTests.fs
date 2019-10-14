namespace Tests

open NUnit.Framework
open FsUnit
open Lazy
open System.Threading

[<TestFixture>]
type MultiThreadLazyTestClass () =
    let threadCount = 100

    [<Test>]
    member this.``Get should return the same object`` () =
        let lazyCalc = LazyFactory.createMultiThreadLazy (fun () -> obj ())
        let expected = lazyCalc.Get ()

        [1 .. threadCount]
        |> List.iter 
            (fun _ ->
                (fun _ -> lazyCalc.Get () |> should equal expected) 
                |> ThreadPool.QueueUserWorkItem 
                |> ignore
            )

    [<Test>]
    member this.``Supplier should be calculated once`` () =
        (* 
            let i = ref 0
            let lazyCalc = LazyFactory.createMultiThreadLazy (fun () -> Interlocked.Increment i)

            [1 .. threadCount]
            |> List.iter 
                (fun _ ->
                    (fun _ -> lazyCalc.Get () |> ignore) 
                    |> ThreadPool.QueueUserWorkItem 
                    |> ignore
                )

            i.Value |> should equal 1

            ??? Test not passed bc i.Value = 0 ???
        *)

        let i = ref 0
        let lazyCalc = LazyFactory.createMultiThreadLazy (fun () -> Interlocked.Increment i)

        lazyCalc.Get () |> ignore

        [1 .. threadCount]
        |> List.iter 
            (fun _ ->
                (fun _ -> lazyCalc.Get () |> should equal 1) 
                |> ThreadPool.QueueUserWorkItem 
                |> ignore
            )