namespace Lazy

/// Класс, реализующий ленивое вычисление объекта однопоточно
type OneThreadLazy<'a>(supplier: unit -> 'a) = 
    let mutable result = None

    interface ILazy<'a> with 
        member this.Get () = 
            match result with 
            | None -> 
                result <- Some <| supplier ()
                result.Value
            | Some value -> value