namespace Tests

open NUnit.Framework
open FsUnit
open Phonebook

[<TestFixture>]
type PhonebookTestClass () =

    [<Test>]
    member this.``Add record to empty phonebook should make book not empty`` () =
        Phonebook.empty 
        |> Phonebook.addRecord "name" "phone" 
        |> Phonebook.isEmpty 
        |> should be False 

    [<Test>]
    member this.``Add record with same name should overwrite prev record`` () =
        Phonebook.empty 
        |> Phonebook.addRecord "name" "phone" 
        |> Phonebook.addRecord "name" "phone1"
        |> Phonebook.tryFindPhone "name"
        |> should equal (Some "phone1")

    [<Test>]
    member this.``Record with same phone should not added`` () =
        Phonebook.empty 
        |> Phonebook.addRecord "name" "phone" 
        |> Phonebook.addRecord "name1" "phone"
        |> Phonebook.count
        |> should equal 1

    [<Test>]
    member this.``tryFindName should return None if record not exists`` () =
        Phonebook.empty 
        |> Phonebook.addRecord "name" "phone" 
        |> Phonebook.tryFindName "phone1"
        |> should equal None

    [<Test>]
    member this.``tryFindPhone should return None if record not exists`` () =
        Phonebook.empty 
        |> Phonebook.addRecord "name" "phone" 
        |> Phonebook.tryFindPhone "name1"
        |> should equal None

    [<Test>]
    member this.``loadFromFile should return same phonebook`` () =
        let phBook = 
            Phonebook.empty 
            |> Phonebook.addRecord "name" "phone" 
            |> Phonebook.addRecord "name" "phone1"
            |> Phonebook.addRecord "name1" "phone2"
        
        phBook |> Phonebook.saveToFile "./phonebook.data"

        "./phonebook.data"
        |> Phonebook.loadFromFile
        |> Phonebook.toList
        |> should equal (Phonebook.toList phBook)
