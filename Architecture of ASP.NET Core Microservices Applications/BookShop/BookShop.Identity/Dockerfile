FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BookShop.Identity/BookShop.Identity.csproj", "BookShop.Identity/"]
COPY ["BookShop/BookShop.csproj", "BookShop/"]
RUN dotnet restore "BookShop.Identity/BookShop.Identity.csproj"
COPY . .
WORKDIR "/src/BookShop.Identity"
RUN dotnet build "BookShop.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.Identity.dll"]