namespace Tests

module LambdaBooleans = 
    open LambdaCalculus.Interpreter

    let tru = 't' ^/ 'f' ^/ &'t'
    let fls = 't' ^/ 'f' ^/ &'f'
    let iif = 'b' ^/ 'x' ^/ 'y' ^/ &'b' |@ &'x' |@ &'y'
    let not' = 'b' ^/ 't' ^/ 'f' ^/ &'b' |@ &'f' |@ &'t'
    let and' = 'x' ^/ 'y' ^/ &'x' |@ &'y' |@ fls
    let or'  = 'x' ^/ 'y' ^/ &'x' |@ tru |@ &'y'
