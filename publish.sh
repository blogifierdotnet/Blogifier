# Local machine
rm -fr dist
dotnet publish -c Release /p:RuntimeIdentifier=linux-x64 ./src/Blogifier/Blogifier.csproj --output dist

# docker
# rm -fr dist
# dotnet publish -c Release /p:RuntimeIdentifier=linux-musl-x64 ./src/Blogifier/Blogifier.csproj --output dist
# docker build -t dorthl/blogifier:latest .
# docker push dorthl/blogifier:latest
