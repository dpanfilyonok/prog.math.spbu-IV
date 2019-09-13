namespace LocalNetwork

module DefaultOS = 
    let linuxOS = {
        new IOperationSystem with 
            member this.Name = "Linux"
            member this.InfectionProbability = 0.3
    }

    let macOS = {
        new IOperationSystem with 
            member this.Name = "Mac"
            member this.InfectionProbability = 0.4
    }

    let windowsOS = {
        new IOperationSystem with 
            member this.Name = "Windows"
            member this.InfectionProbability = 0.7
    }