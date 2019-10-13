namespace Lazy

open System.Threading

/// Класс, реализующий ленивое неблокирующее вычисление объекта многопоточно
type LockFreeLazy<'a>(supplier: unit -> 'a) = 
    let mutable result = None

    interface ILazy<'a> with
        member this.Get () = 
            match result with
            | Some value -> value
            | None -> 
                Interlocked.CompareExchange(&result, Some <| supplier (), None) |> ignore
                result.Value