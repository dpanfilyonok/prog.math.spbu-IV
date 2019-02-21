namespace Tasks

module Combinators = 
    let iif condition x y = if condition then x else y
    let isZero = (=) 0
    let c f x y = f y x
    let pred = c (-) 1
    let rec fix' f = lazy f (fix' f)
    let force (value: Lazy<_>) = value.Force()
    let fix f = force <| fix' f

module Factorial =
    open Combinators    

    let facCondition n f = iif (n < 0) 0 f

    let rec factorial1 n =  iif (isZero n) 1 ((*) n (factorial1 (n - 1)))
    let rec factorial2 n = (fun f -> iif (isZero n) 1 ((*) n (f <| pred n))) factorial2
    let factorial3 = fix (fun f n -> iif (isZero n) 1 ((*) n (force f <| pred n)))

    let rec factorial4 n = 
        facCondition n (match n with 
                        | 0 -> 1
                        | _ -> n * (factorial4 <| pred n))
 
    let factorial5 n =
        let rec loop i acc =
            if n = 0 then acc else loop (pred n) (acc *i)
        facCondition n (match n with 
                        | 0 -> 1
                        | _ -> loop n 1)

    let factorial6 n = facCondition n (List.fold (*) 1 [1..n])

module Fibonacci = 
    open Combinators

    let fibCondition n f = iif (n <= 0) 0 f

    let fibonacci1 n = 
        let fibSeq =  
            let rec loop a b = 
                seq {   yield a 
                        yield! loop b (a + b) }             
            loop 1 1 |> Seq.cache
        fibCondition n (Seq.take n fibSeq |> Seq.toList |> List.last)
         
    let fibonacci2 n = 
        let rec loop i prev curr = 
            match i with
            | 1 -> curr
            | _ -> loop (pred i) curr (prev + curr)
        fibCondition n (loop n 0 1)

module ListReverse =
    let rec reverse1 xs = 
        match xs with 
        | [] -> []
        | head :: tail -> reverse1 tail @ [head]
    
    let reverse2 xs = 
        let rec loop ys acc = 
            match ys with
            | [] -> acc
            | head :: tail -> loop tail (head :: acc)
        loop xs []

module ListOf2Pows = 
    let makeList1 n m = [for each in n .. m do yield pown 2 each]
