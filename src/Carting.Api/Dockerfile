FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["/Carting/Carting.csproj", "./Carting/"]
COPY ["/Carting.Api/Carting.Api.csproj", "./Carting.Api/"]
RUN dotnet restore "./Carting.Api/Carting.Api.csproj"
COPY . .
WORKDIR "/src/Carting.Api"
RUN dotnet build "Carting.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Carting.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Carting.Api.dll"]