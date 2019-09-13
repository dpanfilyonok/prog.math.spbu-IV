namespace LocalNetwork

type MacOS() = 
    interface IOperationSystem with 
        member this.Name = "Mac"
        member this.InfectionProbability = 0.4