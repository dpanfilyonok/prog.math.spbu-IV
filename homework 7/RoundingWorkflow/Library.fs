namespace RoundingWorkflow

module RoundingWorkflow =
    open System
    type RoundingBuilder(accuracy: int) = 
        member this.Bind(x: float, f) = 
            Math.Round(x, accuracy) |> f
        member this.Return(x: float) = 
            Math.Round(x, accuracy)

    let rounding = RoundingBuilder
    let s () = rounding 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    } 

    let d () = rounding 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        let! c = a / b
        return c
    } 
