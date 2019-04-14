namespace LambdaCalculus 

module LambdaBooleans = 
    open Interpreter
    open LambdaCombinators

    let tru = Abstraction("t", Abstraction("f", Variable "t"))
    let fls = Abstraction("t", Abstraction("f", Variable "f"))

    let iif = 
        Abstraction("b", 
            Abstraction("x", 
                Abstraction("y", 
                    Application(
                        Application(Variable "b", Variable "x"), 
                        Variable "y"))))

                     


                     