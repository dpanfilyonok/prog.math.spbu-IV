namespace Phonebook

module Main = 
    open System
    open System.IO
    open Logic
    open Utils.Functions

    let printHelp () =
        let helpString = "\
            1 - exit\n\
            2 - add new record\n\
            3 - find phone by name\n\
            4 - find name by phone\n\
            5 - print all records\n\
            6 - export to text file\n"
            
        printf "%s" helpString

    [<EntryPoint>]
    let main argv =
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

                    try 
                        addRecord (name, number)
                        printf "Record successfully added! \n"
                    with 
                    | :? RecordException as e -> printf "%s \n" e.Data0
                | "3" -> 
                    printf "-- enter name: "
                    (getPhonesByName <| Console.ReadLine ())
                        |> List.iter (printf "-> %s \n")
                | "4" -> 
                    printf "-- enter phone number: "
                    (getNameByPhone <| Console.ReadLine ())
                        |> List.iter (printf "-> %s \n")
                | "5" -> 
                    (getAllRecords ())
                        |> List.iter (fun record -> Pair.uncurry <| printf "-> %s : %s \n" <| record )                
                | "6" ->
                    printf "-- enter filepath: "
                    try
                        let filepath = Console.ReadLine ()
                        use file = File.CreateText(filepath)
                        (getAllRecords ())
                            |> List.iter (fun record -> Pair.uncurry <| fprintf file "%s : %s \n" <| record )
                        printf "Records successfully exported to %s \n" filepath
                    with
                    | :? DirectoryNotFoundException as e 
                        -> printf "%s \n" e.Message
                | _ -> 
                    printf "Wrong input. Please try again ... \n"
                    printHelp ()
                mainLoop flag
        printHelp ()
        mainLoop true
        0 