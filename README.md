# Yandex ID Integration Service

Сервис предоставляет gRPC API для авторизации через Яндекс ID: формирует OAuth URL и обменивает код на данные пользователя. Контракт `UserService` находится в `modules/Common.Contracts/proto/YandexId/UserService.proto` и компилируется в сервисе с серверными gRPC-типами.

## Структура

- `src/Texnokaktus.ProgOlymp.YandexIdIntegrationService/` — ASP.NET Core сервис и Dockerfile.
- `modules/Common.Contracts/` — Git submodule: схемы находятся в `proto/`, а проект контрактов содержит общие C# типы и использует собственный `Directory.Packages.props`.
- `Directory.Build.props`, `Directory.Packages.props` — общие настройки сборки и версии пакетов сервиса.
- `NuGet.Config` — источники nuget.org и GitHub Packages и привязка пакетов к источникам: `Texnokaktus.ProgOlymp.*` и `YandexOAuthClient*` — GitHub Packages, остальные — nuget.org.
- `.github/workflows/dotnet.yml` — сборка и публикация образа; deploy отключён.

## Подготовка и запуск

Нужны .NET SDK 10, доступ к Redis и GitHub Packages, а также OAuth-приложение Яндекса. Клонируйте репозиторий с submodule: `git clone --recurse-submodules <repository-url>`. В существующем клоне выполните `git submodule update --init --recursive`; URL submodule использует SSH.

Для приватных пакетов установите переменную `NuGetPackageSourceCredentials_github` в формате `Username=<github-user>;Password=<token>`, где токен имеет доступ на чтение пакетов организации. Например, в PowerShell:

```powershell
$env:NuGetPackageSourceCredentials_github = 'Username=<github-user>;Password=<token>'
dotnet restore
dotnet build --no-restore
dotnet test --no-build
```

Настройте `YandexAppParameters__ClientId`, `YandexAppParameters__ClientSecret` и `ConnectionStrings__DefaultRedis` через переменные окружения или другой безопасный источник конфигурации ASP.NET Core. Не сохраняйте секреты в репозитории. Redis нужен при запуске; ключи Data Protection хранятся в Redis, автоматическое создание ключей отключено, поэтому они должны быть доступны заранее.

```powershell
dotnet run --project src/Texnokaktus.ProgOlymp.YandexIdIntegrationService
```

По умолчанию сервис слушает HTTP/1 на 8080 и gRPC HTTP/2 на 8081; в Development — 5280 и 7253 соответственно. gRPC reflection включён только в Development. Адрес Redis по умолчанию задан в appsettings и обычно требует переопределения.

## Docker

Для сборки нужен Docker BuildKit и та же переменная `NuGetPackageSourceCredentials_github`. Передайте её как секрет сборки:

```powershell
docker build --secret id=nuget_credentials,env=NuGetPackageSourceCredentials_github --build-arg VERSION=1.0.0 -f src/Texnokaktus.ProgOlymp.YandexIdIntegrationService/Dockerfile -t yandex-id-integration-service .
```

При запуске передайте настройки OAuth и Redis через окружение или защищённый env-файл и опубликуйте необходимые порты. GitHub Actions передаёт токен через BuildKit secret; у `GITHUB_TOKEN` должен быть доступ к приватному пакету и публикации образа.

Сервис использует `Texnokaktus.ProgOlymp.Platform` для общей настройки логирования и телеметрии.
