@rem @echo off
@rem cls
@rem ".nuget\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "packages" "-ExcludeVersion"
@rem "packages\FAKE\tools\Fake.exe" build.fsx %*

dotnet restore
dotnet test ScrapySharp.Tests\ScrapySharp.Tests.csproj
dotnet build --configuration release
dotnet pack --configuration release
