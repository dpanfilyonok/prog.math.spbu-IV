namespace LambdaCalculus 

/// Non-typed lambda calculus interpreter
module Interpreter = 
    open Utils.Functions

    /// Lambda variable type
    type VarType = string

    /// List of available literals for use as variables
    let alphabet : VarType list = ['a' .. 'z'] |> List.map string

    /// Lambda term
    type LambdaTerm = 
        | Variable of VarType
        | Application of LambdaTerm * LambdaTerm
        | Abstraction of VarType * LambdaTerm
    with 
        /// String format overriding
        override this.ToString () = 
            match this with 
            | Variable x -> sprintf "%s" x
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
            let variables = alphabet |> Set.ofList
            match this with
            | Abstraction(abstractionVariable, abstractionBody) -> 
                let avaliableForRanaming = (variables |> Set.difference) <| 
                                            (except |> Set.union abstractionBody.FreeVariables)
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
        | Application(left, right) -> 
            match left with
            | Abstraction _ -> left.ApplyBetaConversion right
            | _ -> Application(reduceByNormalStrategy left, reduceByNormalStrategy right)
        | Abstraction(abstractionVariable, abstractionBody) -> 
            Abstraction(abstractionVariable, reduceByNormalStrategy abstractionBody)
    
    /// Reduce term to normal form
    let rec reduceToNormalForm (term: LambdaTerm) = 
        // сюда бы передавать стратегию редукции ...
        if term = reduceByNormalStrategy term then term
        else reduceToNormalForm <| reduceByNormalStrategy term

    /// Abstraction builder
    let (^/) var term = Abstraction(var, term)

    /// Application builder
    let (|@) n m = Application(n, m)

    /// Prefix operator for Variable build
    // let (~&) x = Variable x

    /// Short literal for Variable build
    let v x = Variable x

    /// Map all variables in term with given function
    let rec private performMappingOnAllVariablesInTerm (func: VarType -> VarType) (term: LambdaTerm) = 
        match term with
        | Variable x -> Variable <| func x
        | Application(left, right) -> 
            Application(performMappingOnAllVariablesInTerm func left, 
                        performMappingOnAllVariablesInTerm func right)
        | Abstraction(abstractionVariable, abstractionBody) -> 
            Abstraction(func abstractionVariable, 
                        performMappingOnAllVariablesInTerm func abstractionBody)

    /// Alpha reduce all abstractions in term
    let rec private performAlphaConversionOnAllAbstractionsInTerm (term: LambdaTerm) = 
        match term with
        | Variable _ -> term
        | Application(left, right) -> 
            Application(performAlphaConversionOnAllAbstractionsInTerm left, 
                        performAlphaConversionOnAllAbstractionsInTerm right)
        | Abstraction(abstractionVariable, abstractionBody) -> 
            let newAbstractionBody = performAlphaConversionOnAllAbstractionsInTerm abstractionBody
            let newAbstraction = Abstraction(abstractionVariable, newAbstractionBody)
            newAbstraction.ApplyAlphaConversion newAbstractionBody.BoundVariables
 
    /// Check if 2 terms are alpha equivalent
    let areAlphaEquivalent (termA: LambdaTerm) (termB: LambdaTerm) = 
        let renameAllVariables = performMappingOnAllVariablesInTerm (fun x -> x + "'")
        (termA, termB)
        |> Pair.mapPair renameAllVariables
        |> Pair.mapPair performAlphaConversionOnAllAbstractionsInTerm
        |> Pair.uncurry (=)
        