namespace LocalNetwork

type Computer(os: IOperationSystem, isInfected: bool) = 
    let mutable mutableIsInfected = isInfected

    new(os: IOperationSystem) = 
        Computer(os, false)

    member this.OS 
        with get () = os.Name
        
    member this.InfectionProbability
        with get () = os.InfectionProbability

    member this.IsInfected
        with get () = mutableIsInfected
        and set isInfected = mutableIsInfected <- isInfected

    override this.ToString() =
        let basicString = sprintf "%s, %.2f" this.OS this.InfectionProbability
        if this.IsInfected 
            then sprintf "[[%s]]" basicString
        else basicString