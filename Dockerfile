FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
COPY dist /opt/blogifier/
WORKDIR /opt/blogifier
ENTRYPOINT ["dotnet", "Blogifier.dll"]
