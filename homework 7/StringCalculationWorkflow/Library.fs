namespace StringCalculationWorkflow

module StringCalculationWorkflow =
    open System
    type CalculateBuilder() = 
        member this.Bind(x, f) = 
            match Int32.TryParse x with
            | true, num -> f num
            | _ -> None // тут мы сразу возвращаем None
        member this.Return(x) = 
            Some x

    let calculate = CalculateBuilder()

    let res () = calculate {
        let! x = "1"
        let! y = "2"
        let z = x + y
        return z
    }