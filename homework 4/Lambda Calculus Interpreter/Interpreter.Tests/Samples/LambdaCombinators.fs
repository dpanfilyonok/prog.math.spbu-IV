namespace Tests.Samples

module LambdaCombinators = 
    open LambdaCalculus.Interpreter

    let s = "f" ^/ "g" ^/ "x" ^/ (v"f" |@ v"x" |@ (v"g" |@ v"x"))
    let b = "f" ^/ "g" ^/ "x" ^/ (v"f" |@ (v"g" |@ v"x"))
    let k = "x" ^/ "y" ^/ v"x"
    let k' = "x" ^/ "y" ^/ v"y"
    let i = "x" ^/ v"x"
    let omega3 = "x" ^/ (v"x" |@ v"x" |@ v"x") 
    let omegaBig3 = omega3 |@ omega3

    let y = "f" ^/ (("x" ^/ (v"f" |@ (v"x" |@ v"x"))) |@ ("x" ^/ (v"f" |@ (v"x" |@ v"x"))))