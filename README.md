# Todo List Backend API (.NET 8)

Production-ready ASP.NET Core Web API for a TODO List backend using:

- Repository Pattern + Unit of Work Pattern
- JWT-based Authentication
- Entity Framework Core with SQLite
- Layered/OOP-focused folder structure
- Pagination support
- Global exception handling middleware

## Tech Stack

- .NET 8 / ASP.NET Core Web API
- EF Core 8 + SQLite
- JWT Bearer Authentication
- Swagger/OpenAPI

## Project Structure

```
TodoList.Api/
  Application/
    Common/
    DTOs/
    Exceptions/
    Interfaces/
    Services/
  Configuration/
  Controllers/
  Domain/
    Entities/
    Enums/
  Infrastructure/
    Data/
      Migrations/
    Persistence/
    Repositories/
  Middleware/
```

## Configuration

`TodoList.Api/appsettings.json`

- `ConnectionStrings:DefaultConnection`
- `Jwt:Issuer`
- `Jwt:Audience`
- `Jwt:Key` (must be at least 32 chars)
- `Jwt:ExpiryMinutes`

## Run Locally

1. Install .NET 8 SDK.
2. Restore/build:

```bash
dotnet restore
dotnet build
```

3. Apply migrations:

```bash
dotnet ef database update --project TodoList.Api/TodoList.Api.csproj --startup-project TodoList.Api/TodoList.Api.csproj
```

4. Run API:

```bash
dotnet run --project TodoList.Api/TodoList.Api.csproj
```

Swagger UI is available at `/swagger` in development mode.

## Main Endpoints

### Auth

- `POST /api/auth/register`
- `POST /api/auth/login`

### Todos (JWT required)

- `GET /api/todos?pageNumber=1&pageSize=10`
- `GET /api/todos/{id}`
- `POST /api/todos`
- `PUT /api/todos/{id}`
- `DELETE /api/todos/{id}`