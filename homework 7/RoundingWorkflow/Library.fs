namespace RoundingWorkflow

module RoundingWorkflow =
    open System

    /// Calculation with given accuracy
    type RoundingBuilder(accuracy: int) = 
        member this.Bind(x: float, f) = 
            Math.Round(x, accuracy) |> f
        member this.Return(x: float) = 
            Math.Round(x, accuracy)