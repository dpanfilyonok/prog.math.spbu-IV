namespace LocalNetwork

open System

type LocalNetworkSimulator(computers: Computer array, network: int list list) = 
    let c f x y = f y x
    let doStep (previousState: Computer array) = 
        let random = Random()
        let newState = [| for node in previousState -> node.ShallowCopy () |]

        List.iteri 
            (fun index item -> 
                if previousState.[index].IsInfected then ()
                else
                    item
                    |> List.map (fun adjacentIndex -> previousState.[adjacentIndex].IsInfected)
                    |> Seq.sumBy (function  | true -> 1 
                                            | false -> 0)
                    |> c Seq.init (fun _ -> random.NextDouble ())
                    |> Seq.forall (fun randomValue -> randomValue > computers.[index].InfectionProbability)
                    |> (function    | false -> newState.[index].IsInfected <- true 
                                    | true -> ())
            ) network
        newState

    let logState stepIndex (currentState: Computer array) = 
        printfn "Step #%i" stepIndex
        List.iteri 
            (fun index item -> 
                printfn "Node %i: %c" index (if currentState.[index].IsInfected then '\u2717' else '\u2713')
                List.iter 
                    (fun adjacentIndex -> 
                        printfn "%i - %O" adjacentIndex currentState.[adjacentIndex]
                    ) item
            ) network
        printf "\n"

    member this.Start iterationCount = 
        let rec loop remainingSteps currentState = 
            if remainingSteps = 0 || currentState |> Seq.forall (fun (node: Computer) -> node.IsInfected) then
                logState (iterationCount - remainingSteps) currentState 
                let allInfected = Seq.forall (fun (node: Computer) -> node.IsInfected) currentState
                allInfected
            else 
                logState (iterationCount - remainingSteps) currentState
                doStep currentState |> loop (remainingSteps - 1)
        loop iterationCount computers    