namespace T1.FirstPosition

module FirstPosition =
    let first (item: 'a) (xs: 'a list) = 
        let rec loop (xs: 'a list) acc =
            match xs with
            | [] -> -1
            | head :: _ when head = item -> acc
            | head :: tail when head <> item -> loop tail (acc + 1)
            | _ -> -1
        loop xs 0