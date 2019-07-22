namespace BalancedBrackets

module BalanceChecker =  
    open FSharpx.Collections

    let openBrackets = ['('; '{'; '['] 
    let closeBrackets = [')'; '}'; ']']
    let pairBrackets = List.zip openBrackets closeBrackets |> Map.ofList

    let (|Open|Closed|) char = 
        if openBrackets |> List.contains char
            then Open
        else Closed

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
                | Closed when pairBrackets.[stack.Top ()] = head -> loop (tail, stack.Pop (), false)
                | _ -> loop (LazyList.empty, Empty, true)
        loop (LazyList.ofSeq sequence, Empty, false)