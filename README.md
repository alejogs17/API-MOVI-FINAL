# ENTREGA TRABAJO API

## Información del Proyecto

**Nombre:** ENTREGA TRABAJO API

**Estudiante:** SAMIR ALEJANDRO GONZALEZ ALBIS

## API.W.Movies

API REST desarrollada en .NET para la gestión de películas y categorías.

---

## Mapeo de Implementación según la Guía

###  Estructura de Proyecto

```
API.W.Movies/
├── Controllers/          # Controladores REST
├── Services/            # Lógica de negocio
│   └── IServices/      # Interfaces de servicios
├── Repository/          # Acceso a datos
│   └── IRepository/    # Interfaces de repositorios
├── DAL/                 # Capa de acceso a datos
│   ├── Models/         # Entidades y DTOs
│   └── ApplicationDbContext.cs
├── MoviesMapper/        # Configuración de AutoMapper
├── Migrations/          # Migraciones de Entity Framework
└── Program.cs           # Configuración de la aplicación
```