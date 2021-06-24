. ".\common.ps1" -f

# Clean all solutions

foreach ($solutionPath in $solutionPaths) {    
    $solutionAbsPath = (Join-Path $rootFolder $solutionPath)
    Set-Location $solutionAbsPath
    dotnet clean --configuration Release
    if (-Not $?) {
        Write-Host ("Clean failed for the solution: " + $solutionPath)
        Set-Location $rootFolder
        exit $LASTEXITCODE
    }
}

Set-Location $rootFolder
Write-Host 'Clean all solutions(Release) success!'
$null = [Console]::ReadKey('?')
