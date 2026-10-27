# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG VERSION
ARG TARGETOS
ARG TARGETARCH
WORKDIR /src
COPY ["Directory.Build.props", "Directory.Packages.props", "NuGet.Config", "./"]
COPY ["src/Texnokaktus.ProgOlymp.YandexIdIntegrationService/Texnokaktus.ProgOlymp.YandexIdIntegrationService.csproj", "src/Texnokaktus.ProgOlymp.YandexIdIntegrationService/"]
RUN --mount=type=secret,id=nuget_credentials,required=true \
    export NuGetPackageSourceCredentials_github="$(cat /run/secrets/nuget_credentials)" && \
    dotnet restore "src/Texnokaktus.ProgOlymp.YandexIdIntegrationService/Texnokaktus.ProgOlymp.YandexIdIntegrationService.csproj" --runtime $TARGETOS-$TARGETARCH
COPY . .
WORKDIR "/src/src/Texnokaktus.ProgOlymp.YandexIdIntegrationService"
RUN dotnet build "./Texnokaktus.ProgOlymp.YandexIdIntegrationService.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Texnokaktus.ProgOlymp.YandexIdIntegrationService.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/publish /p:UseAppHost=false /p:Version=$VERSION

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Texnokaktus.ProgOlymp.YandexIdIntegrationService.dll"]
