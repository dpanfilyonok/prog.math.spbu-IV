namespace BalancedBrackets

module BalanceChecker =  

    /// List of open brackets of three types
    let openBrackets = ['('; '{'; '['] 

    /// List of closed brackets of three type
    let closeBrackets = [')'; '}'; ']']

    /// Dictionary that map open bracket to appropriate closed bracket for each of three types
    let private pairBrackets = List.zip openBrackets closeBrackets |> Map.ofList

    /// Active pattern, that define type of bracket 
    let private (|Open|Closed|) char = 
        if openBrackets |> List.contains char
            then Open
        else Closed

    /// Check is bracket sequence correct (only '()' '[]' '{}' avaliable)
    let isBracketSequenceCorrect (sequence: char seq) = 
        let rec loop (list: char list, stack: char ImmutableStack, isIncorrect: bool) = 
            match list with
            | [] ->
                // true if seq is correct
                match stack with
                | Empty -> not isIncorrect
                | _ -> false
            | head :: tail ->
                match head with
                | Open -> loop (tail, stack.Push head, false)
                | Closed when Option.isNone <| stack.Top () -> loop (List.empty, Empty, true)
                | Closed when pairBrackets.[Option.get <| stack.Top ()] = head -> loop (tail, stack.Pop (), false)
                | _ -> loop (List.empty, Empty, true)
        loop (List.ofSeq sequence, Empty, false)