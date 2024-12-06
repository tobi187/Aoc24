module day4

open System.IO

let file = File.ReadAllLines "in.txt"

let len = file.Length

//let grid = Array2D.init len len (fun x y -> file[y][x])
let grid = array2D file

let get x y =
    if x < 0 || x >= len || y < 0 || y >= len
    then '#' else grid[y, x]

let rec walk cx cy dx dy (r:string) =
    let nx, ny = cx+dx, cy+dy
    let nr = r + string (get nx ny)
    match r with
    | "XMAS" -> 1
    | _ when "XMAS".StartsWith r -> walk nx ny dx dy nr
    | _ -> 0

let collect sx sy =
    [
        // 1,0;0,1;-1,0;0,-1;1,1;-1,-1;-1,1;1,-1
        for dx,dy in [1,0;0,1;1,1;-1,1] do
            yield walk sx sy dx dy "X"
            yield walk sx sy (dx* -1) (dy* -1) "X"
    ]
    |> List.sum

let collectPartTwoHandy sx sy =
    let d1 = new string([|get (sx-1) (sy-1); 'A'; get (sx+1) (sy+1)|] |> Array.sort) 
    let d2 = new string([|get (sx-1) (sy+1); 'A'; get (sx+1) (sy-1)|] |> Array.sort) 
    let g1 = new string([|get sx (sy-1); 'A'; get sx (sy+1)|] |> Array.sort) 
    let g2 = new string([|get (sx-1) sy; 'A'; get (sx+1) sy|] |> Array.sort) 
    let a = if (g1 = "AMS" && g2 = "AMS") then 1 else 0
    let b = if (d1 = "AMS" && d2 = "AMS") then 1 else 0
    b

[
    for y in [0..len - 1] do
        for x in [0..len - 1] do 
            match get x y = 'A' with
            | true -> yield collectPartTwoHandy x y 
            | false -> yield 0
] 
|> List.sum
|> printfn "%i"

