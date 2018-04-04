#I "packages/FAKE/tools/"
#r "FakeLib.dll"

open Fake
open System
open System.IO

let buildDir = __SOURCE_DIRECTORY__ @@ "build"
let nugetsDir = __SOURCE_DIRECTORY__ @@ "NuGet"

ensureDirectory buildDir
ensureDirectory nugetsDir

Target "Clean"
    <| fun _ ->
        CleanDir buildDir
        CleanDir nugetsDir

Target "Packages"
    <| fun _ ->
        trace "Restoring packages"
        RestorePackages()

Target "BuildRelease"
    <| fun _ ->
        !! "*/**.csproj"
        |> MSBuildRelease buildDir "Build"
        |> Log "BuildTests-Output: "

let buildNuGet mustPublish versionSuffix =
    buildDir
    |> directoryInfo
    |> filesInDir
    |> Array.map(fun f -> f.FullName)
    |> CopyFiles nugetsDir

    let nugetAccessKey = 
        let n = @"C:\keys\nuget-romcyber.txt"
        if File.Exists n
        then File.ReadAllText n
        else ""

    let v = NuGetVersion.getLastNuGetVersion "https://www.nuget.org/api/v2" "ScrapySharp"

    let nugetsVersions name = 
        NuGetVersion.nextVersion <|
            fun arg -> 
                { arg 
                    with 
                        PackageName=name
                        DefaultVersion="0.1.0"
                        Increment=NuGetVersion.IncPatch
                }
    let version = 
        match versionSuffix, (nugetsVersions "ScrapySharp") with
        | Some suffix, v -> v + suffix
        | None, v -> v

    NuGet (fun p -> 
            { p with
                Authors = ["Romain Flechner"]
                Project = "ScrapySharp"
                OutputPath = nugetsDir
                AccessKey = "romcyber"
                Version = version
                Publish = mustPublish
                Dependencies = getDependencies "ScrapySharp/packages.config"
                Properties = [("Configuration","Release")]
                ReleaseNotes = (__SOURCE_DIRECTORY__ @@ "ReleaseNotes.txt" |> File.ReadAllText)
            }) "ScrapySharp.nuspec"

Target "NuGet" (fun _ ->
    buildNuGet false None
)

Target "PublishNuGet" (fun _ ->
    buildNuGet true None
)

Target "NuGetBeta" (fun _ ->
    buildNuGet false (Some "-beta1")
)

Target "PublishNuGetBeta" (fun _ ->
    buildNuGet true (Some "-beta1")
)

"Clean"
    ==> "Packages"
    ==> "BuildRelease"
    ==> "NuGet"
    ==> "PublishNuGet"

"Clean"
    ==> "Packages"
    ==> "BuildRelease"
    ==> "NuGetBeta"
    ==> "PublishNuGetBeta"

RunTargetOrDefault "NuGet"


