namespace Tests

module LambdaCombinators = 
    open LambdaCalculus.Interpreter

    let s = 'f' ^/ 'g' ^/ 'x' ^/ (&'f' |@ &'x' |@ (&'g' |@ &'x'))
    let b = 'f' ^/ 'g' ^/ 'x' ^/ (&'f' |@ (&'g' |@ &'x'))
    let k = 'x' ^/ 'y' ^/ &'x'
    let k' = 'x' ^/ 'y' ^/ &'y'
    let i = 'x' ^/ &'x'