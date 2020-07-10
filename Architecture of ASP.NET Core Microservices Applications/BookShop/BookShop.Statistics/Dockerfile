FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BookShop.Statistics/BookShop.Statistics.csproj", "BookShop.Statistics/"]
COPY ["BookShop/BookShop.csproj", "BookShop/"]
RUN dotnet restore "BookShop.Statistics/BookShop.Statistics.csproj"
COPY . .
WORKDIR "/src/BookShop.Statistics"
RUN dotnet build "BookShop.Statistics.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.Statistics.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.Statistics.dll"]