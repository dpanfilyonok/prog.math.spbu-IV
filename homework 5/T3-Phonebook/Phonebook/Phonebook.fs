namespace Phonebook

module Logic = 
    open FSharp.Data.Sql

    exception RecordException of string

    [<Literal>]
    let ConnectionString = "Data Source=" + __SOURCE_DIRECTORY__  + @"/Phonebook.db;Version=3;"

    [<Literal>]
    let ResolutionStr = 
        @"/home/anticnvm/.nuget/packages/system.data.sqlite.core/1.0.111/lib/netstandard2.0"
        
    type SqlConnection = SqlDataProvider<
                            Common.DatabaseProviderTypes.SQLITE, 
                            ConnectionString = ConnectionString,
                            ResolutionPath = ResolutionStr,
                            SQLiteLibrary = Common.SQLiteLibrary.SystemDataSQLite>

    let ctx = SqlConnection.GetDataContext()

    let getAllRecords () = 
        query {
            for row in ctx.Main.Phonebook do
                select (row.FullName, row.PhoneNumber)
        } |> Seq.toList
    
    let addRecord (name: string, number: string) = 
        try 
            let newRow = ctx.Main.Phonebook.``Create(FullName, PhoneNumber)``(name, number)
            ctx.SubmitUpdates()
        with 
        | :? System.Data.SqlClient.SqlException as e 
            when e.Message.Contains "UNIQUE constraint failed" 
            -> raise <| RecordException "FAIL: Given phone number has already exist!"

    let getPhonesByName name = 
        query {
            for record in ctx.Main.Phonebook do
                where (record.FullName = name)
                select record.PhoneNumber
        } |> Seq.toList
     
    let getNameByPhone number = 
        query {
            for record in ctx.Main.Phonebook do
                where (record.PhoneNumber = number)
                select record.FullName
        } |> Seq.toList
