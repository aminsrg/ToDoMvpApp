# ToDoMvpApp

**Version:** v2  
**Description:**  
ToDoMvpApp is a scalable .NET 8 Web API application for managing tasks. It follows clean architecture principles with separate Application, Infrastructure, and API layers. Features include JWT authentication, PostgreSQL persistence, Swagger/OpenAPI documentation, and task management with reminders and repeat options.

---

## Table of Contents

1. [Features](#features)  
2. [Requirements](#requirements)  
3. [Getting Started](#getting-started)  
4. [Running with Docker](#running-with-docker)  
5. [Configuration](#configuration)  
6. [Optional Logging & Error Handling](#optional-logging--error-handling)  
7. [Routing & Constants](#routing--constants)  
8. [Authentication & Authorization](#authentication--authorization)  
9. [Swagger Documentation](#swagger-documentation)  
10. [Project Structure](#project-structure)  
11. [Testing](#testing)  
12. [Future Improvements](#future-improvements)  

---

## Features

- **CRUD operations** for ToDo tasks  
- **User registration and login** with JWT authentication  
- **Customizable reminders** and repeat frequency (daily, weekly, monthly, yearly)  
- **Importance flag** for tasks  
- **Paging, filtering, and sorting** support in queries  
- **Swagger/OpenAPI v3 documentation**  

---

## Requirements

- .NET 8 SDK  
- Docker & Docker Compose  
- PostgreSQL 15+  
- Visual Studio 2022 / VS Code  

---

## Getting Started

1. **Clone the repository**

```bash
git clone https://github.com/your-repo/ToDoMvpApp.git
cd ToDoMvpApp
```

2. **Set environment variables** (for development, can be in `.env` or `launchSettings.json`)

```env
TODO_DB_CONNECTION=Host=localhost;Port=5432;Database=todomvpdb;Username=myuser;Password=mypassword
ASPNETCORE_ENVIRONMENT=Development
Jwt__Key=YourSuperSecretKey123!
Jwt__Issuer=ToDoMvpApp
```

3. **Run database migrations** (optional if using Docker)

```bash
dotnet ef database update --project ToDoMvpApp.Infrastructure --startup-project ToDoMvpApp.Api
```

---

## Running with Docker

Use Docker Compose for a complete environment:

```bash
docker-compose up --build
```

- **PostgreSQL**: `localhost:5432`  
- **API**: `http://localhost:5000`  
- **Swagger**: `http://localhost:5000/swagger`

---

## Configuration

- **Database connection**: Read from `TODO_DB_CONNECTION` environment variable  
- **JWT**: Configurable via `appsettings.Development.json` or environment variables  
- **Ports**: Configurable via `docker-compose.yml`  

---

## Optional Logging & Error Handling

- Logging middleware and structured error handling are **not implemented in this MVP**, but can be added:  
  - **Logging:** Use a custom middleware or integrate providers like Serilog, NLog, or Microsoft.Extensions.Logging.  
  - **Error handling:** Implement centralized exception middleware to return structured error responses for all API endpoints.  

---

## Routing & Constants

- Centralized **Constants.cs** file can be used for API routes and other shared constants, improving maintainability and avoiding hardcoded strings.  

---

## Authentication & Authorization

- JWT authentication is implemented using `Microsoft.AspNetCore.Authentication.JwtBearer`  
- Middleware reads `X-User-Id` claims for authorization purposes  
- Future improvement: integrate **ASP.NET Core Identity** for better user management, roles, and policies  

---

## Swagger Documentation

- Swagger/OpenAPI v3 integrated  
- JWT Bearer token can be used to authorize requests in Swagger UI  
- Multiple JSON content types are supported for commands  

---

## Using Swagger to Call Services

This section explains how to interact with the ToDoMvpApp API using Swagger based on the OpenAPI definition.

1. **Open Swagger UI**  
   Navigate to `http://localhost:5000/swagger` in your browser after running the API.

2. **Sign Up**  
   - Go to the `User → POST /api/User/signup` endpoint.  
   - Provide the required JSON body:
     ```json
     {
       "username": "your-username",
       "password": "your-password"
     }
     ```
   - Submit the request to create a new user.  
   - Response will be `200 OK` if successful.

3. **Login**  
   - Go to the `User → POST /api/User/login` endpoint.  
   - Provide the JSON body with your credentials:
     ```json
     {
       "username": "your-username",
       "password": "your-password"
     }
     ```
   - Copy the `token` value from the response (JWT access token).

4. **Authorize with JWT**  
   - In Swagger UI, click the **Authorize** button at the top.  
   - Enter the token in the format:  
     ```
     Bearer <your-token-here>
     ```
   - Click **Authorize** and then **Close**.  
   - This will attach the JWT token to all requests requiring authentication.

5. **Call ToDo Services**  
   - Now you can use endpoints under the `ToDo` tag:

     **Create ToDo** (`POST /api/ToDo`):
     ```json
     {
       "title": "My Task",
       "description": "Task description",
       "dueDate": "2025-09-28T15:00:00Z",
       "reminderAt": "2025-09-28T14:00:00Z"
     }
     ```

     **Update ToDo** (`PUT /api/ToDo`):
     ```json
     {
       "id": "task-id",
       "title": "Updated Task",
       "description": "Updated description",
       "dueDate": "2025-09-30T15:00:00Z",
       "isCompleted": false,
       "isImportant": true,
       "repeat": 1,
       "reminderAt": "2025-09-30T14:00:00Z"
     }
     ```

     **Get ToDo List** (`GET /api/ToDo`):
     - Optional query parameters: `Page`, `PageSize`, `IsCompleted`, `IsImportant`

     **Get ToDo by ID** (`GET /api/ToDo/{id}`)  
     **Delete ToDo** (`DELETE /api/ToDo/{id}`)  
     **Get ToDos by Date** (`GET /api/ToDo/by-date?date=2025-09-28T00:00:00Z`)

**Note:** Each time the JWT token expires, you must log in again and re-authorize in Swagger to continue calling protected endpoints.
---

## Project Structure

```
ToDoMvpApp/
├─ Api/                  # Web API project
├─ Application/          # CQRS Commands, Queries, Validators
├─ Infrastructure/       # Database, Repositories, Services
├─ Domain/               # Entities, Enums, Value Objects
├─ Shared/               # Common DTOs, Constants, Extensions
├─ Dockerfile
├─ docker-compose.yml
└─ README.md
```

- **CQRS separation**: Commands and Queries are separated for scalability  
- **Dependency Injection**: Static `DependencyInjection` classes per layer for clean DI  

---

## Testing

- In a full development workflow, **unit tests should be written first for each user story**, followed by implementation of the functionality.  
- **Note:** This MVP project does **not include automated tests**. Testing can be added later using xUnit, NUnit, or MSTest.  

---

## Future Improvements

- Full **ASP.NET Core Identity** support for roles and permissions  
- Redis caching for ToDo list queries  
- Advanced notification system for reminders  
- Integration tests and CI/CD pipeline  
- Centralized logging and structured error handling  

---

**Notes:**  
This project demonstrates clean architecture, Docker deployment, JWT authentication, and a structured approach to building a scalable ToDo API MVP. Sensitive information like JWT keys and database credentials should always be stored securely in environment variables or secret managers for production.

