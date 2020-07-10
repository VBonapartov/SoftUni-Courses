FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BookShop.Notifications/BookShop.Notifications.csproj", "BookShop.Notifications/"]
COPY ["BookShop/BookShop.csproj", "BookShop/"]
RUN dotnet restore "BookShop.Notifications/BookShop.Notifications.csproj"
COPY . .
WORKDIR "/src/BookShop.Notifications"
RUN dotnet build "BookShop.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.Notifications.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.Notifications.dll"]