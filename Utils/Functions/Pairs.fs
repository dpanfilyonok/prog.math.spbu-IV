namespace Functions

module Pairs = 
    let replicate x = x, x

    let curry f x y = f (x, y)

    let uncurry f (x, y) = f x y

    let swap (x, y) = (y, x)

    let mapPair f (x, y) = f x, f y

    let mapFst f (x, y) = f x, y

    let mapSnd f (x, y) = x, f y