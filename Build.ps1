echo "Build Unity project"

cmd /c unity  -batchmode -nographics -projectpath . -executeMethod Build.AppBuilder.BuildGame -quit

$branch = (git rev-parse --abbrev-ref HEAD) -replace "/", "-"
$commit = git rev-parse --short HEAD
$currentDate = Get-Date

$timestamp = get-date -f yyyy.MM.dd
$stamp = "$($timestamp)_$($branch)_$($commit)"
$tag = "v$($timestamp)"

$archiveName = "game_$($tag).zip"

echo "Compressing"
Compress-Archive -Path BuildArtifacts/* -DestinationPath $archiveName -Force

 $headers = 
 @{ 
    "Content-Type"="application/json";
    "Accept" = "application/vnd.github.v3+json";
    "Authorization" = "token 240e93d26bcf5e4a30c892ee09a4fc6c78defc92";
 }

$createRelease = @"
{
    "tag_name": "$($tag)",
    "prerelease": true
}
"@

echo "Creating release"

git tag -a "$($tag)" -m "Build $($tag)"
git push origin "$($tag)"
 
$rawResponse = Invoke-WebRequest -Uri https://api.github.com/repos/hadgehog/TeamZ/releases -Headers $headers -Method POST -Body $createRelease -UseBasicParsing
$response = $rawResponse.Content | ConvertFrom-Json
$assets = $response.assets_url -replace "api.github.com", "uploads.github.com"

echo "Uploading archive"
Invoke-WebRequest -Uri "$($assets)?name=$($archiveName)" -Headers $headers -Method POST -InFile $archiveName -UseBasicParsing

echo "Done"
