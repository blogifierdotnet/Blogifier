# Local machine
rm -fr dist
dotnet publish -c Release /p:RuntimeIdentifier=linux-x64 ./src/Blogifier/Blogifier.csproj --output dist
