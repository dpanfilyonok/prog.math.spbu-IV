namespace LocalNetwork

/// Module with standart OS implementations
module DefaultOS = 

    /// Linux (0.3)
    let linuxOS = {
        new IOperationSystem with 
            member this.Name = "Linux"
            member this.InfectionProbability = 0.3
    }

    /// Mac (0.4)
    let macOS = {
        new IOperationSystem with 
            member this.Name = "Mac"
            member this.InfectionProbability = 0.4
    }

    /// Windows (0.7)
    let windowsOS = {
        new IOperationSystem with 
            member this.Name = "Windows"
            member this.InfectionProbability = 0.7
    }