namespace Tasks

module SeqOfPrimes = 
    /// Infinite seq of natural numbers
    let numbers = Seq.initInfinite id

    /// If number is prime
    let isPrime num = 
        if num < 2 then false
        else 
            let upperBound = int (sqrt <| float num)
            seq { 2 .. upperBound }
            |> Seq.exists (fun x -> num % x = 0)
            |> not

    /// Infinite seq of primes  
    let primes = numbers |> Seq.filter isPrime