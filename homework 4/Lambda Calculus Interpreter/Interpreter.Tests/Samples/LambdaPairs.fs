namespace Tests.Samples

module LambdaPairs = 
    open LambdaCalculus.Interpreter
    open LambdaBooleans

    let pair = "x" ^/ "y" ^/ "f" ^/ (v"f" |@ v"x" |@ v"y")
    let fst' = "p" ^/ (v"p" |@ tru)
    let snd' = "p" ^/ (v"p" |@ fls)
    