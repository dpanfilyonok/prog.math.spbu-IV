namespace LambdaCalculus 

module Interpreter = 

    type VarType = char

    type LambdaTerm = 
        | Variable of VarType
        | Application of LambdaTerm * LambdaTerm
        | Abstraction of VarType * LambdaTerm
    
    let applyBetaConversion (abstr: LambdaTerm, term: LambdaTerm) = ()
        

    let rec reductToNormalForm (lambdaTerm: LambdaTerm) = 
        match lambdaTerm with
        | Variable x -> Variable x
        | Abstraction(var, term) -> Abstraction(var, reductToNormalForm term)
        | Application(l, r) -> 
            if l :? Abstraction then applyBetaConversion l r
            else Application(reductToNormalForm l, reductToNormalForm r)


    