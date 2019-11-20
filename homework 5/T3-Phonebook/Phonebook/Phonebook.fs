namespace Phonebook

/// Phonebook type, with UNIQUE(KEY, VALUE) constraint
type Phonebook = Phonebook of Map<string, string>

module Phonebook = 
    open System.IO
    open System.Runtime.Serialization.Formatters.Binary

    /// Returns empty notebook
    let empty = Phonebook Map.empty

    /// Check if notebook is empty
    let isEmpty (Phonebook phonebook) = Map.isEmpty phonebook

    /// Returns amount of records in phonebook
    let count (Phonebook phonebook) = Map.count phonebook

    /// Convert phonebook to list of records
    let toList (Phonebook phonebook) = Map.toList phonebook

    /// Add new record to phonebook and returns it
    /// (rewrite record with the same name; don`t add, if phone is exists)
    let addRecord name phone (Phonebook phonebook) =
        if phonebook |> Map.exists (fun _ value -> value = phone) then
            phonebook |> Phonebook
        else 
            Map.add name phone phonebook |> Phonebook

    /// Try find phone by name
    let tryFindPhone name (Phonebook phonebook) = 
        Map.tryFind name phonebook

    /// Try find name by phone
    let tryFindName phone (Phonebook phonebook) = 
        Map.tryFindKey (fun _ value -> value = phone) phonebook

    /// Print phonebook to console
    let printFBook (Phonebook phonebook) = 
        phonebook
        |> Map.iter (fun k v ->
            printfn "%s : %s" k v)

    /// Save phonebook to binary file
    let saveToFile path (Phonebook phonebook) =
        use outStream = new FileStream(path, FileMode.Create)
        let formatter = BinaryFormatter()
        formatter.Serialize (outStream, phonebook)

    /// Load phonebook from binary file
    let loadFromFile path = 
        use inStream = new FileStream(path, FileMode.Open)
        let formatter = BinaryFormatter()
        unbox<Map<string, string>>(formatter.Deserialize inStream) |> Phonebook
