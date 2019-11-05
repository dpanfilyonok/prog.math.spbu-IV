namespace Phonebook
open System.Runtime.Serialization.Formatters.Binary

module Logic = 
    open System.IO
    
    type Phonebook = Phonebook of Map<string, string>

    let empty = Phonebook Map.empty 

    let addRecord name phone (Phonebook phonebook) = 
        Map.add name phone phonebook |> Phonebook

    let tryFindPhone name (Phonebook phonebook) = 
        Map.tryFind name phonebook

    let tryFindName phone (Phonebook phonebook) = 
        Map.tryFindKey (fun _ value -> value = phone) phonebook

    let printFBook (Phonebook phonebook) = 
        phonebook
        |> Map.iter (fun k v ->
            printfn "%s : %s" k v)

    let saveToFile path (Phonebook phonebook) =
        use outStream = new FileStream(path, FileMode.Create)
        let formatter = BinaryFormatter()
        formatter.Serialize (outStream, phonebook)

    let loadFromFile path = 
        use inStream = new FileStream(path, FileMode.Open)
        let formatter = BinaryFormatter()
        unbox<Phonebook>(formatter.Deserialize inStream)
