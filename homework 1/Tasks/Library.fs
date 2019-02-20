namespace Tasks

module Combinators = 
    let iif condition x y = if condition then x else y
    let isZero = (=) 0
    let c f x y = f y x
    let pred = c (-) 1
    let rec y f = f (y f)

module Factorial =
    open Combinators    
    let rec factorial1 n =  iif (isZero n) 1 ((*) n (factorial1 (n - 1)))
    let rec factorial2 n = (fun f -> iif (isZero n) 1 ((*) n (f <| pred n))) factorial2
    let factorial3 = y (fun f -> fun n -> iif (isZero n) 1 ((*) n (f <| pred n)))
