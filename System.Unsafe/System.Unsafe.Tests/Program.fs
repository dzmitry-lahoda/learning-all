// Learn more about F# at http://fsharp.org

open Expecto
open System
open NUnit
open NUnit.Engine
open System.Reflection
open NUnit.Engine.Runners
open NUnit.Engine.Services
open NUnit.Engine.Agents



[<EntryPoint>]
let main argv =
    //let testEngine = new TestEngine();
    //testEngine.InternalTraceLevel <- InternalTraceLevel.Debug;
    //testEngine.Services.Add(new SettingsService(false));
    //testEngine.Services.Add(new ExtensionService());
    //testEngine.Services.Add(new ProjectService());
    //testEngine.Services.Add(new DomainManager());
    //testEngine.Services.Add(new InProcessTestRunnerFactory());
    //testEngine.Services.Add(new DriverService());
    //testEngine.Initialize()
    //let this = Assembly.GetExecutingAssembly().Location
    //let testPackage = new TestPackage(this);
    //let remoteTestRunner = new DefaultTestRunnerFactory()
    ////let t = new TestAgent(
    //remoteTestRunner.ServiceContext <- testEngine.Services
    //remoteTestRunner.StartService() |> ignore
    //let r = remoteTestRunner.MakeTestRunner(testPackage)
    ////new NullListener(), NUnit.Core.TestFilter.Empty 
    //r.Run(null, null) |> ignore
    0