// --------------------------------------------------------------------------------------
// FAKE build script 
// --------------------------------------------------------------------------------------

#r @"packages/FAKE/tools/FakeLib.dll"
open Fake 
open Fake.Git
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper
open Fake.ProcessHelper
open System

// --------------------------------------------------------------------------------------
// START TODO: Provide project-specific details below
// --------------------------------------------------------------------------------------

// Information about the project are used
//  - for version and project name in generated AssemblyInfo file
//  - by the generated NuGet package 
//  - to run tests and to publish documentation on GitHub gh-pages
//  - for documentation, you also need to edit info in "docs/tools/generate.fsx"

// The name of the project 
// (used by attributes in AssemblyInfo, name of a NuGet package and directory in 'src')
let project = "NDceRpc"

// Short summary of the project
// (used as description in AssemblyInfo and as a short summary for NuGet package)
let summary = "RPC based on .NET, MS-RPC, binary serialization and WCF"

// Longer description of the project
// (used as a description for NuGet package; line breaks are automatically cleaned up)
let description = """
RPC based on .NET, MS-RPC, binary serialization and WCF
"""
// List of author names (for NuGet package)
let authors = [ "Dzmitry Lahoda" ]
// Tags for your project (for NuGet package)
let tags = "dce rpc interop wcf"

// File system information 
let solutionFile  = "NDceRpc"
// Pattern specifying assemblies to be tested using NUnit
let testAssemblies = "tests/**/bin/Release/*Tests*.dll"

// Git configuration (used for publishing documentation in gh-pages branch)
// The profile where the project is posted 
let gitHome = "https://github.com/OpenSharp"
// The name of the project on GitHub
let gitName = "NDceRpc"

// --------------------------------------------------------------------------------------
// END TODO: The rest of the file includes standard build steps 
// --------------------------------------------------------------------------------------

// Read additional information from the release notes document
Environment.CurrentDirectory <- __SOURCE_DIRECTORY__
let release = parseReleaseNotes (IO.File.ReadAllLines "RELEASE_NOTES.md")

// Generate assembly info files with the right version & up-to-date information
Target "AssemblyInfo" (fun _ ->
  let msProj = project+".Microsoft"
  let ms = "src/" + msProj + "/Properties/AssemblyInfo.cs"
  CreateCSharpAssemblyInfo ms
      [ Attribute.Title msProj
        Attribute.Product msProj
        Attribute.Description "Library for interop of managed code with DCE RPC runtime"
        Attribute.Version release.AssemblyVersion
        Attribute.FileVersion release.AssemblyVersion ] 

  let smProj = project+".ServiceModel"
  let sm = "src/" + smProj + "/Properties/AssemblyInfo.cs"
  CreateCSharpAssemblyInfo sm
      [ Attribute.Title smProj
        Attribute.Product smProj
        Attribute.Description "Contains ServiceModel like attributes, errors and interfaces"
        Attribute.Version release.AssemblyVersion
        Attribute.FileVersion release.AssemblyVersion ]

  let coreProj = project+".ServiceModel.Core"
  let core = "src/" + coreProj + "/Properties/AssemblyInfo.cs"
  CreateCSharpAssemblyInfo core
      [ Attribute.Title coreProj
        Attribute.Product coreProj
        Attribute.Description summary
        Attribute.Version release.AssemblyVersion
        Attribute.FileVersion release.AssemblyVersion ] 
)

// --------------------------------------------------------------------------------------
// Clean build results & restore NuGet packages

Target "RestorePackages" RestorePackages

Target "Clean" (fun _ ->
    CleanDirs ["bin"; "temp"]
)

// --------------------------------------------------------------------------------------
// Build library & test project

Target "Build" (fun _ ->
    !! (solutionFile + "*.sln")
    |> MSBuildRelease "" "Rebuild"
    |> ignore
)

// --------------------------------------------------------------------------------------
// Run the unit tests using test runner

Target "RunTests" (fun _ ->
    !! testAssemblies 
    |> NUnit (fun p ->
        { p with
            DisableShadowCopy = true
            TimeOut = TimeSpan.FromMinutes 20.
            OutputFile = "TestResults.xml" })
)

// --------------------------------------------------------------------------------------
// Build a NuGet package

Target "NuGet" (fun _ ->
    NuGet (fun p -> 
        { p with   
            Authors = authors
            Project = project
            Summary = summary
            Description = description
            Version = release.NugetVersion
            ReleaseNotes = String.Join(Environment.NewLine, release.Notes)
            Tags = tags
            OutputPath = "bin"
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey"
            Dependencies = [] })
        ("nuget/" + project + ".nuspec")
)

//--------------------------------------
// Merge output into single file

Target "Merge" (fun _ ->
  Shell.Exec "merge.bat"
  |> ignore
)

// --------------------------------------------------------------------------------------
// Release Scripts

Target "Release" DoNothing

// --------------------------------------------------------------------------------------
// Run all targets by default. Invoke 'build <Target>' to override

Target "All" DoNothing

"Clean"
  ==> "RestorePackages"
  ==> "AssemblyInfo"
  ==> "Build"
  //==> "RunTests" //FIX TESTS
  ==> "Merge"
  ==> "Release"
  ==> "NuGet"
  ==> "All"

RunTargetOrDefault "All"