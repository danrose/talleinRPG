module WeaponSets
    open DomainTypes 

    let peasantWeaponry =
        { Cost=1; Weapons=[ "staff"; "club"; "pitchfork"; "knife"; "sling" ] |> Set.ofList }

    let archery =
        { Cost=5; Weapons=[ "short bow"; "long bow" ] |> Set.ofList }

    let militaryWeaponry =
        { Cost=5; Weapons=[ "short sword"; "dagger"; "crossbow"; "spear"; "polearm"; "warhammer"; 
        "javelin" ] |> Set.ofList }

    let knightlyWeaponry =
        { Cost=5; Weapons=[ "long sword"; "battleaxe"; "mace"; "morning star"; "polearm"; "lance"; 
        "dagger" ] |> Set.ofList }

    let huntingWeaponry =
        { Cost=4; Weapons=[ "dagger"; "short bow"; "hatchet"; "hand axer"; ] 
        |> Set.ofList
        |> Set.union peasantWeaponry.Weapons }

    let barbaricWeaponry =
        { Cost=5; Weapons=[ "two-handed sword"; "long sword"; "battleaxe"; "warhammer"; "hand axe";
         "club"; "spear" ] |> Set.ofList }

    let thrownWeapons =
        { Cost=2; Weapons=[ "throwing dagger"; "dart" ] |> Set.ofList }


