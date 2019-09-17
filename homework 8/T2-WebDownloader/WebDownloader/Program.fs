namespace WebDownloader

module Name = 
    open System
    open System.IO
    open System.Net
    open System.Text.RegularExpressions

    let download (url: string) = 
        async {
            // try
                let request = WebRequest.Create url :?> HttpWebRequest
                let! response = request.AsyncGetResponse () 
                use httpResponse = response :?> HttpWebResponse
                use stream = httpResponse.GetResponseStream ()
                use reader = new StreamReader(stream)
                let content = reader.ReadToEnd ()

                return content
            // with 
            // | :? WebException e -> ...
        }

    let searchLinksInHtml page = 
        let pattern = "<a\s[^>]*href=(\"??)([^\" >]*?)\\1[^>]*>(.*)<\/a>"
        let regex = Regex(pattern, RegexOptions.IgnoreCase) 
        let matches = regex.Matches page
        matches

    [<EntryPoint>]
    let main argv =
        let uri = Uri
        "https://stackoverflow.com/questions/499345/regular-expression-to-extract-url-from-an-html-link"
        |> download
        |> Async.RunSynchronously
        |> searchLinksInHtml
        |> (fun (x: MatchCollection) -> seq {for i in x -> i.Groups.[0].Value})
        |> Seq.distinct
        |> Seq.iter (fun x -> printfn "%s" x)
        0 
