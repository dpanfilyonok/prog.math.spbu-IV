namespace Task1

module AlternatingSequence =
    /// Make seq of repeated elements
    let rec repeat items = 
        seq { yield! items     
              yield! repeat items }

    /// Inf seq of numbers from n
    let rec numbersFrom n = 
         seq { yield n
               yield! numbersFrom (n + 1) }

    /// Seq of alternating numbers
    let alternatingNumberSequence = 
        repeat [1; -1]
        |> Seq.zip <| numbersFrom 1
        |> Seq.map (fun pair -> fst pair * snd pair)