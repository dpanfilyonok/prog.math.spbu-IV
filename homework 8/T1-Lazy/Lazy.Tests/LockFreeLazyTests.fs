namespace Tests

open NUnit.Framework
open FsUnit
open Lazy
open System.Threading.Tasks
open System.Threading

[<TestFixture>]
type LockFreeLazyTestClass () =
    let threadCount = 100

    [<Test>]
    member this.``Get should return the same object`` () =
        let lazyCalc = LazyFactory.createLockFreeLazy (fun () -> obj ())
        let expected =  lazyCalc.Get ()

        let tasks = Array.init threadCount 
                        (fun _ -> Task.Factory.StartNew (lazyCalc.Get >> should equal expected))
        Task.WaitAll tasks