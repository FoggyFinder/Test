module main

open System
open FsXaml
open System.Windows

type MainWindow = XAML<"MainWindow.xaml">

[<STAThread>]
[<EntryPoint>]
let main argv =
    let app = Application()

    Gjallarhorn.Wpf.Platform.install true
    |> ignore

    let vm = ViewModels.Context.create()
    let window = MainWindow(DataContext = vm)

    app.Run window