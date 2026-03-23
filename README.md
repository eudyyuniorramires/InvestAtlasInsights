# InvestAtlasInsights 🌍📈

> **InvestAtlasInsights** es una plataforma diseñada para proporcionar análisis y perspectivas (insights) detalladas sobre datos de inversión y finanzas. Construida con tecnolodeogías mrnas y siguiendo los principios de la Arquitectura Limpia (Clean Architecture) para garantizar un código mantenible, escalable y testeable.

---

## 🏗 Arquitectura del Proyecto

El proyecto está diseñado siguiendo una **Arquitectura en Capas / Clean Architecture**, dividida en los siguientes módulos principales:

*   **`InvestAtlasInsights` (Capa de Presentación / API):** Contiene los controladores, la configuración de la API (REST/GraphQL), middlewares y la inyección de dependencias (IoC). Es el punto de entrada de la aplicación.
*   **`Application` (Capa de Aplicación):** Contiene la lógica de negocio, casos de uso, interfaces, DTOs (Data Transfer Objects), validaciones y manejadores de comandos/consultas (CQRS).
*   **`Persistence` (Capa de Persistencia / Infraestructura):** Encargada del acceso a datos. Aquí se encuentran los contextos de base de datos (Ej. Entity Framework Core), implementaciones de repositorios, migraciones y conexión con servicios externos.

---

## 🚀 Tecnologías Utilizadas

*   **Framework:** [.NET 8 / .NET 9] *(Ajustar según la versión exacta)*
*   **Lenguaje:** C#
*   **Arquitectura:** Clean Architecture, CQRS (opcional: MediatR)
*   **ORM:** Entity Framework Core
*   **Base de Datos:** [SQL Server / PostgreSQL / MySQL] *(Ajustar según tu DB)*
*   **Autenticación/Autorización:** JWT (JSON Web Tokens) *(Si aplica)*
*   **Documentación de API:** Swagger / OpenAPI

---

## ⚙️ Prerrequisitos

Para ejecutar este proyecto de manera local, necesitas tener instalado:

1.  [.NET SDK](https://dotnet.microsoft.com/download) (Versión correspondiente).
2.  Un entorno de desarrollo como [Visual Studio 2022](https://visualstudio.microsoft.com/), [JetBrains Rider](https://www.jetbrains.com/rider/) o [Visual Studio Code](https://code.visualstudio.com/).
3.  Servidor de Base de Datos compatible instalado y corriendo en tu máquina (ej. SQL Server, Docker, etc.).
4.  *(Opcional)* [Docker](https://www.docker.com/) si usas contenedores para correr la base de datos o la aplicación.

---

## 🛠 Instalación y Configuración Local

Sigue estos pasos para levantar el entorno de desarrollo:

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/eudyyuniorramires/InvestAtlasInsights.git
   cd InvestAtlasInsights
   ```

2. **Configurar la Cadena de Conexión (Connection String):**
   Abre el archivo `appsettings.json` o `appsettings.Development.json` dentro del proyecto principal (`InvestAtlasInsights`) y configura tu cadena de conexión a la base de datos:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=TU_SERVIDOR;Database=InvestAtlasDB;User Id=TU_USUARIO;Password=TU_CONTRASEÑA;TrustServerCertificate=True;"
   }
   ```

3. **Aplicar Migraciones de Base de Datos:**
   Abre una terminal en la raíz de la solución y ejecuta las migraciones para crear las tablas en tu base de datos:
   ```bash
   dotnet ef database update --project Persistence --startup-project InvestAtlasInsights
   ```
   *(Nota: Asegúrate de tener instaladas las herramientas de EF Core: `dotnet tool install --global dotnet-ef`)*

4. **Ejecutar el Proyecto:**
   Puedes correr el proyecto desde Visual Studio o usando el CLI de .NET:
   ```bash
   dotnet run --project InvestAtlasInsights
   ```

5. **Explorar la API:**
   Una vez que el proyecto esté corriendo, puedes acceder a la interfaz de Swagger para probar los endpoints:
   *   `https://localhost:<puerto>/swagger`

---

## 📂 Estructura de Carpetas

```plaintext
InvestAtlasInsights/
│
├── Application/                 # Lógica de negocio, Casos de uso (CQRS, MediatR), Interfaces
│   └── Application.csproj
│
├── InvestAtlasInsights/         # API Web (Controladores, Program.cs, appsettings.json)
│   └── InvestAtlasInsights.csproj
│
├── Persistence/                 # Acceso a datos (EF Core, DbContext, Repositorios, Migrations)
│   └── Persistence.csproj
│
├── InvestAtlasInsights.sln      # Solución principal de Visual Studio
├── .gitignore                   # Archivos ignorados por Git
└── .gitattributes               # Configuraciones de atributos de Git
```

---

## 🤝 Cómo Contribuir

¡Las contribuciones son bienvenidas! Si deseas aportar al proyecto:

1. Realiza un *Fork* del repositorio.
2. Crea una rama para tu característica o corrección: `git checkout -b feature/NuevaCaracteristica`
3. Haz commit de tus cambios: `git commit -m 'feat: añade nueva característica de análisis'`
4. Sube la rama: `git push origin feature/NuevaCaracteristica`
5. Abre un **Pull Request** detallando los cambios.

---

## 📝 Licencia

Este proyecto está bajo la Licencia **MIT**. Consulta el archivo `LICENSE` para más detalles.

---
*Desarrollado y mantenido por [eudyyuniorramires](https://github.com/eudyyuniorramires).*
