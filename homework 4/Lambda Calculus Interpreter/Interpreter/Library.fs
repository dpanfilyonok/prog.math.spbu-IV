namespace LambdaCalculus 

module Interpreter = 

    type VarType = string

    type LambdaTerm = 
        | Variable of VarType
        | Application of LambdaTerm * LambdaTerm
        | Abstraction of VarType * LambdaTerm
    with 
        member this.FreeVariables = 
            match this with 
            | Variable x -> Set.ofList [x]
            | Application(left, right) -> 
                (left.FreeVariables |> Set.union) <| right.FreeVariables
            | Abstraction(abstractionVar, abstractionTerm) -> 
                (abstractionTerm.FreeVariables |> Set.difference) <| Set.ofList [abstractionVar] 
        
    let rec applyAlphaConversion (term: LambdaTerm) =
        match term with
        | Abstraction(abstractionVar, abstractionTerm) -> 
            "`"
            |> (+) abstractionVar
            |> (fun x -> Abstraction(x, applySubstitution abstractionTerm abstractionVar (Variable x)))
        | _ -> failwith "sss"
    and applySubstitution (target: LambdaTerm) (var: VarType) (source: LambdaTerm) = 
        match target with
        | Variable x when x = var -> source
        | Variable x when x <> var -> target
        | Application(n, m) -> 
            Application(applySubstitution n var source, applySubstitution m var source)
        | Abstraction(abstractionVar, _) when abstractionVar = var -> target
        | Abstraction(abstractionVar, _) when abstractionVar <> var ->
            let newTarget = 
                if source.FreeVariables |> Set.contains abstractionVar 
                    then applyAlphaConversion target
                else target 
            match newTarget with
            | Abstraction(abstractionVar, abstractionTerm) -> 
                Abstraction(abstractionVar, applySubstitution abstractionTerm var source)
            | _ -> failwith "sss"
        | _ -> failwith "sss"

    let applyBetaConversion (abstr: LambdaTerm) (term: LambdaTerm) = 
        match abstr with
        | Abstraction(abstractionVar, abstractionTerm) -> 
            applySubstitution abstractionTerm abstractionVar term
        | _ -> failwith "sss"

    let rec reductToNormalForm (term: LambdaTerm) = 
        match term with
        | Variable x -> Variable x
        | Abstraction(abstractionVar, abstractionTerm) -> 
            Abstraction(abstractionVar, reductToNormalForm abstractionTerm)
        | Application(left, right) -> 
            match left with
            | Abstraction _ -> applyBetaConversion left right
            | _ -> Application(reductToNormalForm left, reductToNormalForm right)
