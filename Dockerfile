FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DiscordImport/DiscordImport.csproj", "DiscordImport/"]
RUN dotnet restore "DiscordImport/DiscordImport.csproj"
COPY . .
WORKDIR "/src/DiscordImport"
RUN dotnet build "DiscordImport.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiscordImport.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiscordImport.dll"]