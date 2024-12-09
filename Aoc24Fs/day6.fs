module day6

open System.IO

type Tile = Empty | Out | Block | Used
type Dir = Right | Left | Up | Down

type P = {
    x: int
    y: int
    dir: Dir
}

let pp = { x = 89; y = 84; dir = Dir.Up }

let getObst ind line =
    Seq.toList line
    |> List.indexed 
    |> List.filter (fun (_,b) -> b = '#')
    |> List.map (fun (a,_) -> a, ind)

let file =
    File.ReadAllLines "in.txt"
    |> Array.toList
    |> List.mapi getObst
    |> List.concat
    
let arr =
    File.ReadAllLines "in.txt"
    |> array2D
   
printfn "%A" arr[pp.y,pp.x]

arr[pp.y,pp.x] <- 'u'
   
let cDir = function
    | Dir.Right -> Dir.Down
    | Dir.Down -> Dir.Left
    | Dir.Left -> Dir.Up
    | Dir.Up -> Dir.Right
    
let get x y =
    let xx = Array2D.length1 arr
    let yy = Array2D.length1 arr
    if x >= xx || x < 0 || y >= yy || y < 0
    then Tile.Out else
    match arr[y,x] with
    | '#' -> Tile.Block
    | '.' -> Tile.Empty
    | 'u' -> Tile.Used
    | x -> failwith (string x)
    
let rec step p =
    let nx, ny =
        match p.dir with
        | Dir.Down -> p.x, p.y + 1
        | Dir.Up -> p.x, p.y - 1
        | Dir.Left -> p.x - 1, p.y
        | Dir.Right -> p.x + 1, p.y
    match get nx ny with
    | Tile.Used -> Some { p with x = nx; y = ny }
    | Tile.Empty ->
        arr[ny, nx] <- 'u'
        Some { p with x = nx; y = ny }
    | Tile.Block -> step { p with dir = cDir p.dir }
    | Tile.Out -> None

let rec walk p =
    match step p with
    | Some x -> walk x
    | None ->
        arr
        |> Seq.cast<char>
        |> Seq.toList
        |> List.filter ((=)'u')
        |> List.length
        
walk pp       
|> printfn "%i"