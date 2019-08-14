namespace Tasks
open System

module ArithmeticParseTree = 

    let map f (x, y) = f x, f y
    let uncurry f (x, y) = f x y
    let curry f x y = f (x, y)

    /// Node of arithmetic parse tree
    type TreeNode = 
        | Num of float
        | Add of TreeNode * TreeNode
        | Sub of TreeNode * TreeNode
        | Mult of TreeNode * TreeNode
        | Div of TreeNode * TreeNode

    /// Evaluate value of arithmetic parse tree
    let rec evaluateTree tree = 
        let applyToEvaluatedPair f x y = curry (map evaluateTree) x y |> uncurry (f)
        let epsilon = 1e-7
        match tree with 
        | Num x -> x
        | Add(l, r) -> applyToEvaluatedPair (+) l r
        | Sub(l, r) -> applyToEvaluatedPair (-) l r
        | Mult(l, r) -> applyToEvaluatedPair (*) l r
        | Div(l, r) ->
            if abs <| evaluateTree r < epsilon then raise <| DivideByZeroException()
            else applyToEvaluatedPair (/) l r
      