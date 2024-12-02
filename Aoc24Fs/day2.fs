module day2

open System.IO


let file =
    File.ReadAllLines "in.txt"
    |> Array.toList
    |> List.map(_.Split())
    |> List.map (Array.toList >> List.map int)

let check (line:int list) =
    let gF a b = if line[0] < line[1] then a < b else b < a
    List.pairwise line
    |> List.forall (fun (a,b) -> gF a b && abs (b-a) < 4)

let hardBrute (line:int list) =
    let arr = List.toArray line
    List.mapi (fun i x -> Array.removeAt i arr) line
    |> List.map (Array.toList)
    |> List.exists (check)

file
|> List.map hardBrute
|> List.sumBy (fun x -> if x then 1 else 0)
|> printfn "%i"