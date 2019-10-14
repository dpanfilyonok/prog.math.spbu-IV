namespace WebDownloader

module Main = 
    open System
    open System.IO
    open System.Net
    open System.Text.RegularExpressions
    open Utils.Functions

    /// Builds async, that returns page content of a given url
    let getContenAsync (url: string) = 
        async {
            try
                let request = WebRequest.Create url
                let! response = request.AsyncGetResponse () 
                use stream = response.GetResponseStream ()
                use reader = new StreamReader(stream)
                let content = reader.ReadToEnd ()
                return Some content   
            with 
            | _ -> return None
        }

    /// Returns http and https links from html page (using regex)
    let getLinksFromHtml page = 
        let pattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))"
        let regex = Regex(pattern, RegexOptions.IgnoreCase) 
        let uri = Uri "https://www.google.ru/"

        page
        |> regex.Matches 
        |> (fun (matches: MatchCollection) -> seq {for i in matches -> i.Groups.[1].Value})
        |> Seq.distinct
        |> Seq.filter 
            (fun string -> 
                Uri.TryCreate (string, UriKind.Absolute, ref uri) && 
                (uri.Scheme = Uri.UriSchemeHttp || uri.Scheme = Uri.UriSchemeHttps)
            )

    /// Prints content length of all nested pages
    [<EntryPoint>]
    let main argv = 
        let startUri = "https://stackoverflow.com/"
        let content = 
            startUri
            |> getContenAsync
            |> Async.RunSynchronously

        if content.IsNone then
            printfn "Page %s isn`t accessible" startUri
        else 
            content.Value
            |> getLinksFromHtml
            |> Pair.replicate
            |> Pair.mapSnd (Seq.map getContenAsync)
            |> Pair.mapSnd Async.Parallel
            |> Pair.mapSnd Async.RunSynchronously
            |> Pair.mapSnd Seq.ofArray
            |> Pair.uncurry Seq.zip
            |> Seq.choose 
                (fun (uri, contentOpt) -> 
                    match contentOpt with 
                    | None -> None 
                    | Some content -> Some (uri, content)
                )
            |> Seq.map (fun (uri, content) -> (uri, String.length content))
            |> Seq.iter (fun (uri, length) -> printfn "%s --- %i" uri length)

        0 