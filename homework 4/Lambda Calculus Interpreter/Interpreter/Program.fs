open System
open LambdaCalculus.Interpreter

[<EntryPoint>]
let main argv =
    let s = 'f' ^/ 'g' ^/ 'x' ^/ (&'f' |@ &'x' |@ (&'g' |@ &'x'))
    let k = 'x' ^/ 'y' ^/ &'x'

    let skk = s |@ k |@ k
    (reduceToNormalForm skk).ToString () |> printf "%s\n"

    0 // return an integer exit code
