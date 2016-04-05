module DomainTypes
    /// represents the strength of the stat
    type StatValue =
    | None = 0
    | VeryWeak = 1
    | Weak = 2
    | Average = 3
    | Good = 4
    | VeryGood = 5
    | Superb = 6
    | Legendary = 7

    type STR = STR of StatValue
    type CON = CON of StatValue
    type WILL = WILL of StatValue
    type AGI = AGI of StatValue
    type INT = INT of StatValue

    type Stats =
        { STR: STR; CON: CON; WILL: WILL; AGI: AGI; INT: INT }

    type HP = uint32
    type GP = uint32
    type AbilityPoint = System.Int32
    type ActionPoint = uint32
    type Weight = KG of uint32

    type DiceMod =
    | Flat of int
    | PerD of int
    | MaxD of uint32
    | MinD of uint32

    type DiceRoll =
        { D: uint32; M: DiceMod list }
        with
            static member op_Addition (x,y) =
                { D=x.D + y.D; M=x.M@y.M }

    type Damage = uint32
    type Stamina = uint32

    type WeaponSet =
        { Cost: AbilityPoint; Weapons: string Set }
         with
            static member op_Addition (x,y) =
                { Cost=x.Cost + y.Cost; Weapons=x.Weapons |> Set.union y.Weapons }