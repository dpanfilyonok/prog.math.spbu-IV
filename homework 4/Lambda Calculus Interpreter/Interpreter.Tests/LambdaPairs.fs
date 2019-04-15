namespace Tests

module LambdaPairs = 
    open LambdaCalculus.Interpreter
    open LambdaBooleans

    let pair = 'x' ^/ 'y' ^/ 'f' ^/ &'f' |@ &'x' |@ &'y'
    let fst' = 'p' ^/ &'p' |@ tru
    let snd' = 'p' ^/ &'p' |@ fls