namespace Phonebook

module Logic = 
    open FSharp.Data.Sql

    exception RecordException of string

    [<Literal>]
    let ConnectionString = @"
        Server=tcp:dbspbu.database.windows.net,1433;
        Initial Catalog=Phonebook;
        Persist Security Info=False;
        User ID=anticnvm;
        Password=vbnz@vbnz1999;
        MultipleActiveResultSets=False;
        Encrypt=True;
        TrustServerCertificate=False;
        Connection Timeout=30;"

    type SqlConnection = SqlDataProvider<
                            Common.DatabaseProviderTypes.MSSQLSERVER, 
                            ConnectionString, 
                            UseOptionTypes = true>

    let ctx = SqlConnection.GetDataContext()

    let getAllRecords () = 
        query {
            for row in ctx.Dbo.Phonebook do
                select (row.FullName, row.PhoneNumber)
        } |> Seq.toList
    
    let addRecord (name: string, number: string) = 
        try 
            let newRow = ctx.Dbo.Phonebook.``Create(FullName, PhoneNumber)``(name, number)
            ctx.SubmitUpdates()
        with 
        | :? System.Data.SqlClient.SqlException as e 
            when e.Message.Contains "Violation of UNIQUE KEY constraint 'UQ_Record'" 
            -> raise <| RecordException "FAIL: Given record has already exist!"
        | :? System.Data.SqlClient.SqlException as e 
            when e.Message.Contains "The INSERT statement conflicted with the CHECK constraint \"CK__Phonebook__Phone__49C3F6B7\""
            -> raise <| RecordException "FAIL: Given record has a phone number consisting not only of numbers"

    let getPhonesByName name = 
        query {
            for record in ctx.Dbo.Phonebook do
                where (record.FullName = name)
                select record.PhoneNumber
        } |> Seq.toList
     
    let getNameByPhone number = 
        query {
            for record in ctx.Dbo.Phonebook do
                where (record.PhoneNumber = number)
                select record.FullName
        } |> Seq.toList
