dotnet restore
dotnet test ScrapySharp.Tests\ScrapySharp.Tests.csproj
dotnet build --configuration release
dotnet pack --configuration release
Remove-Item -Recurse -Force release
mkdir release
xcopy .\ScrapySharp\bin\Release\*.nupkg release
Remove-Item .\ScrapySharp\bin\**\*.nupkg
Remove-Item .\ScrapySharp.Core\bin\**\*.nupkg
