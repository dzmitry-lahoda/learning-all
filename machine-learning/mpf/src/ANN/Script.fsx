// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.

#r @"F:\Transcend\Work\PostedProjects\mpf-clr\lib\mathnet-numerics\MathNet.Numerics.dll"
#load @"F:\Transcend\Work\PostedProjects\mpf-clr\src\ANN\Module1.fs"
#r @"f:\Transcend\Work\PostedProjects\mpf-clr\lib\DataGrid2DLibrary.dll"

#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Windows.Forms.dll"
#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\PresentationFramework.dll"
#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\WindowsBase.dll"
#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\PresentationCore.dll"
#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Xaml.dll"

open System.Windows;
open Module1
open MathNet.Numerics.LinearAlgebra.Double

let hn = new HopfiledNetwork(4);



let window = new Window()

let show any= 

    let grid = new DataGrid2DLibrary.DataGrid2D()
    let bindingHelper = new DataGrid2DLibrary.BindingHelper();
    grid.ItemsSource <- bindingHelper.GetBindableMultiDimensionalArray<float>(any)
    window.Content <- grid    
    window.Show()


hn.LayerMatrix.ToArray() |> show