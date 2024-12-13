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

let brute f s dx dy =
    [
        for (x,y) in [(0,1;1,0;)]
    ]

let drawPoints f s =
    let fx,fy = f
    let sx,sy = s
    if fx = sx || sy = fy then failwith "hmmm"
    let diffx = abs (fx - sx)
    let diffy = abs (fy - sy)
    0


for KeyValue(k, v) in points do
    let len = (List.length v) - 1
    for i in 0..(len-1) do
        for j in (i+1)..len do
            drawPoints v[i] v[j]