FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG GHPAT
ARG VERSION
WORKDIR /src
COPY ["Texnokaktus.ProgOlymp.YandexIdIntegrationService/Texnokaktus.ProgOlymp.YandexIdIntegrationService.csproj", "Texnokaktus.ProgOlymp.YandexIdIntegrationService/"]
COPY ["Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess/Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.csproj", "Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess/"]
COPY ["Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient/Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.csproj", "Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient/"]
COPY ["Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic/Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic.csproj", "Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic/"]
COPY ["Texnokaktus.ProgOlymp.YandexIdIntegrationService.Domain/Texnokaktus.ProgOlymp.YandexIdIntegrationService.Domain.csproj", "Texnokaktus.ProgOlymp.YandexIdIntegrationService.Domain/"]
COPY ["modules/Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts.csproj", "modules/Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts/"]
RUN dotnet nuget add source --username kav128 --password $GHPAT --store-password-in-clear-text --name "GitHub TexnoKaktus-Progolymp" https://nuget.pkg.github.com/Texnokaktus-ProgOlymp/index.json
RUN dotnet restore "Texnokaktus.ProgOlymp.YandexIdIntegrationService/Texnokaktus.ProgOlymp.YandexIdIntegrationService.csproj"
COPY . .
WORKDIR "/src/Texnokaktus.ProgOlymp.YandexIdIntegrationService"
RUN dotnet build "Texnokaktus.ProgOlymp.YandexIdIntegrationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Texnokaktus.ProgOlymp.YandexIdIntegrationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:Version=$VERSION

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Texnokaktus.ProgOlymp.YandexIdIntegrationService.dll"]
