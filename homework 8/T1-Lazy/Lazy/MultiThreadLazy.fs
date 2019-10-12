namespace Lazy

open System.Threading

/// Класс, реализующий ленивое вычисление объекта многопоточно
type MultiThreadLazy<'a>(supplier: unit -> 'a) = 
    let mutable result = None
    let lockObject = obj ()

    interface ILazy<'a> with
        member this.Get () = 
            match Volatile.Read (ref result) with
            | Some value -> value
            | None -> 
                // без внешней проверки lock будет при каждом вызове Get()
                (fun () -> 
                    match Volatile.Read (ref result) with
                    | Some value -> value
                    | None ->
                        Volatile.Write (ref result, Some <| supplier ())
                        result.Value
                ) |> lock lockObject