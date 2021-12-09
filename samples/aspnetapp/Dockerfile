FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
#FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-20211109-windowsservercore-ltsc2019 AS build
#FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2019 AS build
#FROM mcr.microsoft.com/dotnet/framework/sdk:4.7.2 AS build

WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY aspnetapp/*.csproj ./aspnetapp/
COPY aspnetapp/*.config ./aspnetapp/
RUN nuget restore

# copy everything else and build app
COPY aspnetapp/. ./aspnetapp/
WORKDIR /app/aspnetapp
RUN msbuild /p:Configuration=Release -r:False
#RUN msbuild /p:Configuration=Release -r:False aspnetapp.sln

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS runtime
#FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-20211109-windowsservercore-ltsc2019 AS runtime
#FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019 AS runtime
#FROM mcr.microsoft.com/dotnet/framework/aspnet:4.7.2 AS runtime

WORKDIR /inetpub/wwwroot
COPY --from=build /app/aspnetapp/. ./
