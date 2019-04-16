namespace LambdaCalculus 

/// Non-typed lambda calculus interpreter
module Interpreter = 

    /// Lambda variable type
    type VarType = char

    /// Lambda term
    type LambdaTerm = 
        | Variable of VarType
        | Application of LambdaTerm * LambdaTerm
        | Abstraction of VarType * LambdaTerm
    with 
        /// Abstraction builder
        static member (^/) (var, term) = Abstraction(var, term)

        /// Application builder
        static member (|@) (n, m) = Application(n, m)

        /// String format overriding
        override this.ToString () = 
            match this with 
            | Variable x -> sprintf "%c" x
            | Application(left, right) -> sprintf "(%s%s)" (left.ToString ()) (right.ToString ())
            | Abstraction(abstractionVariable, abstractionBody) -> 
                sprintf "(\\%s. %s)" (abstractionVariable.ToString ()) (abstractionBody.ToString ())

        /// Returns set of free variables of term
        member this.FreeVariables = 
            match this with 
            | Variable x -> Set.ofList [x]
            | Application(left, right) ->   
                (left.FreeVariables |> Set.union) <| right.FreeVariables
            | Abstraction(abstractionVariable, abstractionBody) -> 
                (abstractionBody.FreeVariables |> Set.difference) <| Set.ofList [abstractionVariable] 
        
        /// Returns set of bound variables of term
        member this.BoundVariables = 
            match this with
            | Variable _ -> Set.empty
            | Application(left, right) ->   
                (left.BoundVariables |> Set.union) <| right.BoundVariables
            | Abstraction(abstractionVariable, abstractionBody) -> 
                (abstractionBody.BoundVariables |> Set.union) <| Set.ofList [abstractionVariable]

        /// Applies alpha conversion to term
        member this.ApplyAlphaConversion (except: Set<VarType>) =
            let variables = ['a' .. 'z'] |> Set.ofList
            match this with
            | Abstraction(abstractionVariable, abstractionBody) -> 
                let avaliableForRanaming = (variables |> Set.difference) <| 
                                            except |> Set.union abstractionBody.FreeVariables
                avaliableForRanaming
                |> Set.maxElement
                |> (fun x -> 
                    Abstraction(x, abstractionBody.ApplySubstitution abstractionVariable (Variable x)))
            | _ -> failwith "Term is not lambda abstraction, only them could be alpha reduced"
         
        /// Performs beta conversion 
        member this.ApplyBetaConversion (term: LambdaTerm) = 
            match this with
            | Abstraction(abstractionVariable, abstractionBody) -> 
                abstractionBody.ApplySubstitution abstractionVariable term
            | _ -> failwith "Term is not lambda abstraction, only them could be alpha reduced"
            
        /// Applies substitution to term 
        member this.ApplySubstitution (targetVariable: VarType) (sourceTerm: LambdaTerm) = 
            match this with
            | Variable x when x = targetVariable -> sourceTerm
            | Variable x when x <> targetVariable -> this
            | Application(left, right) -> 
                Application(left.ApplySubstitution targetVariable sourceTerm, 
                            right.ApplySubstitution targetVariable sourceTerm)
            | Abstraction(abstractionVariable, _) when abstractionVariable = targetVariable -> this
            | Abstraction(abstractionVariable, _) when abstractionVariable <> targetVariable ->
                let alphaReducedTerm = 
                    if sourceTerm.FreeVariables |> Set.contains abstractionVariable 
                        then this.ApplyAlphaConversion sourceTerm.FreeVariables
                    else this 
                match alphaReducedTerm with
                | Abstraction(abstractionVariable, abstractionBody) -> 
                    Abstraction(abstractionVariable, 
                                abstractionBody.ApplySubstitution targetVariable sourceTerm)
                | _ -> failwith "Term after alpha conversion should be lambda abstraction"
            | _ -> failwith "Something went wrong in term substitution"

    /// Define and apply operation semantic of reduction by normal strategy
    let rec reduceByNormalStrategy (term: LambdaTerm) = 
        match term with
        | Variable x -> Variable x
        | Abstraction(abstractionVariable, abstractionBody) -> 
            Abstraction(abstractionVariable, reduceByNormalStrategy abstractionBody)
        | Application(left, right) -> 
            match left with
            | Abstraction _ -> left.ApplyBetaConversion right
            | _ -> Application(reduceByNormalStrategy left, reduceByNormalStrategy right)
    
    /// Reduce term to normal form
    let rec reduceToNormalForm (term: LambdaTerm) = 
        if term = reduceByNormalStrategy term then term
        else reduceToNormalForm <| reduceByNormalStrategy term

    /// Prefix operator for for Variable build
    let (~&) x = Variable x