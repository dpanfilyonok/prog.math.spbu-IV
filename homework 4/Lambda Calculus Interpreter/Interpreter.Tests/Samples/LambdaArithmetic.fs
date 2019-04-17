namespace Tests.Samples

module LambdaArithmetic = 
    open LambdaCalculus.Interpreter
    open LambdaBooleans
    open LambdaPairs
    open LambdaCombinators

    let zero  = "s" ^/ "z" ^/ v"z"
    let one   = "s" ^/ "z" ^/ (v"s" |@ v"z")
    let two   = "s" ^/ "z" ^/ (v"s" |@ (v"s" |@ v"z"))
    let three = "s" ^/ "z" ^/ (v"s" |@ (v"s" |@ (v"s" |@ v"z")))

    let iszro = "n" ^/ (v"n" |@ ("x" ^/ fls) |@ tru)
    let succ  = "n" ^/ "s" ^/ "z" ^/ (v"s" |@ (v"n" |@ v"s" |@ v"z"))
    let plus  = "m" ^/ "n" ^/ "s" ^/ "z" ^/ (v"m" |@ v"s" |@ (v"n" |@ v"s" |@ v"z"))
    let mult  = "m" ^/ "n" ^/ "s" ^/ (v"m" |@ (v"n" |@ v"s"))
    let pow   = "m" ^/ "n" ^/ (v"n" |@ v"m")

    let xz   = "x" ^/ (pair |@ v"x" |@ zero)
    let fs   = "f" ^/ "p" ^/ (pair |@ (v"f" |@ (fst' |@ v"p") |@ (snd' |@ v"p")) |@ (succ |@ (snd' |@ v"p")))
    let rec' = "m" ^/ "f" ^/ "x" ^/ (fst' |@ (v"m" |@ (fs |@ v"f") |@ (xz |@ v"x")))
    let pred = "m" ^/ (rec' |@ v"m" |@ k' |@ zero)

    let fac' = "f" ^/ "n" ^/ (iif |@ (iszro |@ v"n") 
                                |@ one 
                                |@ (mult |@ v"n" |@ (v"f" |@ (pred |@ v"n"))))