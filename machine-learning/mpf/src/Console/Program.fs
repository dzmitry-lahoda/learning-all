open System.Collections
open System.Drawing
open System.IO
open System.Windows.Forms
open System.Windows.Forms.DataVisualization.Charting
open System.Windows.Forms.DataVisualization.Charting.Utilities
open System
open AForge.Math.Random
open System
open System.Threading.Tasks;
open System.Threading;
open System.Windows.Forms;
open Bishop


let w1 = [| 0.82; -1.27|]
let w9 = [|0.35; 232.37;-5321.83; 48568.31; -231639.30; 640042.26;-1061800.52;1042400.18;-557682.99;125201.43|]
let trainNumberOfPoints = 100;
let testNumberOfPoints = 100;

let modelComplexity = 2; 


let gause = new GaussianGenerator(0.0,0.1);
let bit = new UniformOneGenerator();

let trainValues = 
    Array.init(trainNumberOfPoints)(fun i -> float i/float trainNumberOfPoints)

let trainSignal =
    Array.map (fun x-> Math.Sin(2.0 * Math.PI * x)) trainValues

type lc() =
    inherit Chart()

    static let axisFont = new Font("Times New Roman", 16.f, FontStyle.Bold)
    static let axisColor = Color.Blue
    static let titleFont = new Font("Times New Roman", 20.f, FontStyle.Bold)
    static let titleColor = Color.DarkBlue
    static let legendFont = new Font("Times New Roman", 12.f, FontStyle.Bold)
    
    static let copyChart (c1:lc) =
        let copyTitle (t1:Title) =
            let t = new Title()
            t.Text <- t1.Text
            t.Font <- t1.Font
            t.ForeColor <- t1.ForeColor
            t.BorderColor <- t1.BorderColor
            t.BackColor <- t1.BackColor
            t.Alignment <- t1.Alignment
            t
        let c = new lc()
        c1.Titles |> Seq.iter (fun t -> c.Titles.Add(copyTitle t))
        c
        
    static let copyArea (a1:ChartArea) =
        let a = new ChartArea()
        a.AxisX.MajorGrid.Enabled <- a1.AxisX.MajorGrid.Enabled
        a.AxisY.MajorGrid.Enabled <- a1.AxisY.MajorGrid.Enabled
        a.AxisX.Title <- a1.AxisX.Title
        a.AxisX.TitleFont <- a1.AxisX.TitleFont 
        a.AxisX.TitleForeColor <- a1.AxisX.TitleForeColor       
        a.AxisY.Title <- a1.AxisY.Title
        a.AxisY.TitleFont <- a1.AxisY.TitleFont
        a.AxisY.TitleForeColor <- a1.AxisY.TitleForeColor
        a        

    static let rec copySeries (s1:Series) (c:lc) a: Series =
        let findLastNotEmpty (seriesCol:SeriesCollection) =
            Array.FindLast(seriesCol |> Seq.toArray, fun s -> not(s.Points.Count = 0) && not(s.ChartType = SeriesChartType.BoxPlot))    
        if s1.Points.Count = 0 && not(s1.ChartType = SeriesChartType.BoxPlot) then
            let s = copySeries (c.Series |> findLastNotEmpty) c a
            s.ChartType <- s1.ChartType
            s.ChartArea <- a
            s
        else
            let copyPoint (p1:DataPoint) =
                let p = new DataPoint(p1.XValue, p1.YValues |> Array.copy)
                p
            let s = new Series()
            s1.Points |> Seq.iter (fun p -> s.Points.Add(copyPoint p))
            s.ChartType <- s1.ChartType
            if s.ChartType = SeriesChartType.BoxPlot then
                let lastSeries = c.Series |> findLastNotEmpty
                s.["BoxPlotSeries"] <- (lastSeries).Name
                lastSeries.Enabled <- false
            s.IsValueShownAsLabel <- s1.IsValueShownAsLabel
            s.MarkerSize <- s1.MarkerSize
            s.MarkerStyle <- s1.MarkerStyle
            s.Color <- s1.Color
            s.Enabled <- s1.Enabled
            s.["DrawingStyle"] <- s1.["DrawingStyle"]
            s.ChartArea <- a
            s
       
    static let addAreaAndSeries (c:lc) oldArea (seriesCol:SeriesCollection) =
        let newArea = copyArea oldArea
        c.ChartAreas.Add(newArea)
        let relatedSeries = seriesCol |> Seq.filter (fun s -> s.ChartArea = oldArea.Name)
        relatedSeries |> Seq.iter (fun s -> c.Series.Add(copySeries s c newArea.Name))         
        
    static let Create (chartType, x, y, isValueShownAsLabel, markerSize, markerStyle, color, xname, yname, seriesName, title) =
        let c = new lc()
        let a = new ChartArea()
        let s = new Series()
        s.ChartType <- chartType
        c.ChartAreas.Add(a)
        c.Series.Add(s)
        match x, y with
            | Some(x), None     -> failwith "You cannot pass only x to a chart drawing function"
            | Some(x), Some(y)  -> s.Points.DataBindXY(x, [|y|])
            | None, Some(y)     -> s.Points.DataBindY([|y|])
            | None, None        -> ()
        s.IsValueShownAsLabel <- defaultArg isValueShownAsLabel s.IsValueShownAsLabel
        s.MarkerSize <- defaultArg markerSize s.MarkerSize
        s.MarkerStyle <- defaultArg markerStyle s.MarkerStyle
        s.Color <- defaultArg color s.Color
        
        a.AxisX.MajorGrid.Enabled <- false
        a.AxisY.MajorGrid.Enabled <- false
        
        match xname with
        | Some(xname) ->
            a.AxisX.Title <- xname
            a.AxisX.TitleFont <- axisFont
            a.AxisX.TitleForeColor <- axisColor
        | _ -> ()
        match yname with
        | Some(yname) ->
            a.AxisY.Title <- yname
            a.AxisY.TitleFont <- axisFont
            a.AxisY.TitleForeColor <- axisColor
        | _ -> ()
        match seriesName with
        | Some(seriesName) -> s.Name <- seriesName
        | _ -> ()
        match title with
        | Some(title) ->
            let t = c.Titles.Add(title: string)
            t.Font <- titleFont
            t.ForeColor <- titleColor
        | _ -> ()
        c
          
    static member (+) (c1:lc, c2:lc) =    
        let c = copyChart(c1)   
        c1.ChartAreas |> Seq.iter (fun a -> addAreaAndSeries c a c1.Series)
        let lastArea = c.ChartAreas |> Seq.nth ((c.ChartAreas |> Seq.length) - 1)
        c2.Series |> Seq.iter(fun s -> c.Series.Add(copySeries s c lastArea.Name))
        let l = c.Legends.Add("")
        l.Font <- legendFont
        c
       
    static member (++) (c1:lc, c2:lc) =
        let c = copyChart(c1)   
        c1.ChartAreas |> Seq.iter (fun a -> addAreaAndSeries c a c1.Series)
        let lastArea = c.ChartAreas |> Seq.nth ((c.ChartAreas |> Seq.length) - 1)
        addAreaAndSeries c c2.ChartAreas.[0] c2.Series
        let firstArea = c.ChartAreas |> Seq.nth ((c.ChartAreas |> Seq.length) - 1)
        c2.ChartAreas |> Seq.skip 1 |> Seq.iter (fun a -> addAreaAndSeries c a c2.Series)
        c    
        
    // Here are the interesting public static construction methods
    // There is one method for every type of chart that we support
    
    static member scatter (?y,?x, ?isValueShownAsLabel, ?markerSize, ?markerStyle, ?color, ?xname, ?yname, ?seriesName, ?title) = 
        Create (SeriesChartType.Point, x, y, isValueShownAsLabel, markerSize, markerStyle, color, xname, yname, seriesName, title)
    static member line (?y,?x, ?isValueShownAsLabel, ?markerSize, ?markerStyle, ?color, ?xname, ?yname, ?seriesName, ?title) = 
        Create (SeriesChartType.Line, x, y, isValueShownAsLabel, markerSize, markerStyle, color, xname, yname, seriesName, title)
    static member spline (?y,?x, ?isValueShownAsLabel, ?markerSize, ?markerStyle, ?color, ?xname, ?yname, ?seriesName, ?title) = 
        Create (SeriesChartType.Spline, x, y, isValueShownAsLabel, markerSize, markerStyle, color, xname, yname, seriesName, title)
    static member stepline (?y,?x, ?isValueShownAsLabel, ?markerSize, ?markerStyle, ?color, ?xname, ?yname, ?seriesName, ?title) = 
        Create (SeriesChartType.StepLine, x, y, isValueShownAsLabel, markerSize, markerStyle, color, xname, yname, seriesName, title)
    static member bar (?y,?x, ?isValueShownAsLabel, ?markerSize, ?markerStyle, ?color, ?xname, ?yname, ?seriesName, ?title, ?drawingStyle) = 
        let c = Create (SeriesChartType.Bar, x, y, isValueShownAsLabel, markerSize, markerStyle, color, xname, yname, seriesName, title)
        c.Series.[0].["DrawingStyle"] <- defaultArg drawingStyle (c.Series.[0].["DrawingStyle"])
        c
        
    // This is the F# wrapper for the .NET Bubble Chart.
        
    static member bubble (?y,?x, ?isValueShownAsLabel, ?markerSize, ?markerStyle, ?color, ?xname, ?yname, ?seriesName, ?title, ?drawingStyle) = 
        let c = Create (SeriesChartType.Bubble, x, y, isValueShownAsLabel, markerSize, markerStyle, color, xname, yname, seriesName, title)
        c.Series.[0].["DrawingStyle"] <- defaultArg drawingStyle (c.Series.[0].["DrawingStyle"])
        c
    static member column (?y,?x, ?isValueShownAsLabel, ?markerSize, ?markerStyle, ?color, ?xname, ?yname, ?seriesName, ?title, ?drawingStyle) = 
        let c = Create (SeriesChartType.Column, x, y, isValueShownAsLabel, markerSize, markerStyle, color, xname, yname, seriesName, title)
        c.Series.[0].["DrawingStyle"] <- defaultArg drawingStyle (c.Series.[0].["DrawingStyle"])
        c
    static member boxplot (?y, ?color, ?xname, ?yname, ?seriesName, ?title, ?whiskerPercentile, ?percentile, ?showAverage, ?showMedian, ?showUnusualValues) = 
        let c = Create (SeriesChartType.Spline, None, y, None, None, None, color, xname, yname, seriesName, title)
        c.Series.[0].Enabled <- false
        let s = new Series()
        c.Series.Add(s)
        s.ChartType <- SeriesChartType.BoxPlot
        s.["BoxPlotSeries"] <- c.Series.[0].Name
        whiskerPercentile |> Option.iter (fun x -> s.["BoxPlotWhiskerPercentile"] <- string x) 
        percentile |> Option.iter (fun x -> s.["BoxPlotPercentile"] <- string x) 
        showAverage |> Option.iter (fun x -> s.["BoxPlotShowAverage"] <- string x) 
        showMedian |> Option.iter (fun x -> s.["BoxPlotShowMedian"] <- string x)
        s.["BoxPlotShowUnusualValues"] <- "true" 
        showUnusualValues |> Option.iter (fun x -> s.["BoxPlotShowUnusualValues"] <- string x)
        c
    static member legend(?title) =
        let l = new Legend()
        title |> Option.iter (fun t -> l.Title <- string title)
        l 

let display (c:lc) =
    let copy () =
        let stream = new MemoryStream()
        c.SaveImage(stream, Imaging.ImageFormat.Bmp)
        let bmp = new Bitmap(stream)
        Clipboard.SetDataObject(bmp)
    
    c.KeyDown.Add(fun e -> if e.Control = true && e.KeyCode = Keys.C then copy ())
    let pressToCopy = "(press CTRL+C to copy)"     
    let name = if c.Titles.Count = 0 then sprintf "%s %s " "lc" pressToCopy else sprintf "%s %s " c.Titles.[0].Text  pressToCopy
    let f = new Form(Text = name, Size = new Size(800,600), TopMost = true)
    c.Dock <- DockStyle.Fill
    f.Controls.Add(c)
    f.Show()
    c

// ---------------------------------------------------------------------------
// Sample tests for the API above.
// ---------------------------------------------------------------------------


let (info,b,rep) = fitsin
let pred = predicted b

let x = testValuesVector;// [1.;2.5;3.1;4.;4.8;6.0;7.5;8.;9.1;15.]
let y = pred;// [1.6;2.1;1.4;4.;2.3;1.9;2.4;1.4;5.;2.9]

lc.scatter(y,x) |> display