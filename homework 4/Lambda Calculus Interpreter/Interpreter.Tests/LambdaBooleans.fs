namespace Tests

module LambdaBooleans = 
    open LambdaCalculus.Interpreter

    let tru = "t" ^/ "f" ^/ v"t"
    let fls = "t" ^/ "f" ^/ v"f"
    let iif = "b" ^/ "x" ^/ "y" ^/ v"b" |@ v"x" |@ v"y"
