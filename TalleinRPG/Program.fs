// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open DomainTypes
open Funcs
open Dice
open Damage

[<EntryPoint>]
let main argv = 
    
    let d = { D=3u; M=[ Flat 12 ] }
    printfn "%A" (eval d)

    0 // return an integer exit code
