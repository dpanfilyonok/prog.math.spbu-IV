namespace BalancedBrackets

module BalanceChecker =    
    let c f x y = f y x

    let openBrackets = ['('; '{'; '['] 
    let closeBrackets = [')'; '}'; ']']

    let pairBrackets = List.zip openBrackets closeBrackets |> Map.ofList

    let parseSeq (sequence: string, stack: char ImmutableStack) = 
        5

    // let stack = Stack<char>()

    // let rec parseSeq (sequence, stack: char Stack)= 
    //     match sequence with
    //     | ch :: tail when ch |> c List.contains openBrackets -> 
    //         ch |> stack.Push
    //         parseSeq tail stack
    //     | ch :: tail when ch |> c List.contains openBrackets

    // let checkIfBalanced (sequence: #seq<char>) = 
