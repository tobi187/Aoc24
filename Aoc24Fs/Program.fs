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
    |> List.groupBy fst
    |> List.map (fun (f, s) -> f, List.map (snd) s)
    |> readOnlyDict

let checkP1 line =
    [
        let iList = List.indexed line 
        for i, l in iList do
            let f, v = rules.TryGetValue l
            if not f then yield true else
            let nl = List.filter (fun (f,s) -> List.contains s v) iList |> List.sort |> List.tryHead
            match nl with
            | Some (i2,_) when i2 > i -> yield true
            | Some _ -> yield false
            | None -> yield true
    ]
    |> List.forall ((=)true)
    |> fun t -> if t then line[line.Length/2] else 0

let findFails line =
    let r = [
        let iList = List.indexed line 
        for i, l in line |> List.indexed do
            let f, v = rules.TryGetValue l
            if not f then () else 
            let nl = List.filter (fun (f,s) -> List.contains s v) iList |> List.sort
            match List.tryHead nl with
            | Some (i2,_) when i2 > i -> ()
            | Some (i2,_) -> yield (i, i2) 
            | None -> ()
    ]
    if List.length r = 0 then None else 
    let (r1,r2) = List.head r
    Some (r1, r2, line)

let rec fixLines (i1, i2, (line: int list)) =
    let swap i e = match i with | _ when i = i1 -> line[i2] | _ when i = i2 -> line[i1] | _ -> e
    let swapped = List.mapi (swap) line
    match findFails swapped with
    | Some x -> fixLines x
    | None -> swapped[line.Length / 2]

pages1
|> List.map(_.Split(","))
|> List.map(Array.toList >> List.map int)
|> List.choose (findFails)
|> List.map (fixLines)
|> List.sum
|> printfn "%i"
