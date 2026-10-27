# Инструкции для работы с репозиторием

## Устройство сервиса

- `src/Texnokaktus.ProgOlymp.YandexIdIntegrationService` — ASP.NET Core gRPC-сервис на .NET 10. Регистрация зависимостей и endpoints находится в `Program.cs`.
- `Services/Grpc/UserServiceImpl.cs` реализует методы `GetOAuthUrl` и `AuthenticateUser`. Он использует `IAuthService` для OAuth и `IYandexIdClient` для запроса данных пользователя.
- `Services/YandexIdClient.cs` обращается к `https://login.yandex.ru/info`; модель ответа находится в `Models/UserData.cs`. При изменении обработки ответа учитывайте соответствие полей gRPC-контракту.
- Protobuf-контракт `UserService` расположен в `modules/Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts/Protos/YandexId/UserService.proto`. Контракты генерируются из protobuf при сборке; не редактируйте сгенерированные файлы в `obj`.
- `modules/Common.Contracts` — Git submodule с собственными настройками и версиями пакетов. Не изменяйте его без необходимости. Для получения контрактов выполните `git submodule update --init --recursive`; при изменении контракта учитывайте совместимость клиентов и отдельно проверяйте состояние submodule.

## Сборка и зависимости

- Общие свойства сборки задаются в корневом `Directory.Build.props`; версии пакетов сервиса — в `Directory.Packages.props`. Не добавляйте версии в `PackageReference` при включённом CPM. При добавлении проекта выбирайте подходящий уровень общих настроек и включайте его в solution.
- Источники и привязка пакетов к ним описаны в `NuGet.Config`: `Texnokaktus.ProgOlymp.*` и `YandexOAuthClient*` используют GitHub Packages, остальные пакеты — nuget.org. При добавлении зависимости из другого источника обновляйте соответствующий mapping, включая транзитивные пакеты.
- Для GitHub Packages используйте `NuGetPackageSourceCredentials_github`; формат и подготовка окружения описаны в README. Не записывайте токены в файлы или Docker build-args.
- При переносе проектов обновляйте solution, ссылки на проекты, Dockerfile и workflow. Docker build запускается из корня репозитория; restore в Dockerfile копирует файлы проекта и общие настройки до остальных исходников.

## Конфигурация и запуск

- Запуск требует доступного Redis: подключение устанавливается при старте через `ConnectionStrings:DefaultRedis`. Data Protection хранит ключи в Redis, автоматическое создание ключей отключено. Учитывайте это при проверках запуска и изменении конфигурации.
- OAuth использует `YandexAppParameters:ClientId` и `YandexAppParameters:ClientSecret`. Передавайте значения через безопасную конфигурацию, не добавляйте секреты в исходники, логи и примеры.
- Порты и протоколы Kestrel заданы в `appsettings.json` и `appsettings.Development.json`; gRPC reflection включён только в Development. Не меняйте их без учёта клиентов и конфигурации развёртывания.
- `builder.UsePlatform()` подключает общую инфраструктуру логирования и телеметрии. При изменении наблюдаемости учитывайте поведение платформы и существующую конфигурацию Serilog.

## Проверки

- Из корня репозитория выполните `dotnet restore`, `dotnet build --no-restore` и `dotnet test --no-build`. Для восстановления нужен доступ к приватным пакетам либо подготовленный кеш. При недоступности зависимостей укажите границы проверки.
- В solution сейчас нет тестовых проектов, поэтому успешный `dotnet test` сам по себе не подтверждает поведение сервиса. При изменениях поведения добавляйте подходящие тесты и проверяйте их выполнение.
- Для проверки запуска или интеграции нужны Redis и настройки OAuth; не отправляйте реальные токены в вывод. Docker-изменения проверяйте сборкой образа при доступном Docker и учётных данных; команду с BuildKit secret см. в README.
- Workflow публикует образ при push в `master`; job deploy сейчас отключён. При изменении CI учитывайте восстановление submodule, доступ к GitHub Packages и сборку для `linux/amd64` и `linux/arm64`.

## Git

- Сохраняйте существующие пользовательские изменения, в том числе состояние submodule и staging area. Не включайте секреты и локальные настройки IDE в коммит.
- Пишите сообщения коммитов на английском языке, кратко, начиная со строчной буквы; избегайте многострочных сообщений.
