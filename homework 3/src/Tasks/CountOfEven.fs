module CountOfEven

open System

module StuffFuns = 
    let isEven x =  x % 2 = 0
    let mapSnd f (x, y) = x, f y
    let uncurry f (x, y) = f x y
    let curry f x y = f (x, y)
    let c f x y = f y x
    let succ = (+) 1
    let mod2 = c (%) 2

open StuffFuns

/// Count of even in list using filter fun
let usingFilter list =
    list
    |> List.filter isEven

/// Count of even in list using map fun
let usingMap list =
    list
    |> List.map (succ >> mod2 >> Math.Abs)
    |> List.sum 

/// Count of even in list using fold fun
let usingFold list =
    list
    |> List.fold (curry <| (mapSnd (succ >> mod2 >> Math.Abs) >> uncurry (+))) 0