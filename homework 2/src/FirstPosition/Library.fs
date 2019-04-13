module FirstPosition

/// try to find first entry of mentioned element in list
let firstPosition (list: 'a list) (item: 'a) = 
    let rec loop (xs: 'a list) acc =
        match xs with
        | [] -> None
        | head :: _ when head = item -> Some acc
        | head :: tail when head <> item -> loop tail (acc + 1)
        | _ -> None
    loop list 0
