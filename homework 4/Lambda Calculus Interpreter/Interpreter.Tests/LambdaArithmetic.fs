namespace Tests

module LambdaArithmetic = 
    open LambdaCalculus.Interpreter
    open LambdaBooleans

    let zero = 's' ^/ 'z' ^/ &'z'
    let one = 's' ^/ 'z' ^/ &'s' |@ &'z'
    let two = 's' ^/ 'z' ^/ &'s' |@ (&'s' |@ &'z')

    let iszro = 'n' ^/ &'n' |@ ('x' ^/ fls) |@ tru
    let suc   = 'n' ^/ 's' ^/ 'z' ^/ &'s' |@ (&'n' |@ &'s' |@ &'z')
    let plus  = 'm' ^/ 'n' ^/ 's' ^/ 'z' ^/ &'m' |@ &'s' |@ (&'n' |@ &'s' |@ &'z')
    let mult  = 'm' ^/ 'n' ^/ 's' ^/ &'m' |@ (&'n' |@ &'s')
    let pow   = 'm' ^/ 'n' ^/ &'n' |@ &'m'