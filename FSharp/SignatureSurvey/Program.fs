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

let signatureOf path = path |> File.ReadAllText |> signature

let generatedWpfCode = [".g.i.cs" ; ".g.cs"]
let resharper = [ "_ReSharper"] 

let skipGenerated (path:string) =  
    generatedWpfCode @ resharper
    |> List.exists (fun pattern -> path.Contains pattern)

let rec walk exclude pattern path = 
    seq { 
        for file in Directory.GetFiles(path, pattern, SearchOption.AllDirectories) do 
        if not <| exclude file 
        then yield file 
    } 

let searchCodeFilesIn = walk skipGenerated "*.cs"

let fileNameAndSignature path = (Path.GetFileName path, signatureOf path, (File.ReadAllLines path).Length) 

let first (a,_,_) = a;;

let surveyFor path = 
    searchCodeFilesIn path 
    |> Seq.map fileNameAndSignature
    |> Seq.sortBy first

[<EntryPoint>]
let main argv = 
    let path = argv.[0]
    for filename,signature,loc in surveyFor path do
        printf "%s " filename
        printf "(%i): " loc
        printfn "%s" signature
    0

