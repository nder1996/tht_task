# 📋 Sistema de Gestión de Tareas

Un sistema completo de gestión de tareas desarrollado con ASP.NET Core 6+ (backend) y Angular 14+ (frontend), implementando principios de Clean Architecture y Domain-Driven Design.

![GitHub](https://img.shields.io/badge/.NET%206+-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![GitHub](https://img.shields.io/badge/Angular%2014+-DD0031?style=for-the-badge&logo=angular&logoColor=white)
![GitHub](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)

## 🚀 Características Principales

- ✅ CRUD completo de tareas (Crear, Leer, Actualizar, Eliminar)
- 📊 Seguimiento de estados de tareas (PENDIENTE, EN PROGRESO, COMPLETADO)
- 📅 Gestión de fechas de vencimiento con validación
- 🔍 Filtrado avanzado y paginación
- 🛡️ Manejo robusto de errores y excepciones
- 📱 Diseño de interfaz adaptable (responsive)
- 🌐 Arquitectura API RESTful
- 🔒 Soft delete para tareas (estado activo/inactivo)

## 📐 Arquitectura del Proyecto

### 🏗️ Backend (ASP.NET Core)

El proyecto backend implementa Clean Architecture con una clara separación de responsabilidades:

```
Backend/
├── Application/           # Lógica de aplicación y DTOs
│   ├── Dtos/              # Objetos de transferencia de datos
│   └── Service/           # Servicios de aplicación
├── Domain/                # Entidades y reglas de negocio
│   ├── Entities/          # Entidades de dominio (TaskEntity)
│   ├── Exceptions/        # Excepciones personalizadas
│   └── Interfaces/        # Contratos de repositorios y servicios
├── Infrastructure/        # Implementaciones técnicas
│   └── Persistence/       # Capa de acceso a datos
│       ├── DBContext/     # Contexto de Entity Framework
│       └── Repository/    # Implementación de repositorios
└── WebApi/                # Capa de presentación
    ├── Controllers/       # Controladores REST
    ├── Extensions/        # Extensiones de aplicación
    ├── Filters/           # Filtros para cross-cutting concerns
    └── Middleware/        # Componentes de pipeline HTTP
```

#### 🧩 Patrones de Diseño Implementados (Backend)

- **Patrón Repositorio**: Abstracción para operaciones de persistencia
- **Inyección de Dependencias**: Configurada a través de los servicios de .NET Core
- **Builder Pattern**: Implementado en el `ResponseApiBuilderService` para construir respuestas estandarizadas
- **Middleware Pipeline**: Gestión de excepciones y validación centralizada
- **Filtros de Acción**: Para validación y logging transversal
- **Entidades ricas**: Con validación a nivel de dominio

### 🎨 Frontend (Angular)

La arquitectura del frontend sigue una estructura organizada por módulos y capas de responsabilidad:

```
Frontend/
└── task-management/
    └── src/
        └── app/
            └── module/
                └── task/
                    ├── application/     # Modelos y DTOs
                    │   └── dtos/        # Data Transfer Objects
                    ├── domain/          # Interfaces y contratos
                    ├── infrastructure/  # Implementaciones
                    │   └── adapters/    # Adaptadores para API
                    └── presentation/    # Componentes UI
                        ├── components/  # Componentes reutilizables
                        └── pages/       # Páginas principales
```

#### 🧩 Patrones de Diseño Implementados (Frontend)

- **Arquitectura por Módulos**: Separación por funcionalidad
- **Patrón Repositorio**: Implementado a través de servicios Angular
- **Patrón Observador**: Programación reactiva con RxJS
- **Componentes Reutilizables**: Encapsulación de lógica UI
- **Reactive Forms**: Manejo de formularios con validación
- **Servicios Singleton**: Inyección de dependencias

## 🌐 API RESTful

La API sigue principios REST con respuestas estandarizadas:

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/v1/tasks` | Obtener todas las tareas activas |
| GET | `/api/v1/tasks/{id}` | Obtener tarea por ID |
| POST | `/api/v1` | Crear nueva tarea |
| PUT | `/api/v1/{id}` | Actualizar tarea existente |
| DELETE | `/api/v1/{id}` | Inactivar tarea (soft delete) |

### 📊 Estructura de Respuesta API

Todas las respuestas de la API siguen un formato estandarizado:

```json
{
  "meta": {
    "message": "Operación Exitosa",
    "statusCode": 200
  },
  "data": {
    // Datos de respuesta...
  },
  "error": null // Solo presente en caso de error
}
```

### ⚠️ Manejo de Errores

El sistema implementa un manejo robusto de errores con:

- Mensajes personalizados por tipo de error
- Códigos HTTP apropiados
- Información detallada para depuración
- Middleware especializado para captura global de excepciones

## 💾 Modelo de Datos

El sistema utiliza PostgreSQL con la siguiente estructura principal:

```sql
CREATE TABLE tasks (
    id SERIAL PRIMARY KEY,
    title VARCHAR(100) NOT NULL,
    description TEXT,
    due_date TIMESTAMP WITH TIME ZONE,
    status VARCHAR(100) NOT NULL, -- (PENDIENTE, EN PROGRESO, COMPLETADO)
    state CHARACTER(1) NOT NULL,  -- (A: Activo, I: Inactivo)
    create_at TIMESTAMP WITH TIME ZONE,
    update_at TIMESTAMP WITH TIME ZONE
);
```

## 🧰 Tecnologías Utilizadas

### 🔙 Backend:
- **ASP.NET Core 6+**: Framework web moderno y performante
- **Entity Framework Core**: ORM para acceso a datos
- **PostgreSQL**: Base de datos relacional
- **Serilog**: Logging estructurado
- **Npgsql**: Proveedor de PostgreSQL para .NET
- **Swagger/OpenAPI**: Documentación de API

### 🔝 Frontend:
- **Angular 14+**: Framework para aplicaciones SPA
- **PrimeNG**: Biblioteca de componentes UI
- **RxJS**: Programación reactiva
- **TypeScript**: Lenguaje tipado para desarrollo frontend
- **Angular Forms**: Formularios reactivos
- **Primeflex**: Utilidades CSS para layout

## 🔍 Características de Calidad de Código

- **Logging estructurado**: Implementado con Serilog
- **Validación automática**: A través de filtros y atributos
- **Manejo global de excepciones**: Middleware especializado
- **Respuestas API estandarizadas**: Formato consistente
- **Restricciones de integridad**: Validación en múltiples capas
- **SOLID**: Implementación de principios sólidos de diseño

## 🚀 Configuración e Instalación

### Requisitos Previos
- SDK .NET 6+
- Node.js y NPM
- Conexión a internet (PostgreSQL está alojado en la nube en alwaysdata.net)

### ⚙️ Configuración del Backend
1. Navega al directorio `Backend/task-management`
2. La cadena de conexión ya está configurada para la base de datos en la nube
3. Ejecuta `dotnet restore`
4. Ejecuta `dotnet run`

### ⚙️ Configuración del Frontend
1. Navega al directorio `Frontend/task-management`
2. Ejecuta `npm install`
3. Ejecuta `ng serve`
4. Accede a la aplicación en `http://localhost:4200`

### 📚 Bibliotecas Principales

#### Backend (.NET)
```bash
dotnet add package Microsoft.EntityFrameworkCore v6.0.25
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL v6.0.22
dotnet add package Serilog v3.0.1
dotnet add package Serilog.Extensions.Logging v7.0.0
dotnet add package Serilog.Sinks.Console v4.1.0
dotnet add package Serilog.Sinks.File v5.0.0
```

#### Frontend (Angular)
```bash
# Angular core v14.3.0
npm install primeng v14.2.3
npm install primeicons v6.0.1
npm install @angular/animations v14.3.0
npm install primeflex v3.3.1
npm install rxjs v7.8.1
```

## 📐 Principios de Arquitectura Aplicados

### ✨ Clean Architecture

El sistema implementa Clean Architecture con las siguientes capas:

1. **Capa de Dominio**: Entidades, interfaces de repositorio y reglas de negocio
2. **Capa de Aplicación**: Servicios, DTOs y orquestación de lógica de negocio
3. **Capa de Infraestructura**: Implementaciones concretas de repositorios y servicios externos
4. **Capa de Presentación**: Controllers REST (backend) y componentes Angular (frontend)

### 🛡️ Principios SOLID

- **S**: Cada clase tiene una única responsabilidad (ej: TaskService, TaskRepository)
- **O**: Las clases están abiertas para extensión (ej: middleware extensible)
- **L**: Los subtipos son sustituibles por sus tipos base (ej: ApiException y sus derivadas)
- **I**: Interfaces específicas para cada necesidad (ITaskService, ITaskRepository)
- **D**: Dependencia de abstracciones (controladores dependen de interfaces)

