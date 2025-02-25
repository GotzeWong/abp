$full = $args[0] 

. ".\common.ps1" $full

# Build all solutions   

Write-Host $solutionPaths

foreach ($solutionPath in $solutionPaths) {    
    $solutionAbsPath = (Join-Path $rootFolder $solutionPath)
    Set-Location $solutionAbsPath
    dotnet build
    if (-Not $?) {
        Write-Host ("Build failed for the solution: " + $solutionPath)
        Set-Location $rootFolder
        exit $LASTEXITCODE
    }
}

Set-Location $rootFolder
Write-Host 'Build all solutions(Debug) success!'
$null = [Console]::ReadKey('?')

