namespace LambdaCalculus 

module Interpreter = 

    type VarType = string

    type LambdaTerm = 
        | Variable of VarType
        | Application of LambdaTerm * LambdaTerm
        | Abstraction of VarType * LambdaTerm

    let rec getFreeVariables (term: LambdaTerm) = 
        match term with 
        | Variable x -> Set.ofList [x]
        | Application(m, n) -> 
            Set.union (getFreeVariables m) (getFreeVariables n)
        | Abstraction(abstrVar, term) -> Set.difference (getFreeVariables term) (Set.ofList [abstrVar])
        
    let rec applyAlphaConversion (term: LambdaTerm) =
        match term with
        | Abstraction(abstrVar, abstrTerm) -> 
            let newVar = abstrVar + "'"
            Abstraction(newVar, applySubstitution abstrTerm abstrVar (Variable newVar))
        | _ -> failwith "sss"
    and applySubstitution (target: LambdaTerm) (var: VarType) (source: LambdaTerm) = 
        match target with
        | Variable x when x = var -> source
        | Variable x when x <> var -> target
        | Application(n, m) -> Application(applySubstitution n var source, applySubstitution m var source)
        | Abstraction(abstrVar, _) when abstrVar = var -> target
        | Abstraction(abstrVar, _) when abstrVar <> var ->
            let newTarget = 
                if getFreeVariables source |> Set.contains abstrVar then applyAlphaConversion target
                else target 
            match newTarget with
            | Abstraction(abstrVar, abstrTerm) -> Abstraction(abstrVar, applySubstitution abstrTerm var source)
            | _ -> failwith "sss"
        | _ -> failwith "sss"

    let applyBetaConversion (abstr: LambdaTerm) (term: LambdaTerm) = 
        match abstr with
        | Abstraction(abstrVar, abstrTerm) -> applySubstitution abstrTerm abstrVar term
        | _ -> failwith "sss"

    let rec reductToNormalForm (term: LambdaTerm) = 
        match term with
        | Variable x -> Variable x
        | Abstraction(abstrVar, abstrTerm) -> Abstraction(abstrVar, reductToNormalForm abstrTerm)
        | Application(left, right) -> 
            match left with
            | Abstraction _ -> applyBetaConversion left right
            | _ -> Application(reductToNormalForm left, reductToNormalForm right)


    