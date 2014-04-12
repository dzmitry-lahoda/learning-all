module Bishop


open System.Collections;
open System.Collections.Generic;

open AForge.Math.Random
open Accord;
open Accord.Statistics.Visualizations;
open Accord.Statistics;

open System
open System.Threading.Tasks;
open System.Threading;
open System.Windows.Forms;

//let w1 = [| 0.82; -1.27|]
//let w9 = [|0.35; 232.37;-5321.83; 48568.31; -231639.30; 640042.26;-1061800.52;1042400.18;-557682.99;125201.43|]
let trainNumberOfPoints = 10;
let testNumberOfPoints = 100;

let modelComplexity = 4; 


let gause = new GaussianGenerator(0.0,0.3);

let samples = 
    Array.init(100) (fun i -> gause.Next())



let histo =
    //let h = new AForge.Math.Histogram();
    let hh = new Accord.Statistics.Visualizations.Histogram(samples);
    
    (hh);
    
//let expectation = 
    
let bit = new UniformOneGenerator();

let trainValues = 
    Array.init(trainNumberOfPoints)(fun i -> float i/float trainNumberOfPoints)

let trainSignal =
    Array.map (fun x-> Math.Sin(2.0 * Math.PI * x)) trainValues

let testValuesVector = 
    Array.init(testNumberOfPoints)(fun i -> float i/float testNumberOfPoints)

let testSignal = 
    Array.map (fun x-> Math.Sin(2.0 * Math.PI * x)) testValuesVector

let trainSignalWithNoise = 
    Array.map (fun x ->  float (x + gause.Next()*bit.Next())) trainSignal 


let predictedValue x weights= 
    let polynomialValues = Array.mapi (fun i w -> w * Math.Pow(x,float i)) weights
    Array.sum polynomialValues
 
let diff weights =
    Array.mapi2 (fun i x s -> (predictedValue x weights) - s ) testValuesVector testSignal

let squaresVector weights = 
    Array.map (fun term -> Math.Pow(term, 2.0)) (diff weights);

let sumOfSquares weights = Array.sum ( squaresVector weights)

let error_function weights= 1.0/2.0 * sumOfSquares weights ;       

let e_rms weights= 
    Math.Sqrt(2.0 * (error_function weights) / float testNumberOfPoints)

let polyfit x y = 
    let (info,b,rep) = alglib.polynomialfit(x,y,modelComplexity)
    (info,b,rep)

let fitsin = 
    polyfit trainValues trainSignalWithNoise

let predicted b= 
    Array.map (fun x -> alglib.barycentriccalc(b, x)) testValuesVector

let error_function1 b = 

    let diff1 b =
        Array.map2 (fun x s -> x - s ) (predicted b) testSignal
    let squaresVector1 b = 
        Array.map (fun term -> Math.Pow(term, 2.0)) (diff1 b);
    let sumOfSquares1 b = Array.sum ( squaresVector1 b)
    1.0/2.0 * sumOfSquares1 b ;       

let e_rms1 b= 
    Math.Sqrt(2.0 * (error_function1 b) / float testNumberOfPoints)

let run f =
    let thread  = new System.Threading.Thread(new System.Threading.ThreadStart(f)) in
    thread.Start();
    thread


let main =
    let l1 = new List<HistogramBin>(histo.Bins);
    let binArray = Array.init (histo.Count) (fun i-> 0.0)
    for i=0 to histo.Count-1 do
        binArray.[i] <- l1.[i].Probability;
    


    let (info,b,rep) = fitsin
    let predictedSignal = predicted b
    //LChart.lc.scatter(trainSignalWithNoise,trainValues) |>  LChart.display |>ignore
    //LChart.lc.scatter(predictedSignal,testValuesVector) |>  LChart.display |>ignore
    LChart.lc.bar(binArray) |>  LChart.display |>ignore
    LChart.lc.scatter(samples) |>  LChart.display |>ignore
    //let ts = 
     //   fun (state:Object) -> (LChart.lc.scatter(trainSignalWithNoise,trainValues) |> LChart.display);
    //let ts = new System.Threading.ThreadStart(fun (state) -> LChart.display(LChart.lc.scatter(trainSignalWithNoise,trainValues)) |> ignore);
    //let thread  = new System.Threading.Thread(ts)
    //thread.Start();

    //LChart.lc.scatter(trainSignalWithNoise,trainValues) |> LChart.display
    //run ts |> ignore
    //run (fun state -> ( LChart.lc.scatter(trainSignalWithNoise,trainValues) |> LChart.display)) |> ignore
    //Array.iter (fun x -> printfn "%f" x) signalV 
    //Array.iter (fun x -> printfn "%f" x) trainSignalWithNoise
    //printfn  "e_RMS %f" (e_rms b.innerobj.w)
    printfn  "e_RMS %f" (e_rms1 b)
    //printfn  "e_RMS %f" (e_rms w9)

    //let predict =alglib.barycentriccalc(b, 2.2);
    //Array.iter (fun  x -> printfn "%f" ((predictedValue x b.innerobj.w) - b.innerobj.w.[0])) testValuesVector
    //printfn  "%f" error_function
    //Array.iter (fun x -> printfn "W %f" x) b.innerobj.w
    //printfn "RMSerror %f" rep.rmserror
    Console.ReadKey();
    ()
    

main

