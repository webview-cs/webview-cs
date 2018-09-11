## Build
dotnet build

## Test
ForEach ($proj in (Get-ChildItem -Path tests -Recurse -Filter '*.csproj'))
{
	dotnet test --no-restore --no-build $proj.FullName
}

## Package
if (${env:APPVEYOR_REPO_TAG} -eq $true)
{
	# Tag pushes are packaged with the version in the `.csproj`
	dotnet pack --no-restore `
	  -o artifacts_nuget `
	  --configuration Release
}
else
{
	# Non-tag pushes are packaged with a pre-release version
	dotnet pack --no-restore `
	  -o artifacts_nuget `
	  --version-suffix=${env:APPVEYOR_REPO_BRANCH}${env:APPVEYOR_BUILD_NUMBER} `
	  --configuration Release
}
