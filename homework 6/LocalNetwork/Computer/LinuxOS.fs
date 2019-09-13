namespace LocalNetwork

type LinuxOS() = 
    interface IOperationSystem with
        member this.Name = "Linux"
        member this.InfectionProbability = 0.25