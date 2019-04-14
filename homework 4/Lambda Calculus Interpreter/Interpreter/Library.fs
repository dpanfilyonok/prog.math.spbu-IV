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
            | Abstraction(abstractionVariable, abstractionTerm) -> 
                (abstractionTerm.FreeVariables |> Set.difference) <| Set.ofList [abstractionVariable] 

        member this.ApplyAlphaConversion () =
            match this with
            | Abstraction(abstractionVariable, abstractionTerm) -> 
                "`"
                |> (+) abstractionVariable
                |> (fun x -> Abstraction(x, abstractionTerm.ApplySubstitution abstractionVariable (Variable x)))
            | _ -> failwith "Term is not lambda abstraction, only them could be alpha reduced"
            
        member this.ApplySubstitution (targetVariable: VarType) (sourceTerm: LambdaTerm) = 
            match this with
            | Variable x when x = targetVariable -> sourceTerm
            | Variable x when x <> targetVariable -> this
            | Application(left, right) -> 
                Application(left.ApplySubstitution targetVariable sourceTerm, right.ApplySubstitution targetVariable sourceTerm)
            | Abstraction(abstractionVariable, _) when abstractionVariable = targetVariable -> this
            | Abstraction(abstractionVariable, _) when abstractionVariable <> targetVariable ->
                let alphaReducedTerm = 
                    if sourceTerm.FreeVariables |> Set.contains abstractionVariable 
                        then this.ApplyAlphaConversion ()
                    else this 
                match alphaReducedTerm with
                | Abstraction(abstractionVariable, abstractionTerm) -> 
                    Abstraction(abstractionVariable, abstractionTerm.ApplySubstitution targetVariable sourceTerm)
                | _ -> failwith "Term after alpha conversion should be lambda abstraction"
            | _ -> failwith "Something went wrong in term substitution"

    let applyBetaConversion (abstraction: LambdaTerm) (term: LambdaTerm) = 
        match abstraction with
        | Abstraction(abstractionVariable, abstractionTerm) -> 
            abstractionTerm.ApplySubstitution abstractionVariable term
        | _ -> failwith "Term is not lambda abstraction, only them could be alpha reduced"

    let rec reductToNormalForm (term: LambdaTerm) = 
        match term with
        | Variable x -> Variable x
        | Abstraction(abstractionVariable, abstractionTerm) -> 
            Abstraction(abstractionVariable, reductToNormalForm abstractionTerm)
        | Application(left, right) -> 
            match left with
            | Abstraction _ -> applyBetaConversion left right
            | _ -> Application(reductToNormalForm left, reductToNormalForm right)
