namespace Lazy

open Lazy
open System.Threading

module Main = 
    [<EntryPoint>]
    let main arg = 
        let lazyCalc = LazyFactory.createLockFreeLazy (fun () -> obj ())
        let expected = lazyCalc.Get ()

        [1 .. 100]
        |> List.iter 
            (fun _ ->
                (fun o -> lazyCalc.Get () |> ignore) 
                |> ThreadPool.QueueUserWorkItem 
                |> ignore
            )
        0