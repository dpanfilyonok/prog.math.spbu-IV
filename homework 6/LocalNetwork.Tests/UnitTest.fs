namespace Tests

open NUnit.Framework
open FsUnit
open LocalNetwork
open LocalNetwork.DefaultOS

[<TestFixture>]
type LocalNetworkSimulatorTestClass () =

    let nonInfectiousOS = {
        new IOperationSystem with 
            member this.Name = "NonInfectious"
            member this.InfectionProbability = 0.
    }

    let contagiousOS = {
        new IOperationSystem with 
            member this.Name = "Contagious"
            member this.InfectionProbability = 1.
    }

    [<Test>]
    member this.``Net without infection on start should not be infected`` () =
        let computers = Array.create 4 (Computer windowsOS)
        let network = [
            [1; 2; 3]
            [0; 2; 3]
            [0; 1; 3]
            [0; 1; 2]
        ]
        LocalNetworkSimulator(computers, network).Start 10 |> should be False

    [<Test>]
    member this.``Net with contagious nodes should be infected on correct step`` () =
        let computers = 
            [|
                Computer(contagiousOS, true);
                Computer(contagiousOS);
                Computer(contagiousOS);
                Computer(contagiousOS);
                Computer(contagiousOS);
                Computer(contagiousOS);
                Computer(contagiousOS)
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
        LocalNetworkSimulator(computers, network).Start 3 |> should be True 

    [<Test>]
    member this.``Net with contagious nodes should not be infected on incorrect step`` () =
        let computers = 
            [|
                Computer(contagiousOS, true);
                Computer(contagiousOS);
                Computer(contagiousOS);
                Computer(contagiousOS);
                Computer(contagiousOS);
                Computer(contagiousOS);
                Computer(contagiousOS)
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
        LocalNetworkSimulator(computers, network).Start 2 |> should be False

    [<Test>]
    member this.``[N-n-w] should not be infected`` () =
        let computers = 
            [|
                Computer(nonInfectiousOS, true);
                Computer(nonInfectiousOS);
                Computer(windowsOS)
            |]
        let network = [
            [1]
            [0; 2]
            [1]
        ]
        LocalNetworkSimulator(computers, network).Start 10 |> should be False

    [<Test>]
    member this.``[C-c n] should not be infected`` () =
        let computers = 
            [|
                Computer(contagiousOS, true);
                Computer(contagiousOS);
                Computer(nonInfectiousOS)
            |]
        let network = [
            [1]
            [0]
            []
        ]
        LocalNetworkSimulator(computers, network).Start 10 |> should be False

    [<Test>]
    member this.``[N-c-c] should be infected`` () =
        let computers = 
            [|
                Computer(nonInfectiousOS, true);
                Computer(contagiousOS);
                Computer(contagiousOS)
            |]
        let network = [
            [1]
            [0; 2]
            [1]
        ]
        LocalNetworkSimulator(computers, network).Start 10 |> should be True