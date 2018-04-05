dotnet restore
dotnet test ScrapySharp.Tests\ScrapySharp.Tests.csproj
dotnet build --configuration release
dotnet pack --configuration release
