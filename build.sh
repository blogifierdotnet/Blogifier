
# Local machine
# dotnet publish -c Release /p:RuntimeIdentifier=linux-x64 ./src/Blogifier/Blogifier.csproj --output dist

# docker
docker build -t dorthl/blogifier:latest .
docker push dorthl/blogifier:latest
