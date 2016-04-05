module Funcs
    open DomainTypes

    let pow2 x =
        let rec loop x' sum m =
            match x' with
            | 0 -> sum * m
            | v -> loop (v-1) (sum*2) m
        
        match x with
        | v when v < 0 ->  loop (x*(-1)) 1 -1
        | 0 -> 0
        | _ -> loop x 1 1