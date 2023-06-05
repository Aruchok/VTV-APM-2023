#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build_env
WORKDIR /src

COPY . .
RUN dotnet restore "XMLParser/XMLParser.csproj"
RUN dotnet publish "XMLParser/XMLParser.csproj" -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS run_env
WORKDIR /app
COPY ["XMLParser/Resources/", "/app/Resources"]
COPY --from=build_env /app/publish .
ENTRYPOINT ["dotnet", "XMLParser.dll"]