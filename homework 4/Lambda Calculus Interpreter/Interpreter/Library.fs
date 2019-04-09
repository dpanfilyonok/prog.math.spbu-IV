namespace LambdaCalculus 

module Interpreter = 

    type VarType = char

    type LambdaTerm = 
        | Variable of VarType
        | LApplication of LambdaTerm * LambdaTerm
        | Abstraction of VarType * LambdaTerm
    
    // let (|NA|_|) (lambdaTerm: LambdaTerm) = 
    //     match lambdaTerm with 
    //     | Variable _ -> Some NA
    //     | LApplication _ -> Some NA
    //     | _ -> None

    // let (|NANF|_|) (lambdaTerm: LambdaTerm) = 
    //     match lambdaTerm with 
    //     | Variable _ -> Some NANF
    //     | LApplication(m, n) -> 
    //         match m with
    //         | NANF -> 
            

    // let (|NF|_|) (lambdaTerm: LambdaTerm) = 
    //     match lambdaTerm with 
    //     | Abstraction(_, term) -> 
    //         match term with 
    //         | NF -> Some NF
    //         | _ -> None
    //     | NANF -> Some NANF
    //     | _ -> None


    let applyBetaConversion (abstr: LambdaTerm, term: LambdaTerm) = ()

    let rec reductToNormalForm (lambdaTerm: LambdaTerm) = 
        match lambdaTerm with
        | Variable x -> Variable x
        | Abstraction(var, term) -> Abstraction(var, reductToNormalForm term)
        | LApplication(l, r) -> 
            if l :? Abstraction then applyBetaConversion l r
            else LApplication(reductToNormalForm l, reductToNormalForm r)


    