FROM mcr.microsoft.com/dotnet/sdk:7.0 as base

# Copy everything else and build
COPY ./ /opt/blogifier
WORKDIR /opt/blogifier

RUN ["dotnet","publish","./src/Blogifier/Blogifier.csproj","-o","./outputs" ]

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as run
COPY --from=base /opt/blogifier/outputs /opt/blogifier/outputs
WORKDIR /opt/blogifier/outputs
ENTRYPOINT ["dotnet", "Blogifier.dll"]