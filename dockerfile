# BUILD
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ./src/ ./src/
COPY ./tests/ ./tests/
RUN ls
COPY *.sln ./
RUN dotnet publish -c Release -o build/output

# RUN IMAGE
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS run
WORKDIR /app
COPY --from=build /app/build/output .
ENV ASPNETCORE_LOCALSITEURL="http://localhost:80"
EXPOSE 80
ENTRYPOINT ["dotnet", "Blogifier.dll"]