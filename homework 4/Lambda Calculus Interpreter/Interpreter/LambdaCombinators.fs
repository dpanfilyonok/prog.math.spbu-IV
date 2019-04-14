namespace LambdaCalculus 

module LambdaCombinators = 
    open Interpreter

    let s = 
        Abstraction("f", 
            Abstraction("g", 
                Abstraction("x", 
                    Application(
                        Application(Variable "f", Variable "x"), 
                        Application(Variable "g", Variable "x")))))

    let b = 
        Abstraction("f", 
            Abstraction("g", 
                Abstraction("x", 
                    Application(
                        Variable "f", 
                        Application(Variable "g", Variable "x")))))
                    
    let k = Abstraction("x", Abstraction("y", Variable "x"))
    let k' = Abstraction("x", Abstraction("y", Variable "y"))
    let i = Abstraction("x", Variable "x")