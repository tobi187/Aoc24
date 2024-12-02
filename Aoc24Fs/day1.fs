module day1

open System
open System.IO

File.ReadAllLines "in.txt"
|> Array.toList
|> List.map (_.Split(' ', StringSplitOptions.RemoveEmptyEntries))
|> List.map (Array.toList >> List.map int)
|> List.map (fun [a;b] -> a,b)
|> List.unzip
|> fun (a,b) -> [a;b]
|> List.map (List.sortDescending)
|> fun [a;b] -> List.zip a b
|> List.sumBy (fun (f, s) -> abs (f - s))
|> printfn "first: %i"


File.ReadAllLines "in.txt"
|> Array.toList
|> List.map (_.Split(' ', StringSplitOptions.RemoveEmptyEntries))
|> List.map (Array.toList >> List.map int)
|> List.map (fun [a;b] -> a,b)
|> List.unzip
|> fun (a,b) -> a, (List.groupBy (id) b |> List.map (fun (k, v) -> k, List.length v) |> dict)
|> fun (f,s) -> List.sumBy (fun e -> e * if s.ContainsKey e then s[e] else 0) f
|> printfn "second %i"
                 
 