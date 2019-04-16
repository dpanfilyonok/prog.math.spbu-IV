namespace Tests

module LambdaCombinators = 
    open LambdaCalculus.Interpreter

    let s = "f" ^/ "g" ^/ "x" ^/ (v"f" |@ v"x" |@ (v"g" |@ v"x"))
    let b = "f" ^/ "g" ^/ "x" ^/ (v"f" |@ (v"g" |@ v"x"))
    let k = "x" ^/ "y" ^/ v"x"
    let k' = "x" ^/ "y" ^/ v"y"
    let i = "x" ^/ v"x"