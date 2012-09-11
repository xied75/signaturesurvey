open System
open System.IO

let interestingCharacters char = 
    match char with 
    | ';' | '{' | '}' -> true 
    | _ -> false

let signature (content:string) = 
    content 
    |> Seq.filter interestingCharacters 
    |> String.Concat

let content path = File.ReadAllText(path) 

let signatureOf path = path |> content |> signature

let generatedCode = [".g.i.cs" ; ".g.cs"]
let resharper = [ "_ReSharper"] 

let skip (path:string) =  
    generatedCode @ resharper
    |> List.exists (fun pattern -> path.Contains pattern)

let rec walk exclude pattern path = 
    seq { 
        for file in Directory.GetFiles(path, pattern, SearchOption.AllDirectories) do 
        if not <| exclude file 
        then yield file 
    } 

let searchCodeFilesIn = walk skip "*.cs"

let fileNameAndFile path = (Path.GetFileName path, signatureOf path)

let surveyFor path = 
    searchCodeFilesIn path 
    |> Seq.map fileNameAndFile
    |> Seq.sortBy (fst)

[<EntryPoint>]
let main argv = 
    let path = "D:\\Solutions\\Current\Okra"
    for filename,signature in surveyFor path do // Side effects go here
        printf "%s " filename
        printfn "%s" signature
    0 // return an integer exit code

