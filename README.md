# TasksApi – API de Gestión de Tareas

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
TasksApi
│
├── Controllers
│   └── TasksController.cs
│
├── Data
│   └── ApplicationDbContext.cs
│
├── Models
│   └── Task.cs
│
├── Services
│   ├── ITaskService.cs
│   └── TaskService.cs
│
├── Program.cs
└── tasks.db

TasksApi.Tests
├── CustomWebApplicationFactory.cs
└── TasksControllerTests.cs
```

## Requisitos

- .NET 8 SDK
- Visual Studio 2022 o superior
- SQLite

## Instalación y ejecución

1. Clonar o descargar el repositorio.
2. Abrir la solución `TasksApi.sln` en Visual Studio.
3. Restaurar los paquetes NuGet.
4. Ejecutar las migraciones de Entity Framework Core si es necesario:

```bash
dotnet ef database update --project TasksApi
```

5. Ejecutar el proyecto:

```bash
dotnet run --project TasksApi
```

6. Abrir Swagger desde la dirección `/swagger` de la aplicación.

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

El proyecto `TasksApi.Tests` utiliza xUnit y una base de datos temporal en memoria.

Para ejecutar las pruebas:

```bash
dotnet test TasksApi.Tests
```

Pruebas implementadas:

1. Crear una tarea correctamente y comprobar el código 201.
2. Enviar una tarea sin título y comprobar el código 400.
3. Consultar una tarea inexistente y comprobar el código 404.

## Arquitectura

El proyecto utiliza separación de responsabilidades:

- **Controllers:** reciben las solicitudes HTTP y devuelven respuestas.
- **Services:** contienen la lógica de las operaciones.
- **Data:** administra el acceso a la base de datos mediante Entity Framework Core.
- **Models:** definen la estructura de los datos.

## Persistencia

La aplicación utiliza SQLite para almacenar las tareas de forma persistente.

Las pruebas automatizadas utilizan una base de datos independiente en memoria para evitar modificar los datos reales.