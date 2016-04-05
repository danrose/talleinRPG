module Dice
    open DomainTypes
    
    let private dice = System.Random()

    let roll() = uint32 (dice.Next(7))

    let checkZero (roll:uint32) m =
        let x = int roll
        let modded = (int roll) + m
        if modded < 0 then (uint32 0) else (uint32 modded)

    let (+/-) = checkZero

    let evalMod (rolls:uint32 list) (dm:DiceMod list) =
        dm
        |> List.fold (
            fun (d,f) item ->
                match item with
                | Flat x -> (d,f+x)
                | PerD x -> (d |> List.map (fun x' -> x' +/- x), f)
                | MinD x -> (d |> List.map (fun x' -> if x' > x then x' else x), f)
                | MaxD x -> (d |> List.map (fun x' -> if x' < x then x' else x), f)
        ) (rolls,0)

    let eval (dm:DiceRoll) =
        let rolls = List.init (int dm.D) (fun x -> roll())
        let modifiedRolls = evalMod rolls (dm.M)
        let ret = int ((fst modifiedRolls) |> List.sum) + (snd modifiedRolls)
        0u +/- ret
       
        
