namespace Tasks

module MyImmutableQueue = 
    /// Немутабельная очередь
    type 'a MyImmutableQueue =
        | Empty 
        | ImmutableQueue of 'a * 'a MyImmutableQueue
        
        /// Добавить в очередь
        member this.Enqueue(x) =
            let rec enqueue queue x = 
                match queue with
                | Empty -> ImmutableQueue(x, Empty)
                | ImmutableQueue(h, t) -> ImmutableQueue(h, enqueue t x)
            enqueue this x

        /// Забрать из очереди пару
        member this.Dequeue () = 
            match this with
            | Empty -> failwith "Queue is empty"
            | ImmutableQueue(h, t) -> h, t

        /// Забрать из очереди элемент
        member this.First () = 
            match this with
            | Empty -> failwith "Queue is empty"
            | ImmutableQueue(h, t) -> h
        
        member this.Tail () = 
            match this with
            | Empty -> failwith "Queue is empty"
            | ImmutableQueue(h, t) -> t