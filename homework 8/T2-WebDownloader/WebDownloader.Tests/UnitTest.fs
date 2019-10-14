namespace Tests

open NUnit.Framework
open FsUnit
open WebDownloader.Main

[<TestFixture>]
type WebDownloaderTestClass () =

    [<Test>]
    member this.``Getting content from invalid url should return None`` () =
        "InvalidSchema"
        |> getContentAsync
        |> Async.RunSynchronously
        |> should equal None 
