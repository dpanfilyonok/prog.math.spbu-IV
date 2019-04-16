open System
open LambdaCalculus.Interpreter

[<EntryPoint>]
let main argv =
    // тут вообще не понятен порядок операция и тп
    let s = "f" ^/ "g" ^/ "x" ^/ (v"f" |@ v"x" |@ (v"g" |@ v"x"))
    let k = "x" ^/ "y" ^/ v"x"
    let i = "x" ^/ v"x"
    let k' = "x" ^/ "y" ^/ v"y"
    let b = "f" ^/ "g" ^/ "x" ^/ (v"f" |@ (v"g" |@ v"x"))


    // let w3 = "x" ^/ (v"x" |@ v"x" |@ v"x")

    // let omega3 = w3 |@ w3 |@ w3
    // omega3.ToString () |> printf "%s\n"
    // (reduceToNormalForm omega3).ToString () |> printf "%s\n"

    // let x = "x" ^/ v"x"
    // let y = "y" ^/ v"y"
    // let ki = k |@ i
    // let sksk = s |@ (k |@ s) |@ k
    areAlphaEquivalent (v"x" |@ v"y") (v"y" |@ v"x") |> printfn "%b" 

    // let renameAllVariables = performMappingOnAllVariablesInTerm (fun x -> x + "'")
    // reduceToNormalForm sksk 
    // |> renameAllVariables
    // |> performAlphaConversionOnAllAbstractionsInTerm 
    // |> printfn "%A"

    

    0 // return an integer exit code
