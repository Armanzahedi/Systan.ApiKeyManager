#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Systan.ApiKeyManager.Api/Systan.ApiKeyManager.Api.csproj", "Systan.ApiKeyManager.Api/"]
RUN dotnet restore "Systan.ApiKeyManager.Api/Systan.ApiKeyManager.Api.csproj"
COPY . .
WORKDIR "/src/Systan.ApiKeyManager.Api"
RUN dotnet build "Systan.ApiKeyManager.Api.csproj" -c Release -o /app/build

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh

FROM build AS publish
RUN dotnet publish "Systan.ApiKeyManager.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Systan.ApiKeyManager.Api.dll"]