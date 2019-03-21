namespace Tests

open NUnit.Framework
open FsUnit

open Tasks.MapBinaryTree

[<TestFixture>]
type MapBinaryTreeTest () =

    let c f x y = f y x

    let emptyTree = Empty
    let rootTree = Tree(-5, Empty, Empty)
    let balancedTree =  Tree(1, 
                            Tree(2, 
                                Tree(4, Empty, Empty), 
                                Tree(5, Empty, Empty)), 
                            Tree(3, 
                                Tree(6, Empty, Empty), 
                                Tree(7, Empty, Empty)))
    let flatternTree = Tree(1, Tree(2, Tree(3, Tree(4, Tree(5, Empty, Empty), Empty), Empty), Empty), Empty)

    [<Test>]
    member this.``mapping emptyTree with id should return same tree`` () =
        emptyTree |> mapBinaryTree id |> should equal emptyTree

    [<Test>]
    member this.``mapping rootTree with id should return same tree`` () =
        rootTree |> mapBinaryTree id |> should equal rootTree

    [<Test>]
    member this.``mapping balancedTree with id should return same tree`` () =
        balancedTree |> mapBinaryTree id |> should equal balancedTree

    [<Test>]
    member this.``mapping flatternTree with id should return same tree`` () =
        flatternTree |> mapBinaryTree id |> should equal flatternTree


    [<Test>]
    member this.``mapping emptyTree with pown2 should return empty tree`` () =
        emptyTree |> mapBinaryTree (c pown 2) |> should equal emptyTree

    [<Test>]
    member this.``mapping rootTree with pown2 should return correct tree`` () =
        let expected = Tree(25, Empty, Empty)
        rootTree |> mapBinaryTree (c pown 2) |> should equal expected

    [<Test>]
    member this.``mapping balancedTree with pown2 should return correct tree`` () =
        let expected = Tree(1, 
                            Tree(4, 
                                Tree(16, Empty, Empty), 
                                Tree(25, Empty, Empty)), 
                            Tree(9, 
                                Tree(36, Empty, Empty), 
                                Tree(49, Empty, Empty)))
        balancedTree |> mapBinaryTree (c pown 2) |> should equal expected

    [<Test>]
    member this.``mapping flatternTree with pown2 should return correct tree`` () =
        let expected = Tree(1, Tree(4, Tree(9, Tree(16, Tree(25, Empty, Empty), Empty), Empty), Empty), Empty)
        flatternTree |> mapBinaryTree (c pown 2) |> should equal expected