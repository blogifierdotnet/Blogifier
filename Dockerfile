FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
# Copy src/Blogifier/Blogifier.csproj file to /source/Blogifier/
COPY src/Blogifier/*.csproj Blogifier/
# Copy src/Blogifier.Admin/Blogifier.Admin.csproj file to /source/Blogifier.Admin/
COPY src/Blogifier.Admin/*.csproj Blogifier.Admin/
# Copy src/Blogifier.Core/Blogifier.Core.csproj file to /source/Blogifier.Core/
COPY src/Blogifier.Core/*.csproj Blogifier.Core/
# Copy src/Blogifier.Core/Blogifier.Core.csproj file to /source/Blogifier.Core/
COPY src/Blogifier.Shared/*.csproj Blogifier.Shared/
# restore
RUN dotnet restore Blogifier/Blogifier.csproj

# copy everything else and build app
COPY src/Blogifier/. Blogifier/
COPY src/Blogifier.Admin/. Blogifier.Admin/
COPY src/Blogifier.Core/. Blogifier.Core/
COPY src/Blogifier.Shared/. Blogifier.Shared/
RUN dotnet publish Blogifier/Blogifier.csproj -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT [ "dotnet", "Blogifier.dll"]