namespace Bowling.FSharp

module Details =
  type Frame = {
    Pins : int
    Roll : int
  }
  let score xs = 
    match xs with
    | x1 :: (x2 :: x3 :: _ as xs) when x1 = 10 -> Some({ Pins = 10 + x2 + x3; Roll = 1}, xs)
    | x1 :: x2 :: (x3 :: _ as xs) when x1 + x2 = 10 -> Some({ Pins = 10 + x3; Roll = 2} , xs)
    | x1 :: x2 :: xs -> Some({ Pins = x1 + x2; Roll = 2}, xs)
    | x1 :: [] -> Some ({ Pins = x1; Roll = 1}, [])
    | _ -> None

  let finalScore xs =
    match xs with
    | x1 :: x2 :: x3 :: [] when 9 < x1 + x2 -> x1 + x2 + x3
    | x1 :: x2 :: [] -> x1 + x2
    | x1 :: [] -> x1
    | _ -> 0

  let rec total (rolls:int list) = 
    let scores = Seq.unfold score rolls
    
    if (scores |> Seq.length) > 9 then
        let roll = scores |> Seq.take(9) |> Seq.sumBy (fun f -> f.Roll)
        let final = rolls |> Seq.skip(roll) |> Seq.toList |> finalScore
        (scores |> Seq.take(9) |> Seq.sumBy (fun f -> f.Pins)) + final
    else
        scores |> Seq.sumBy (fun f -> f.Pins)

type Game () = 
  let mutable rolls = ResizeArray []
  member x.Roll pins = rolls.Add(pins)
  member x.Score () = Details.total (rolls |> List.ofSeq)