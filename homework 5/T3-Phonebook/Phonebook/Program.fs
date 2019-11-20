namespace Phonebook

module Main = 
    open System
    open System.IO
    open Phonebook

    let printHelp () =
        let helpString = "\
            1 - exit\n\
            2 - add new record\n\
            3 - find phone by name\n\
            4 - find name by phone\n\
            5 - print all records\n\
            6 - export to file\n
            7 - import from file\n"
            
        printf "%s" helpString

    [<EntryPoint>]
    let main argv =
        let mutable phonebook = empty
        let rec mainLoop needContinue  = 
            if not needContinue then ()
            else
                let mutable flag = true

                printf " \nEnter command: "
                match Console.ReadLine() with
                | "1" -> 
                    printf "Session finished! \n"
                    flag <- false
                    
                | "2" ->
                    printf "-- enter name: "
                    let name = Console.ReadLine()

                    printf "-- enter phone number: "
                    let number = Console.ReadLine()

                    phonebook <- addRecord name number phonebook
                    printf "Record successfully added! \n"
                   
                | "3" -> 
                    printf "-- enter name: "
                    match (tryFindPhone <| Console.ReadLine () <| phonebook) with
                    | Some phone -> printfn "phone: %s" phone
                    | None -> printfn "Phone not found"
                        
                | "4" -> 
                    printf "-- enter phone number: "
                    match (tryFindName <| Console.ReadLine () <| phonebook) with
                    | Some name -> printfn "name: %s" name
                    | None -> printfn "Name not found"

                | "5" -> printFBook phonebook       

                | "6" ->
                    printf "-- enter filepath: "
                    try
                        let filepath = Console.ReadLine ()
                        saveToFile filepath phonebook
                        printf "Records successfully exported to %s \n" filepath
                    with
                    | :? DirectoryNotFoundException as e 
                        -> printf "%s \n" e.Message

                | "7" -> 
                    printf "-- enter filepath: "
                    let filepath = Console.ReadLine ()
                    phonebook <- loadFromFile filepath

                | _ -> 
                    printf "Wrong input. Please try again ... \n"
                    printHelp ()

                mainLoop flag
        printHelp ()
        mainLoop true
        0 