:: downloads dependencies used

.nuget\nuget.exe install protobuf-net -ExcludeVersion -OutputDirectory packages

:: for .NET 3.5
.nuget\nuget.exe install CodeContracts.Unofficial -ExcludeVersion -OutputDirectory packages
.nuget\nuget.exe install TaskParallelLibrary -ExcludeVersion -OutputDirectory packages

:: for tests

.nuget\nuget.exe install Unity -Version 2.1.505.2 -ExcludeVersion -OutputDirectory packages
.nuget\nuget.exe install Unity.Interception -Version 2.1.505.2 -ExcludeVersion -OutputDirectory packages
.nuget\nuget.exe install NSubstitute -ExcludeVersion -OutputDirectory packages
.nuget\nuget.exe install NUnit -ExcludeVersion -OutputDirectory packages
.nuget\nuget.exe install NUnit.Runners  -ExcludeVersion -OutputDirectory tools
.nuget\nuget.exe install NStopwatch -ExcludeVersion -OutputDirectory packages

:: for deployment
.nuget\nuget.exe install ILRepack -OutputDirectory packages -ExcludeVersion -ExcludeVersion -OutputDirectory packages








