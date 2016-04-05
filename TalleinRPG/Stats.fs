module Stats
    open DomainTypes
    open Funcs

    let statCost (fromValue:StatValue) (toValue:StatValue) =
        let to' = int toValue
        let from' = int fromValue
        pow2 (to' - from')

    let damageMod =
        function
        | STR StatValue.None -> Some (MaxD 1u)
        | STR StatValue.VeryWeak -> Some (PerD -2)
        | STR StatValue.Weak -> Some (PerD -1)
        | STR StatValue.Average -> None
        | STR StatValue.Good -> Some (MinD 2u)
        | STR StatValue.VeryGood -> Some (MinD 3u)
        | STR StatValue.Superb -> Some (MinD 4u)
        | STR StatValue.Legendary -> Some (MinD 5u)
        | _ -> failwith "Did not understand value"

    let baseEncumberance = 
        function
        | STR StatValue.None -> KG 0u
        | STR StatValue.VeryWeak -> KG 10u
        | STR StatValue.Weak -> KG 25u
        | STR StatValue.Average -> KG 40u
        | STR StatValue.Good -> KG 70u
        | STR StatValue.VeryGood -> KG 100u
        | STR StatValue.Superb -> KG 200u
        | STR StatValue.Legendary -> KG 500u
        | _ -> failwith "Did not understand value"

    let damageReduction = 
        function
        | STR StatValue.None | STR StatValue.VeryWeak | STR StatValue.Weak | STR StatValue.Average -> 0
        | STR StatValue.Good -> 2
        | STR StatValue.VeryGood -> 3
        | STR StatValue.Superb -> 5
        | STR StatValue.Legendary -> 8
        | _ -> failwith "Did not understand value"

    let strWeaponBonus =
        function
        | STR StatValue.None | STR StatValue.VeryWeak | STR StatValue.Weak | STR StatValue.Average | STR StatValue.Good -> None
        | STR StatValue.VeryGood -> Some { D=1u; M=[] }
        | STR StatValue.Superb -> Some { D=2u; M=[] }
        | STR StatValue.Legendary -> Some { D=3u; M=[] }
        | _ -> failwith "Did not understand value"

    let hpMod =
        function
        | CON StatValue.None -> Some (MaxD 1u)
        | CON StatValue.VeryWeak -> Some (MaxD 3u)
        | CON StatValue.Weak -> Some (MaxD 5u)
        | CON StatValue.Average -> None
        | CON StatValue.Good -> Some (PerD 1)
        | CON StatValue.VeryGood -> Some (PerD 2)
        | CON StatValue.Superb -> Some (PerD 3)
        | CON StatValue.Legendary -> Some (PerD 4)
        | _ -> failwith "Did not understand value"

    let calcHpAtLevel1 con =
        let (CON unwrapped) = con
        let int = int unwrapped
        {   D=uint32 int; 
            M=
                match hpMod con with
                | None -> []
                | Some m -> [m] 
        }

    let toughnessMod =
        function
        | CON StatValue.None -> -6
        | CON StatValue.VeryWeak -> -4
        | CON StatValue.Weak -> -2
        | CON StatValue.Average -> 0
        | CON StatValue.Good -> 2
        | CON StatValue.VeryGood -> 4
        | CON StatValue.Superb -> 6
        | CON StatValue.Legendary -> 8
        | _ -> failwith "Did not understand value"

    let staminaMod =
        function
        | CON StatValue.None -> Some (MaxD 1u)
        | CON StatValue.VeryWeak | CON StatValue.Weak -> None
        | CON StatValue.Average -> Some (PerD 1)
        | CON StatValue.Good -> Some (PerD 2)
        | CON StatValue.VeryGood -> Some (PerD 3)
        | CON StatValue.Superb -> Some (PerD 4)
        | CON StatValue.Legendary -> Some (PerD 5)
        | _ -> failwith "Did not understand value"

    let staminaRegen =
        function
        | CON StatValue.None -> 0
        | CON StatValue.VeryWeak -> 1
        | CON StatValue.Weak -> 2
        | CON StatValue.Average -> 3
        | CON StatValue.Good -> 4
        | CON StatValue.VeryGood -> 5
        | CON StatValue.Superb -> 7
        | CON StatValue.Legendary -> 10
        | _ -> failwith "Did not understand value"

    let magicResMod =
        function
        | WILL StatValue.None -> -6
        | WILL StatValue.VeryWeak -> -3
        | WILL StatValue.Weak -> -1
        | WILL StatValue.Average -> 0
        | WILL StatValue.Good -> 1
        | WILL StatValue.VeryGood -> 2
        | WILL StatValue.Superb -> 3
        | WILL StatValue.Legendary -> 4
        | _ -> failwith "Did not understand value"

    let chanceOfSpellFailure (int':INT) will =
        let int'' = match int' with INT i -> int i
        let will'' = match will with WILL i -> int i

        match will'' - int'' with
        | x when x < 0 -> 3 * x
        | _ -> 0

    let maxExperienceGainPerEncounter =
        function
        | WILL statVal -> int statVal
        | _ -> failwith "Did not understand value"

    let thrownWeaponMod =
        function
        | AGI StatValue.None -> -7
        | AGI StatValue.VeryWeak -> -3
        | AGI StatValue.Weak -> -1
        | AGI StatValue.Average -> 0
        | AGI StatValue.Good -> 1
        | AGI StatValue.VeryGood -> 2
        | AGI StatValue.Superb -> 4
        | AGI StatValue.Legendary -> 7
        | _ -> failwith "Did not understand value"

    let baseActionPoints =
        function
        | AGI StatValue.None -> 1
        | AGI StatValue.VeryWeak -> 3
        | AGI StatValue.Weak | AGI StatValue.Average | AGI StatValue.Good | AGI StatValue.VeryGood -> 5
        | AGI StatValue.Superb -> 6
        | AGI StatValue.Legendary -> 7
        | _ -> failwith "Did not understand value"

        