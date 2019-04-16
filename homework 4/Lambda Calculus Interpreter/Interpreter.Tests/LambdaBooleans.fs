namespace Tests

module LambdaBooleans = 
    open LambdaCalculus.Interpreter

    let tru = "t" ^/ "f" ^/ v"t"
    let fls = "t" ^/ "f" ^/ v"f"
    let iif = "b" ^/ "x" ^/ "y" ^/ v"b" |@ v"x" |@ v"y"
    let not' = "b" ^/ "t" ^/ "f" ^/ v"b" |@ v"f" |@ v"t"
    let and' = "x" ^/ "y" ^/ v"x" |@ v"y" |@ fls
    let or'  = "x" ^/ "y" ^/ v"x" |@ tru |@ v"y"
