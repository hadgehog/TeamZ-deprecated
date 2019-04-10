if (-not (Get-Command unity -errorAction SilentlyContinue))
{
    Write-Output "unity command is missing"
    Write-Output "Add unity folder to global path enviroment variable"
    exit 1
}

$token = $Env:GitHubToken

Write-Output "Build Unity project"

cmd /c unity  -batchmode -nographics -projectpath . -executeMethod Build.AppBuilder.BuildGame -quit

if (-not $args.Contains("--upload"))
{
    Write-Output "Done"
    exit 0
}

$timestamp = get-date -f yyyy.MM.dd
$tag = "v$($timestamp)"

$archiveName = "game_$($tag).zip"

Write-Output "Compressing"
Compress-Archive -Path BuildArtifacts/* -DestinationPath $archiveName -Force

$headers = 
@{ 
   "Content-Type"="application/json";
   "Accept" = "application/vnd.github.v3+json";
   "Authorization" = "token $($token)";
}

$createRelease = @"
{
    "tag_name": "$($tag)",
    "prerelease": true
}
"@

Write-Output "Creating release"

git tag -a "$($tag)" -m "Build $($tag)"
git push origin "$($tag)"
 
$rawResponse = Invoke-WebRequest -Uri https://api.github.com/repos/hadgehog/TeamZ/releases -Headers $headers -Method POST -Body $createRelease -UseBasicParsing
$response = $rawResponse.Content | ConvertFrom-Json
$assets = $response.assets_url -replace "api.github.com", "uploads.github.com"

Write-Output "Uploading archive"
Invoke-WebRequest -Uri "$($assets)?name=$($archiveName)" -Headers $headers -Method POST -InFile $archiveName -UseBasicParsing

Write-Output "Done"
