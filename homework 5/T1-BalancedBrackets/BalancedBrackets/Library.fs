namespace BalancedBrackets

module BalanceChecker =  
    open FSharpx.Collections

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
        let rec loop (lazyList: char LazyList, stack: char ImmutableStack, isIncorrect: bool) = 
            match lazyList with
            | LazyList.Nil ->
                // true if seq is correct
                match stack with
                | Empty -> not isIncorrect
                | _ -> false
            | LazyList.Cons(head, tail) ->
                match head with
                | Open -> loop (tail, stack.Push head, false)
                | Closed when Option.isNone <| stack.Top () -> loop (LazyList.empty, Empty, true)
                | Closed when pairBrackets.[Option.get <| stack.Top ()] = head -> loop (tail, stack.Pop (), false)
                | _ -> loop (LazyList.empty, Empty, true)
        loop (LazyList.ofSeq sequence, Empty, false)