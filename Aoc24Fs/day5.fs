module day5

open System.IO

let [| rules1; pages1 |] = 
    (File.ReadAllText "in.txt").Split "\r\n\r\n"
    |> Array.map (_.Split("\r\n"))
    |> Array.map (Array.toList)

let rules = 
    rules1 
    |> List.map (_.Split("|"))
    |> List.map (Array.map int)
    |> List.map (fun [|f;s|] -> f,s)
    |> readOnlyDict

