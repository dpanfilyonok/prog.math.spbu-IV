namespace LocalNetwork

open System

module Main = 
    [<EntryPoint>]
    let main argv =
        let comps = [| Computer(WindowsOS()); Computer(WindowsOS()) |]
        let net = [[1]; [1]]
        let sim = LocalNetworkSimulator(comps, net)
        sim.Start(10)
        0