namespace Tasks

module MapBinaryTree = 

    /// Binary tree
    type 'a Tree = 
        | Tree of 'a * 'a Tree * 'a Tree
        | Empty

    /// Map binary tree with a given function
    let rec mapBinaryTree func tree = 
        match tree with
        | Tree(value, left, right) -> Tree(func value, mapBinaryTree func left, mapBinaryTree func right)
        | Empty -> Empty