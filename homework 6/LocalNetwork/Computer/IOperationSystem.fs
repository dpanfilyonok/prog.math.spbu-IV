namespace LocalNetwork

/// Interface, representing cumputer operation system
type IOperationSystem = 

    /// Name od OS
    abstract member Name: string 

    /// Infection probability of OS
    abstract member InfectionProbability: float