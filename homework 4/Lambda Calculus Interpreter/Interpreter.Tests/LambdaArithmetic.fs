namespace Tests

module LambdaArithmetic = 
    open LambdaCalculus.Interpreter
    open LambdaBooleans

    let zero = "s" ^/ "z" ^/ v"z"
    let one = "s" ^/ "z" ^/ v"s" |@ v"z"
    let two = "s" ^/ "z" ^/ v"s" |@ (v"s" |@ v"z")

    let iszro = "n" ^/ v"n" |@ ("x" ^/ fls) |@ tru
    let suc   = "n" ^/ "s" ^/ "z" ^/ v"s" |@ (v"n" |@ v"s" |@ v"z")
    let plus  = "m" ^/ "n" ^/ "s" ^/ "z" ^/ v"m" |@ v"s" |@ (v"n" |@ v"s" |@ v"z")
    let mult  = "m" ^/ "n" ^/ "s" ^/ v"m" |@ (v"n" |@ v"s")
    let pow   = "m" ^/ "n" ^/ v"n" |@ v"m"