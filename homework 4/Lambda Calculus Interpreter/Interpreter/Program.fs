open System
open LambdaCalculus.Interpreter

[<EntryPoint>]
let main argv =
    areAlphaEquivalent (v"x" |@ v"y") (v"y" |@ v"x") 
    |> printfn "%b" 
    0 // return an integer exit code