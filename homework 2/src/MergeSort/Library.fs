module MergeSort

/// split list in aa half
let split list =
    let rec split' ls left right = 
        match ls with
        | [] -> (left, right)
        | [a] -> (a :: left, right)
        | a :: b :: tail -> split' tail (a :: left) (b :: right)
    split' list [] []
    
/// merge two sorted list in one sorted 
let merge left right = 
    let rec merge' left right acc = 
        match left, right with
        | [], _ -> (acc |> List.rev) @ right 
        | _, [] -> (acc |> List.rev) @ left
        | leftHead :: leftTail, rightHead :: rightTail ->
            if leftHead <= rightHead then merge' leftTail right (leftHead :: acc)
            else merge' rightTail left (rightHead :: acc)
    merge' left right []

let map f (x, y) = (f x, f y)
let uncurry f (x, y) = f x y

/// merge sort
let rec mergeSort (list: 'a list) = 
    match list with
    | [] -> []
    | [a] -> [a]
    | _ -> 
        list
        |> split
        |> map mergeSort
        |> uncurry merge    