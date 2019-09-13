namespace LocalNetwork

open System

type LocalNetworkSimulator(computers: Computer array, network: int list list) = 
    let c f x y = f y x
    let doStep (previousState: Computer array) = 
        let mutable newState = previousState
        List.iteri 
            (fun index item -> 
                if previousState.[index].IsInfected then ()
                else
                    let rnd = Random()
                    item
                    |> List.map (fun adjacentIndex -> previousState.[adjacentIndex].IsInfected)
                    |> Seq.sumBy (function  | true -> 1 
                                            | false -> 0)
                    |> c Seq.init (fun _ -> rnd.NextDouble ())
                    |> Seq.forall (fun randomValue -> randomValue > computers.[index].InfectionProbability)
                    |> (function    | false -> newState.[index].IsInfected <- true 
                                    | true -> ())  
            ) network
        newState

    let logStep (stepIndex: int) (currentState: Computer array) = 
        printfn "Step #%i" stepIndex
        List.iteri 
            (fun index item -> 
                if currentState.[index].IsInfected then ()
                else
                    printfn "%i: " index
                    List.iter (fun x -> printfn "\t%O" currentState.[index]) item
            ) network
        printf "\n"

    member this.Start (iterationCount: int) = 
        let rec loop remainingStepsCount (state: Computer array) = 
            if (remainingStepsCount = 0) then
                let a = Seq.forall (fun (x: Computer) -> x.IsInfected) state
                printfn "%b" a
            else 
                let newState = doStep state
                logStep (iterationCount - remainingStepsCount + 1) newState
                loop (remainingStepsCount - 1) newState
        loop iterationCount computers

// || (state |> Seq.forall (fun x -> x.IsInfected))
    