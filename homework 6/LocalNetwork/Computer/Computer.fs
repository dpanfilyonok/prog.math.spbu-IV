namespace LocalNetwork

/// Implements computer with OS and infection status
type Computer(os: IOperationSystem, isInfected: bool) = 
    let mutable currentInfectionStatus = isInfected

    /// Returns new computer with a given OS without infection on it
    new(os: IOperationSystem) = 
        Computer(os, false)

    /// Name of OS, installed on the computer
    member this.OS 
        with get () = os.Name
        
    /// Probability of infection (depends on OS)
    member this.InfectionProbability
        with get () = os.InfectionProbability

    /// Is computer infected
    member this.IsInfected
        with get () = currentInfectionStatus
        and set isInfected = currentInfectionStatus <- isInfected

    /// Beauty view of the computer obj
    override this.ToString() =
        sprintf "(%c) %s, %.2f" 
            (if this.IsInfected then '\u2717' else '\u2713') 
            this.OS 
            this.InfectionProbability

    /// Returns new computer with same OS and infection status
    member this.ShallowCopy () = 
        Computer(os, this.IsInfected)