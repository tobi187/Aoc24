module day8

open System.IO

let file = File.ReadAllLines "in.txt" |> array2D

let lenY = Array2D.length1 file
let lenX = Array2D.length2 file

let points = 
    [
    for y in 0..(lenY-1) do
        for x in 0..(lenX-1) do
            if file[y,x] <> '.' then
                yield (file[y,x], (x,y))
    ]
    |> List.groupBy fst
    |> List.map (fun (f,s) -> f, List.map (snd) s)
    |> Map

let drawPoint x y =
    if x >= 0 && y >= 0 && x < lenX && y < lenY && file[y,x] = '.' then file[y, x] <- '#'

let dp x y =
    if x >= 0 && y >= 0 && x < lenX && y < lenY
    then Some (x,y) else None


let drawPoints f s =
    let fx,fy = f
    let sx,sy = s
    if fx = sx || sy = fy then failwith "hmmm"
    let diffx = (fx - sx)
    let diffy = (fy - sy)
    [
        dp (sx + (diffx * -1)) (sy + (diffy * -1)) 
        dp (fx+diffx) (fy+diffy)
    ]
    //match sign diffx, sign diffy with
    //| 1,1   -> 
    //    drawPoint (fx+(diffx*-1) (fy+(diffy*-1)) 
    //    drawPoint (sx+diffx) (sy+diffy) 
    //| 1,-1  -> 
    //    drawPoint (fx-diffx) (fy+diffy) 
    //    drawPoint (sx+diffx) (sy-diffy) 
    //| -1,1  -> 
    //    drawPoint (fx+diffx) (fy-diffy) 
    //    drawPoint (sx-diffx) (sy+diffy)
    //| -1,-1 -> 
    //    drawPoint (fx+diffx) (fy+diffy) 
    //    drawPoint (sx-diffx) (sy-diffy)
    //| a, b -> failwith (sprintf "Unexpected %i %i " a b)
    //|> ignore


[
    for KeyValue(k, v) in points do
        let len = (List.length v) - 1
        for i in 0..(len-1) do
            for j in (i+1)..len do
                yield drawPoints v[i] v[j]
]
|> List.concat
|> List.choose id
|> List.distinct
|> List.length
|> printfn "%i"


//file
//|> Seq.cast<char>
//|> Seq.filter((=)'#')
//|> Seq.length
//|> printfn "%i"

//for y in 0 .. file.GetLength(1) - 1 do
//    printfn "%A" file.[y,*]