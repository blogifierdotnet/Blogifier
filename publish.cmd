rmdir dist /s/q
dotnet publish -c Release /p:RuntimeIdentifier=win-x64 ./src/Blogifier/Blogifier.csproj -v minimal --output dist
