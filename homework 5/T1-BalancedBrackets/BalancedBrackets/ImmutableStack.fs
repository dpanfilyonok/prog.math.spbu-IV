namespace BalancedBrackets

type 'a ImmutableStack =
    | Empty 
    | Stack of 'a * 'a ImmutableStack

    member this.Push x = 
        Stack(x, this)

    member this.Top () = 
        match this with
        | Empty -> failwith "Underflow"
        | Stack(top, _) -> top

    member this.Pop () = 
        match this with
        | Empty -> failwith "Contain no elements"
        | Stack(_, stack) -> stack