FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BookShop.Books/BookShop.Books.csproj", "BookShop.Books/"]
COPY ["BookShop/BookShop.csproj", "BookShop/"]
RUN dotnet restore "BookShop.Books/BookShop.Books.csproj"
COPY . .
WORKDIR "/src/BookShop.Books"
RUN dotnet build "BookShop.Books.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.Books.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.Books.dll"]