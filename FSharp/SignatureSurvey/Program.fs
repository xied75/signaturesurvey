open System
open System.IO

let (|~>) a b = b (List.ofSeq a)
let concat = String.Concat:char list->string

let interestingCharacters char = 
    match char with 
    | ';' | '{' | '}' -> true 
    | _ -> false

let isCodefile filename = 
    match Path.GetExtension filename with 
    | ".cs" -> true
    | _ -> false

let signature (content:string) = 
    content 
    |~> List.filter interestingCharacters 
    |> concat

let content path = 
    File.ReadAllText(path) 

let signatureEntryFor path = 
    let filename = Path.GetFileName path
    let signature = path |> content |> signature
    String.Format("{0}: {1}", filename, signature)    

// Select : List.map
// Where : List.filter




let rec walk exclude pattern path = 
    seq { 
        for file in Directory.GetFiles(path, pattern, SearchOption.AllDirectories) do if not <| exclude file then yield file 
    } 

let walkCodeFiles = walk (fun (path:string) -> path.Contains "_ReSharper") "*.cs"

// let walkCodeFiles "C:\Users\Johannes Hofmeister\Desktop\Float\okra" |> Seq.map Path.GetFileName;;

[<EntryPoint>]
let main argv = 
    let path = "C:\Users\Johannes Hofmeister\Desktop\Float\okra\Okra"
    for codefile in walk isCodefile path do 
        printfn "%s" codefile
    0 // return an integer exit code

