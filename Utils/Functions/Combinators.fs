namespace Utils.Functions

module Combinators = 
    let c f x y = f y x

    let iif condition x y = if condition then x else y

    let succ = (+) 1

    let pred = c (-) 1