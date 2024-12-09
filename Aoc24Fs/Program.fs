open System.IO

let parse (line:string) =
    let p = line.Split ": "
    let pp = p[1].Split ' ' |> Array.map int
    int p[0], List.ofArray pp

let file =
    File.ReadAllLines "in.txt"
    |> Array.toList
    |> List.map parse

let calc l =
    let f,s = l
    let combos =
        List.pairwise s
        |> List.map (fun (a,b) -> b*a)
        |> List.indexed
        |> List.sortBy snd
        |> List.map fst

    0