FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine as sdk
# TOTO zh-CH
RUN sed -i 's/dl-cdn.alpinelinux.org/mirrors.aliyun.com/g' /etc/apk/repositories
RUN apk add --no-cache npm
# Copy everything else and build
COPY ./ /opt/blogifier
WORKDIR /opt/blogifier
RUN ["dotnet","publish", "-c", "Release","/p:RuntimeIdentifier=linux-musl-x64", "./src/Blogifier/Blogifier.csproj","-o","dist" ]

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine as run
# TOTO zh-CH
RUN sed -i 's/dl-cdn.alpinelinux.org/mirrors.aliyun.com/g' /etc/apk/repositories
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=sdk /opt/blogifier/dist /opt/blogifier/
WORKDIR /opt/blogifier
ENTRYPOINT ["dotnet", "Blogifier.dll"]
