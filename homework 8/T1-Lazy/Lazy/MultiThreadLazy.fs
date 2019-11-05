namespace Lazy

open System.Threading

/// Класс, реализующий ленивое вычисление объекта многопоточно
type MultiThreadLazy<'a>(supplier: unit -> 'a) = 
    [<VolatileField>]
    let mutable result = None
    let lockObject = obj ()

    interface ILazy<'a> with
        member this.Get () = 
            match result with
            | Some value -> value
            | None -> 
                // без внешней проверки lock будет при каждом вызове Get()
                (fun () -> 
                    match result with
                    | Some value -> value
                    | None ->
                        result <- Some <| supplier ()
                        result.Value
                ) |> lock lockObject