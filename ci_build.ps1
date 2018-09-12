## Build
dotnet build

## Test
ForEach ($proj in (Get-ChildItem -Path tests -Recurse -Filter '*.csproj'))
{
	dotnet test --no-restore `
		--no-build `
		--configuration Release `
		$proj.FullName
}

## Package
if (${env:APPVEYOR_REPO_TAG} -eq $true)
{
	Write-Host "Building without suffix"
	
	# Tag pushes are packaged with the version in the `.csproj`
	dotnet pack --no-restore `
	  -o artifacts_nuget `
	  --configuration Release
}
else
{
	$suffix = ${env:APPVEYOR_REPO_BRANCH}${env:APPVEYOR_BUILD_NUMBER}
	Write-Host "Building with suffix=$suffix"

	# Non-tag pushes are packaged with a pre-release version
	dotnet pack --no-restore `
	  -o artifacts_nuget `
	  --version-suffix=$suffix `
	  --configuration Release
}
