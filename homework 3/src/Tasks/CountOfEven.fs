module CountOfEven

let isEven x =  x % 2 = 0

let usingFilter list =
    list
    |> List.filter isEven

let usingFold list =
    list
    |> List.fold (fun x acc -> if isEven then (acc + 1)) else acc) 0
