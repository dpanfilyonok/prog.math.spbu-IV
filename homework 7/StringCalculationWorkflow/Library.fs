﻿namespace StringCalculationWorkflow

module StringCalculationWorkflow =
    open System
    
    type CalculateBuilder() = 
        member this.Bind(x, f) = 
            match Int32.TryParse x with
            | true, num -> f num
            | _ -> None // тут мы сразу возвращаем None
        member this.Return(x) = 
            Some x