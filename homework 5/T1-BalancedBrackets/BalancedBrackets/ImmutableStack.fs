namespace BalancedBrackets

/// Immutable stack
type 'a ImmutableStack =
    | Empty 
    | Stack of 'a * 'a ImmutableStack

    /// Returns new stack with given element on the top
    member this.Push x = 
        Stack(x, this)

    /// Returns option of top element of stack or raise exception if it empty 
    member this.Top () = 
        match this with
        | Empty -> None
        | Stack(top, _) -> Some top

    /// Returns new stack without top element or raise exception if it empty 
    member this.Pop () = 
        match this with
        | Empty -> failwith "Stack is empty"
        | Stack(_, stack) -> stack