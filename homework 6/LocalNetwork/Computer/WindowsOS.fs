namespace LocalNetwork

type WindowsOS() = 
    interface IOperationSystem with 
        member this.Name = "Windows"
        member this.InfectionProbability = 1.