open System
open Mono.Unix
open Mono.Unix.Native
open ServiceStack
open ServiceStack.Common

type Hello = { mutable Name: string; }
type HelloResponse = { mutable Result: string; }
type HelloService() =
    inherit Service() with
        member this.Any (req:Hello) = { Result = "Hello, " + req.Name } :> Object

//Define the Web Services AppHost
type AppHost =
    inherit AppHostHttpListenerBase
    new() = { inherit AppHostHttpListenerBase("Hello F# Services", typeof<HelloService>.Assembly) }
    override this.Configure container =
        base.Routes
            .Add<Hello>("/hello")
            .Add<Hello>("/hello/{Name}") |> ignore

//Run it!
[<EntryPoint>]
let main args =
    let host = if args.Length = 0 then "http://*:8080/" else args.[0]
    printfn "listening on %s ..." host
    let appHost = new AppHost()
    appHost.Init() |> ignore
    appHost.Start host |> ignore

    let mutable continuing = true 
    let signals = [| new UnixSignal(Signum.SIGINT); new UnixSignal(Signum.SIGTERM) |]

    while continuing do
        let id = UnixSignal.WaitAny(signals)
        if id > 0 then
            continuing <- false    
    0