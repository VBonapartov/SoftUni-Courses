FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BookShop.Watchdog/BookShop.Watchdog.csproj", "BookShop.Watchdog/"]
RUN dotnet restore "BookShop.Watchdog/BookShop.Watchdog.csproj"
COPY . .
WORKDIR "/src/BookShop.Watchdog"
RUN dotnet build "BookShop.Watchdog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.Watchdog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.Watchdog.dll"]