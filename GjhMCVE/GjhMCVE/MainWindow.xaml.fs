namespace ViewModels

open System
open System.Windows

open FsXaml

module Context =

    open Gjallarhorn
    open Gjallarhorn.Bindable

    let rand = Random()
    let choose n = 
        Array.sortBy (fun _ -> rand.Next()) [|0..10|]

    let create () =    
        let source = Binding.createSource()

        let count = 10
        let rand = Random()
        let model : IMutatable<int[]> = Mutable.create [| |]
                 
        let up items param = 
            //Do something with items
            printfn "%A" items
            items

        Binding.toView source "Model" model

        Binding.createCommand "CreateCommand" source
        |> Observable.subscribe 
            (fun _ -> choose count |> Mutable.set model)
        |> source.AddDisposable

        Binding.createMessageParam "Up" (up model.Value) source
        |> Observable.subscribe (Mutable.set model)

//        Binding.createMessageParam "Up" id source
//        |> Observable.subscribe (fun p -> Mutable.set model (up model.Value p))
        |> source.AddDisposable

        source
