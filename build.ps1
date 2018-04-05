# functions coming from https://blogs.msdn.microsoft.com/jaredpar/2009/01/16/powershell-linq-take-count-and-take-while/
#============================================================================
# Take count elements fro the pipeline 
#============================================================================
function Take-Count() {
    param ( [int]$count = $(throw "Need a count") )
    begin { 
        $total = 0;
    }
    process { 
        if ( $total -lt $count ) {
            $_
        }
        $total += 1
    }
}

#============================================================================
# Take elements from the pipeline while the predicate is true
#============================================================================
function Take-While() {
    param ( [scriptblock]$pred = $(throw "Need a predicate") )
    begin {
        $take = $true
    }
    process {
        if ( $take ) {
            $take = & $pred $_
            if ( $take ) {
                $_
            }
        }
    }
}

$lines = Get-Content .\ReleaseNotes.md | Where-Object { $_.Length -gt 0 }
$top = $lines | Select-Object -First 1
$version = $top.Replace("#", "").Trim()
$releaseNotes = ""

$notes = $lines | Select-Object -Skip 1 | Take-While { -not $_.Trim().StartsWith("#") }

foreach ($note in $notes) {
    $releaseNotes += $note + "`n"
}

dotnet restore
dotnet test ScrapySharp.Tests\ScrapySharp.Tests.csproj
dotnet build --configuration release
dotnet pack --configuration release /p:PackageVersion=$version /p:PackageReleaseNotes=$releaseNotes
if (Test-Path .\release)
{
    Remove-Item -Recurse -Force -Path release
}
mkdir release
xcopy .\ScrapySharp\bin\Release\*.nupkg release
Remove-Item .\ScrapySharp\bin\**\*.nupkg
Remove-Item .\ScrapySharp.Core\bin\**\*.nupkg
