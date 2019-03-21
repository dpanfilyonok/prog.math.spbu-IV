namespace Tasks

module SeqOfPrimes = 

    /// Infinite seq of natural numbers
    let numbers = Seq.initInfinite id

    /// If number is prime
    let isPrime num = 
        let upperBound = int (sqrt <| float num)
        let rec loop i =
            if i <= upperBound then 
                if num % i = 0 then false
                else loop <| i + 1
            else true     
        loop 2

    /// Infinite seq of primes
    let primes = numbers |> Seq.filter isPrime