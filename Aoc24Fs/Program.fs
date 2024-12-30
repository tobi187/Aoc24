open System.IO

let file = (File.ReadAllText "in.txt").Split() |> Array.toList |> List.map (decimal)

let blink = function
    | 0m -> [1m]
    | x when (string x).Length % 2 = 0 -> 
        let str = string x
        let l = str.Length / 2
        [decimal str[..(l - 1)]; decimal str[l..]]
    | x -> [x * 2024m]


let rec blinkAll arr cutoff =
    printfn "%i" cutoff
    match cutoff with
    | 75 -> List.length arr
    | _ ->
        let nl =
            List.map (blink) arr
            |> List.concat
        blinkAll nl (cutoff + 1)


blinkAll file 0
|> printfn "%i"