FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BookShop.Reviews/BookShop.Reviews.csproj", "BookShop.Reviews/"]
COPY ["BookShop/BookShop.csproj", "BookShop/"]
RUN dotnet restore "BookShop.Reviews/BookShop.Reviews.csproj"
COPY . .
WORKDIR "/src/BookShop.Reviews"
RUN dotnet build "BookShop.Reviews.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.Reviews.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.Reviews.dll"]