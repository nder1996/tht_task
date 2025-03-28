# 📋 Sistema de Gestión de Tareas

Un sistema completo de gestión de tareas desarrollado con ASP.NET Core y Angular, implementando arquitectura limpia y principios sólidos de ingeniería de software.

## 🚀 Características

- ✅ CRUD completo de tareas (Crear, Leer, Actualizar, Eliminar)
- 📊 Seguimiento de estados de tareas (PENDIENTE, EN PROGRESO, COMPLETADO)
- 📅 Gestión de fechas de vencimiento con validación
- 🔍 Filtrado avanzado y paginación
- 🛡️ Manejo robusto de errores y excepciones
- 📱 Diseño de interfaz adaptable (responsive)
- 🌐 Arquitectura API RESTful

## 🏗️ Arquitectura del Proyecto

Este proyecto implementa Clean Architecture (Arquitectura Limpia) y principios de Domain-Driven Design (DDD):

### Backend (ASP.NET Core)

```
Backend/
├── Application/           # Servicios de aplicación, DTOs, interfaces de servicio
│   ├── Dtos/              # Objetos de transferencia de datos (Request/Response)
│   └── Service/           # Implementación de servicios de aplicación
├── Domain/                # Entidades centrales del dominio e interfaces
│   ├── Entities/          # Entidades de dominio (TaskEntity)
│   ├── Exceptions/        # Excepciones personalizadas
│   └── Interfaces/        # Contratos de repositorios y servicios
├── Infrastructure/        # Implementaciones de acceso a datos
│   └── Persistence/       # Implementación de persistencia
│       ├── DBContext/     # Contexto de Entity Framework
│       └── Repository/    # Implementación de repositorios
└── WebApi/                # Controladores, middleware y puntos de entrada API
    ├── Controllers/       # Controladores de API
    ├── Extensions/        # Extensiones de aplicación
    ├── Filters/           # Filtros de acción y validación
    └── Middleware/        # Componentes de pipeline HTTP
```

#### 🧩 Patrones de Diseño Implementados

- **Patrón Repositorio**: Abstrae la lógica de acceso a datos (TaskRepository)
- **Inyección de Dependencias**: Utilizado en toda la aplicación para lograr bajo acoplamiento
- **Separación tipo CQRS**: División conceptual entre operaciones de lectura y escritura
- **Patrón Builder**: Para construir respuestas API estructuradas (ResponseApiBuilderService)
- **Patrón Mediador**: Para gestionar el flujo de solicitudes HTTP a través de middleware
- **Unidad de Trabajo**: Para la gestión de transacciones de base de datos
- **Patrón DTO**: Para transferencia de datos entre capas
- **Filtros de Acción**: Para validación y logging transversal (ValidationFilter, LogOperationAttribute)

### Frontend (Angular)

```
Frontend/
└── task-management/
    └── src/
        └── app/
            └── module/
                └── task/
                    ├── application/     # DTOs y modelos de aplicación
                    │   └── dtos/        # Objetos de transferencia de datos
                    ├── domain/          # Interfaces y contratos
                    ├── infrastructure/  # Implementaciones de repositorio
                    │   └── adapters/    # Adaptadores de API
                    └── presentation/    # Componentes y páginas
                        ├── components/  # Componentes reutilizables
                        └── pages/       # Páginas de la aplicación
```

#### 🧩 Patrones de Diseño en el Frontend

- **Patrón de Módulos**: Organización modular para escalabilidad
- **Patrón Repositorio**: Abstracción de acceso a datos (TaskRepository)
- **Patrón Observador**: Programación reactiva con RxJS
- **Patrón de Componentes**: Composición de interfaz de usuario
- **Inyección de Dependencias**: Provisión de servicios
- **Patrón MVVM**: Aprovechando el sistema de binding de Angular
- **Form Builder**: Construcción programática de formularios reactivos



## 🌐 Endpoints de la API

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/Task/tasks` | Obtener todas las tareas |
| GET | `/api/Task/tasks/{id}` | Obtener tarea por ID |
| POST | `/api/Task` | Crear nueva tarea |
| PUT | `/api/Task/{id}` | Actualizar tarea existente |
| DELETE | `/api/Task/{id}` | Eliminar tarea por ID |
| GET | `/api/Task/debug-table` | Endpoint de diagnóstico para estructura de tabla |

## 💾 Base de Datos

La aplicación utiliza PostgreSQL alojado en la nube (alwaysdata.net) con la siguiente estructura principal:

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

La cadena de conexión ya está configurada en el proyecto para conectar a la instancia en la nube.

## 📊 Características de Calidad de Código

- **Logging estructurado**: Utilizando Serilog para registro detallado
- **Logging de operaciones**: Mediante filtros de acción personalizados
- **Validación de modelos**: Filtros para validación automática
- **Manejo global de excepciones**: Mediante middleware especializado
- **Middleware para métodos no permitidos**: Control de métodos HTTP
- **Validación de base de datos**: Control de restricciones de integridad
- **Respuestas API estandarizadas**: Formato consistente

## 🔧 Tecnologías Utilizadas

### Backend:
- ASP.NET Core (.NET 6+)
- Entity Framework Core
- PostgreSQL
- Serilog (Logging estructurado)
- Npgsql (Proveedor PostgreSQL)

### Frontend:
- Angular (v14+)
- PrimeNG (Componentes UI)
- RxJS (Programación reactiva)
- TypeScript
- Angular Forms (Formularios reactivos)

## 🚀 Configuración e Instalación

### Requisitos
- SDK .NET 6+
- Node.js y NPM

### Librerías y Paquetes a Instalar

#### Backend (.NET)
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Serilog
dotnet add package Serilog.Extensions.Logging
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
```

#### Frontend (Angular)
```bash
# Instalación principal
npm install

# Componentes PrimeNG
npm install primeng
npm install primeicons
npm install @angular/animations
npm install primeflex

# RxJS para programación reactiva
npm install rxjs
```

### Configuración del Backend
1. Navega al directorio `Backend/task-management`
2. La cadena de conexión ya está configurada para la base de datos en la nube
3. Ejecuta `dotnet restore`
4. Ejecuta `dotnet run`

### Configuración del Frontend
1. Navega al directorio `Frontend/task-management`
2. Ejecuta `npm install`
3. Configura la URL de la API en los archivos de environments
4. Ejecuta `ng serve`



---

## 📐 Principios de Arquitectura Aplicados

### Implementación de Clean Architecture

La aplicación implementa Clean Architecture con las siguientes capas bien definidas:

1. **Capa de Dominio** - Lógica de negocio central, entidades e interfaces de dominio
2. **Capa de Aplicación** - Servicios de aplicación, DTOs y casos de uso
3. **Capa de Infraestructura** - Aspectos externos como base de datos, sistemas de archivos y servicios de terceros
4. **Capa de Presentación** - Interfaz de usuario o endpoints API

Esta separación garantiza:
- Independencia de frameworks
- Testabilidad
- Independencia de UI
- Independencia de base de datos
- Independencia de agentes externos

### Aplicación de Principios SOLID

- **Principio de Responsabilidad Única (SRP)**: Cada clase tiene una única responsabilidad
- **Principio Abierto/Cerrado (OCP)**: Las clases están abiertas para extensión pero cerradas para modificación
- **Principio de Sustitución de Liskov (LSP)**: Los subtipos son sustituibles por sus tipos base
- **Principio de Segregación de Interfaces (ISP)**: Múltiples interfaces específicas en lugar de una interfaz de propósito general
- **Principio de Inversión de Dependencias (DIP)**: Los módulos de alto nivel dependen de abstracciones, no de implementaciones concretas
