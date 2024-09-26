# Product Management API

Product Management API — это RESTful API, разработанный на .NET Core с использованием Entity Framework и MS SQL Server, предназначенный для управления продуктами. API поддерживает CRUD операции, пагинацию и выгрузку данных в формате CSV.

## 🛠️ Стек технологий

- **.NET 7** — основная платформа для разработки API.
- **Entity Framework Core** — ORM для работы с базой данных.
- **MS SQL Server** — система управления базами данных.
- **XUnit** — тестирование приложения.

## 📋 Предварительные требования

Убедитесь, что у вас установлены следующие инструменты:

- [.NET 7 SDK]([https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/en-us/download/dotnet/7.0))
- [SQL Server]([https://www.microsoft.com/sql-server/sql-server-downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads))
- [Visual Studio 2022]([https://visualstudio.microsoft.com/](https://visualstudio.microsoft.com/ru/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false)) с установленными компонентами .NET и MS SQL.

## 🚀 Запуск проекта

### 1. Клонирование репозитория

- bash
git clone https://github.com/B3kadil/ProductManagementAPI.git
cd ProductManagementAPI

### 2. Настройка строки подключения

Откройте проект в Visual Studio и обновите строку подключения в файле appsettings.json, заменив YOUR_SERVER_NAME на имя вашего сервера SQL:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ProductManagementDB;Trusted_Connection=True;"
}

### 3. Создание и применение миграций базы данных

Перейдите в Package Manager Console или используйте терминал Visual Studio и выполните команды:

# Создать миграцию
Add-Migration InitialCreate

# Применить миграцию и создать базу данных
Update-Database

### 4. Запуск API

Запустите API в Visual Studio:

- Нажмите F5 или выберите Debug > Start Debugging.
- API будет доступен по адресам:
  - https://localhost:5001 (HTTPS)
  - http://localhost:5000 (HTTP)

### 5. Тестирование API

Для тестирования API используйте инструменты вроде Postman, Swagger или любой другой REST клиент.

## 🧪 Запуск тестов

Чтобы выполнить тесты:

1. Откройте Test Explorer в Visual Studio: Test > Test Explorer.
2. Нажмите Run All для запуска всех тестов.

## 📂 Структура проекта

- Controllers — контроллеры API для обработки запросов.
- Data — настройки контекста базы данных.
- Models — модели данных, используемые в приложении.
- Migrations — миграции базы данных для управления версиями.

## 📝 Основные возможности

- CRUD операции — управление продуктами (создание, чтение, обновление и удаление).
- Пагинация — удобная работа с большими наборами данных.
- Экспорт в CSV — выгрузка данных о продуктах в удобный формат.
