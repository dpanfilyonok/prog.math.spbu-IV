namespace LocalNetwork

/// Main module
module Main = 
    open System
    open DefaultOS

    [<EntryPoint>]
    let main argv =
        let computers = 
            [|
                Computer(linuxOS, true);
                Computer(linuxOS);
                Computer(windowsOS);
                Computer(windowsOS);
                Computer(macOS);
                Computer(linuxOS);
                Computer(macOS)
            |]

        let network = [
            [1; 2; 6]
            [0; 2; 6]
            [0; 1; 3]
            [2; 4; 5; 6]
            [3]
            [3; 6]
            [0; 1; 3; 5]
        ]

        printfn "Are all nodes infected : %b" <|        
            LocalNetworkSimulator(computers, network).Start 10 
        0