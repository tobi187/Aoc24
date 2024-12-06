module day3

open System
open System.IO
open System.Text.RegularExpressions

let file = File.ReadAllText "in.txt"

let day1 =
    Regex.Matches (file, @"mul\(\d+,\d+\)")
    |> Seq.toList
    |> List.map (_.Value.Split(','))
    |> List.map (Array.map(fun e -> e.Replace("mul(","").Replace(")","")|>int))
    |> List.map (Array.reduce (*))
    |> List.sum
    |> printfn "%i"

type Res = Do | Dont | Match of string | End

