namespace BalancedBrackets

module BalanceChecker =    
    let c f x y = f y x

    let openBrackets = ['('; '{'; '['] 
    let closeBrackets = [')'; '}'; ']']

    let pairBrackets = List.zip openBrackets closeBrackets |> Map.ofList

    let filterBrackets str = 
        str
        |> Seq.filter (fun x -> (openBrackets |> List.contains x || closeBrackets |> List.contains x))

    let rec parseSeq (str: string, stack: char ImmutableStack) = 
        match str with 
        | St

