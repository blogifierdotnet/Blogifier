FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine as sdk
# Copy everything else and build
COPY ./ /opt/blogifier
WORKDIR /opt/blogifier
RUN ["dotnet","publish", "-c", "Release","/p:RuntimeIdentifier=linux-musl-x64", "./src/Blogifier/Blogifier.csproj","-o","dist" ]

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine as run
COPY --from=sdk /opt/blogifier/dist /opt/blogifier/
WORKDIR /opt/blogifier
ENTRYPOINT ["dotnet", "Blogifier.dll"]
