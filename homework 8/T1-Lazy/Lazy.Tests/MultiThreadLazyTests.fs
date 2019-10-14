namespace Tests

open NUnit.Framework
open FsUnit
open Lazy
open System.Threading.Tasks
open System.Threading


[<TestFixture>]
type MultiThreadLazyTestClass () =
    let threadCount = 100

    [<Test>]
    member this.``Get should return the same object`` () =
        let lazyCalc = LazyFactory.createMultiThreadLazy (fun () -> obj ())
        let expected = lazyCalc.Get ()

        let tasks = Array.init threadCount 
                        (fun _ -> Task.Factory.StartNew (lazyCalc.Get >> should equal expected))
        Task.WaitAll tasks

    [<Test>]
    member this.``Supplier should be calculated once`` () =
         
        let i = ref 0
        let lazyCalc = LazyFactory.createMultiThreadLazy (fun () -> Interlocked.Increment i)

        let tasks = Array.init threadCount 
                        (fun _ -> Task.Factory.StartNew (lazyCalc.Get >> ignore))
        Task.WaitAll tasks

        i.Value |> should equal 1