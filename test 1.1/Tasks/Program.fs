namespace Tasks

module SumOfFibNums = 
    let iif condition x y = if condition then x else y
    let c f x y = f y x
    let pred = c (-) 1
    let fibCondition n f = iif (n <= 0) 0 f 
    let fibSeq: seq<bigint> =  
        let rec loop a b = 
            seq {   yield a 
                    yield! loop b (a + b) }             
        loop 1I 1I |> Seq.cache

    let isEven (x: bigint) = int x % 2 = 0
    let sum () = 
        fibSeq
        |> Seq.filter isEven
        |> Seq.where (fun x -> x <= 1000000I)
        |> Seq.sum
  
    [<EntryPoint>]
    let main argv =
        printfn "%A" <| sum ()
