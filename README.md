
﻿# TasksApi – API de Gestión de Tareas

## Descripción

TasksApi es una API REST desarrollada con ASP.NET Core 8 que permite gestionar tareas mediante operaciones CRUD (crear, consultar, actualizar y eliminar).

El proyecto utiliza SQLite para almacenar los datos y Entity Framework Core para el acceso a la base de datos.

## Tecnologías utilizadas

- C#
- ASP.NET Core 8 Web API
- Entity Framework Core 8
- SQLite
- Swagger / OpenAPI
- xUnit
- Entity Framework Core InMemory para pruebas automatizadas

## Estructura del proyecto

```text
TasksApi.sln
├── Dockerfile
├── docker-compose.yml
├── src
│   ├── TasksApi.Domain            # Núcleo: entidades y contratos (sin dependencias)
│   │   ├── Entities/TaskItem.cs
│   │   └── Repositories/ITaskRepository.cs
│   │
│   ├── TasksApi.Application       # Casos de uso: servicios y DTOs
│   │   ├── DTOs/ (CreateTaskDto, UpdateTaskDto, TaskDto)
│   │   ├── Interfaces/ITaskService.cs
│   │   ├── Services/TaskService.cs
│   │   └── DependencyInjection.cs
│   │
│   ├── TasksApi.Infrastructure    # Acceso a datos con EF Core + SQLite
│   │   ├── Persistence/ApplicationDbContext.cs
│   │   ├── Persistence/Configurations/TaskItemConfiguration.cs
│   │   ├── Persistence/Migrations/
│   │   ├── Repositories/TaskRepository.cs
│   │   └── DependencyInjection.cs
│   │
│   └── TasksApi.Api               # Presentación: controladores y arranque
│       ├── Controllers/TasksController.cs
│       ├── Program.cs
│       └── appsettings.json
│
└── tests
    └── TasksApi.Tests
        ├── CustomWebApplicationFactory.cs
        └── TasksControllerTests.cs
```

## Requisitos

- .NET 8 SDK
- Visual Studio 2022 o superior (opcional)
- Docker Desktop (opcional, para ejecutar en contenedor)

## Instalación y ejecución

1. Clonar o descargar el repositorio.
2. Ejecutar el proyecto (las migraciones se aplican automáticamente al iniciar y se crea `tasks.db`):

```bash
dotnet run --project src/TasksApi.Api
```

3. Abrir Swagger en `http://localhost:5123/swagger`.

Para crear una nueva migración:

```bash
dotnet ef migrations add NombreMigracion --project src/TasksApi.Infrastructure --startup-project src/TasksApi.Api -o Persistence/Migrations
```

## Ejecución con Docker

```bash
docker compose up -d --build
```

- API: `http://localhost:8080/api/Tasks`
- Swagger: `http://localhost:8080/swagger`
- La base SQLite se guarda en el volumen `tasks-data` (`/app/data/tasks.db`), por lo que los datos persisten entre reinicios del contenedor.

Para detener el contenedor: `docker compose down` (añadir `-v` para borrar también los datos).

## Endpoints disponibles

| Método | Endpoint | Descripción |
|---|---|---|
| GET | /api/Tasks | Obtener todas las tareas |
| GET | /api/Tasks/{id} | Obtener una tarea por ID |
| POST | /api/Tasks | Crear una tarea |
| PUT | /api/Tasks/{id} | Actualizar una tarea |
| DELETE | /api/Tasks/{id} | Eliminar una tarea |

## Modelo de tarea

```json
{
  "id": 1,
  "title": "Estudiar programación",
  "description": "Practicar ASP.NET Core",
  "isCompleted": false
}
```

El campo `title` es obligatorio y no puede superar los 100 caracteres.

## Códigos de respuesta HTTP

- 200 OK: consulta realizada correctamente.
- 201 Created: tarea creada correctamente.
- 204 No Content: tarea actualizada o eliminada correctamente.
- 400 Bad Request: solicitud inválida o datos incorrectos.
- 404 Not Found: tarea no encontrada.

## Pruebas automatizadas

El proyecto `TasksApi.Tests` utiliza xUnit y una base de datos SQLite temporal en memoria.

Para ejecutar las pruebas:

```bash
dotnet test
```

Pruebas implementadas:

1. Crear una tarea correctamente y comprobar el código 201.
2. Enviar una tarea sin título y comprobar el código 400.
3. Consultar una tarea inexistente y comprobar el código 404.
4. Actualizar y eliminar una tarea y comprobar los códigos 204 y 404.

## Arquitectura

El proyecto sigue Clean Architecture. Las dependencias apuntan siempre hacia el núcleo:

```text
Api  ──►  Application  ──►  Domain
 │                            ▲
 └──►  Infrastructure  ───────┘
```

- **Domain:** entidad `TaskItem` y el contrato `ITaskRepository`. No depende de ningún otro proyecto ni de EF Core.
- **Application:** lógica de negocio (`TaskService`), DTOs de entrada/salida y validaciones. Solo conoce el Domain.
- **Infrastructure:** implementa `ITaskRepository` con Entity Framework Core y SQLite; contiene el `DbContext` y las migraciones.
- **Api:** controladores HTTP y composición de dependencias (`AddApplication()` y `AddInfrastructure()`).

Los controladores reciben y devuelven DTOs; la entidad de dominio nunca se expone directamente en la API.

## Persistencia

La aplicación utiliza SQLite para almacenar las tareas de forma persistente.

Las pruebas automatizadas utilizan una base de datos independiente en memoria para evitar modificar los datos reales.
=======
# TasksApi
API REST para gestión de tareas con ASP.NET Core 8
origin/main
