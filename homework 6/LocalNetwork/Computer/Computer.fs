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
        sprintf "(%c) %s, %.2f" 
            (if this.IsInfected then '\u2717' else '\u2713') 
            this.OS 
            this.InfectionProbability

    member this.ShallowCopy () = 
        Computer(os, this.IsInfected)