# personapi-dotnet

## 🚀 Despliegue 

### Opción 1: Ver en línea
- **Repositorio**: https://github.com/SergioLopezAyala/personapi-dotnet
- **Release**: https://github.com/SergioLopezAyala/personapi-dotnet/releases/tag/v1.0.0

### Opción 2: Ejecutar localmente (Docker)

```bash
# 1. Clonar el repositorio
git clone https://github.com/SergioLopezAyala/personapi-dotnet.git
cd personapi-dotnet

# 2. Iniciar los servicios
cd .devcontainer
docker-compose up -d

# 3. Esperar ~30 segundos a que SQL Server inicie

# 4. La app estará disponible en:
#   - HTTP:      http://localhost:5000
#   - Swagger:   http://localhost:5000/swagger

# Para detener: docker-compose down
```

**Credenciales SQL**: `sa` / `Admin123!`

---

## Descripción

Web API y aplicación MVC en **ASP.NET Core 7** con patrón **MVC + DAO (Repository Pattern)** que expone:

- **API REST** (Swagger 3) para CRUD de `persona`, `profesion`, `estudios`, `telefono`.
- **Vistas Razor + Bootstrap 5** para CRUD desde navegador.
- Persistencia con **Entity Framework Core 7** sobre **SQL Server 2022**.

El entorno corre en Dev Containers con dos servicios: `app` (.NET) y `db` (SQL Server). Es un laboratorio: **HTTP only, sin autenticación**.

---

## Requisitos previos

- Docker Desktop
- VS Code o Cursor con extensión Dev Containers
- (Opcional, para uso fuera del Dev Container) .NET SDK 7.0.410

---

## Configuración del ambiente

### Opción A: Docker Compose (Recomendado)

```bash
cd .devcontainer
docker-compose up -d
```

La app escucha en `http://localhost:5000` y la BD en `localhost:1433`.

### Opción B: VS Code Dev Container

1. Abrir el repositorio en VS Code.
2. Ejecutar **Reopen in Container**.
3. Esperar a que se construyan los servicios.

Variables clave (definidas en `.devcontainer/docker-compose.yml`):

| Variable | Valor |
|---|---|
| Host SQL desde `app` | `db:1433` |
| Usuario SQL          | `sa` / `Admin123!` |
| URL API              | `http://localhost:5000` |

---

## Configuración de la base de datos

La base se llama **`persona_db`** y se crea/actualiza automáticamente al iniciar la app. El arranque aplica migraciones si existen, o crea el esquema desde el modelo y luego **siembra datos con EF Core**.

Los scripts en [`scripts/schema.sql`](scripts/schema.sql) y [`scripts/seed.sql`](scripts/seed.sql) quedan como referencia o para uso manual. Para entornos fuera del Dev Container, existen los scripts equivalentes en [`personapi-dotnet/Database/ddl.sql`](personapi-dotnet/Database/ddl.sql) y [`personapi-dotnet/Database/dml.sql`](personapi-dotnet/Database/dml.sql).

### Ejecutar los scripts manualmente

Desde el contenedor `app`:

```bash
sqlcmd -S db -U sa -P 'Admin123!' -i personapi-dotnet/Database/ddl.sql
sqlcmd -S db -U sa -P 'Admin123!' -i personapi-dotnet/Database/dml.sql
```

### Modelo

| Tabla | Columnas | PK |
|---|---|---|
| `profesion` | `id` (identity), `nom`, `des` | `id` |
| `persona`   | `cc` (PK), `nombre`, `apellido`, `genero` ('M'/'F'), `edad` | `cc` |
| `estudios`  | `id_prof` (FK→profesion), `cc_per` (FK→persona), `fecha`, `univer` | (`id_prof`, `cc_per`) |
| `telefono`  | `num` (PK), `oper`, `duenio` (FK→persona, NOT NULL) | `num` |

### Relaciones (diagrama entidad-relación)

```
┌─────────────┐       ┌─────────────┐
│  profesion  │       │   persona   │
│─────────────│       │─────────────│
│ PK id (AI)  │       │ PK cc       │
│ nom         │       │ nombre      │
│ des         │       │ apellido    │
└──────┬──────┘       │ genero      │
       │              │ edad        │
       │ 1:N          └──────┬──────┘
       │                     │
       ▼                     │
┌─────────────┐       ┌──────┴──────┐
│  estudios   │       │  telefono   │
│─────────────│       │─────────────│
│ PK id_prof  │◄──FK  │ PK num      │
│ PK cc_per   │◄──FK  │ oper        │
│ fecha       │       │ FK duenio ──┼──► NOT NULL
│ univer      │       └─────────────┘
└─────────────┘
```

- `profesion` **1 → N** `estudios` (cada profesión puede tener muchos estudios)
- `persona` **1 → N** `estudios` (cada persona puede estudiar muchas profesiones)
- `persona` **1 → N** `telefono` (cada persona tiene muchos teléfonos, FK obligatoria)

### Auto-seeding

Al iniciar la app, [`PersonaDbSeeder.cs`](personapi-dotnet/Models/Entities/PersonaDbSeeder.cs) sembrar automáticamente:

- 5 profesiones (Ingeniero, Médico, Abogado, Arquitecto, Contador)
- 5 personas (con cédulas 1001-1005)
- 5 estudios (relacionando personas con profesiones)
- 5 teléfonos (asociados a cada persona)

### Cadena de conexión

`personapi-dotnet/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=db;Database=persona_db;User Id=sa;Password=Admin123!;TrustServerCertificate=True"
}
```

### Migraciones

Si deseas generar migraciones, crea la inicial desde la raíz del repo:

```bash
dotnet ef migrations add InitialCreate -p personapi-dotnet
dotnet ef database update -p personapi-dotnet
```

Si no tienes `dotnet-ef` instalado:

```bash
dotnet tool install --global dotnet-ef
```

En el arranque, la app aplica automáticamente las migraciones pendientes.

---

## Compilación

Desde la raíz del repo (dentro del Dev Container):

```bash
dotnet restore personapi-dotnet.sln
dotnet build   personapi-dotnet.sln
```

---

## Despliegue / Ejecución

```bash
cd personapi-dotnet
dotnet run
```

La aplicación escucha en `http://0.0.0.0:5000`.

| Ruta | Contenido |
|---|---|
| `http://localhost:5000/`         | Página de inicio (vistas MVC) |
| `http://localhost:5000/Persona`  | CRUD UI Persona |
| `http://localhost:5000/Profesion`| CRUD UI Profesión |
| `http://localhost:5000/Estudios` | CRUD UI Estudios |
| `http://localhost:5000/Telefono` | CRUD UI Teléfono |
| `http://localhost:5000/swagger`  | Swagger UI (solo en Development) |

---

## Endpoints disponibles

> Todos los endpoints REST devuelven JSON. Códigos de estado: `200`, `201`, `204`, `400`, `404`, `409`.

### `/api/persona`

| Método | Ruta | Descripción |
|---|---|---|
| GET    | `/api/persona`           | Listar todas |
| GET    | `/api/persona/{cc}`      | Obtener por cédula |
| POST   | `/api/persona`           | Crear |
| PUT    | `/api/persona/{cc}`      | Actualizar |
| DELETE | `/api/persona/{cc}`      | Eliminar |

### `/api/profesion`

| Método | Ruta | Descripción |
|---|---|---|
| GET    | `/api/profesion`         | Listar todas |
| GET    | `/api/profesion/{id}`    | Obtener por id |
| POST   | `/api/profesion`         | Crear |
| PUT    | `/api/profesion/{id}`    | Actualizar |
| DELETE | `/api/profesion/{id}`    | Eliminar |

### `/api/estudios` (clave compuesta `idProf`/`ccPer`)

| Método | Ruta | Descripción |
|---|---|---|
| GET    | `/api/estudios`                      | Listar todos |
| GET    | `/api/estudios/{idProf}/{ccPer}`     | Obtener por clave compuesta |
| POST   | `/api/estudios`                      | Crear |
| PUT    | `/api/estudios/{idProf}/{ccPer}`     | Actualizar |
| DELETE | `/api/estudios/{idProf}/{ccPer}`     | Eliminar |

### `/api/telefono`

| Método | Ruta | Descripción |
|---|---|---|
| GET    | `/api/telefono`          | Listar todos |
| GET    | `/api/telefono/{num}`    | Obtener por número |
| POST   | `/api/telefono`          | Crear |
| PUT    | `/api/telefono/{num}`    | Actualizar |
| DELETE | `/api/telefono/{num}`    | Eliminar |

---

## Estructura del proyecto

```
personapi-dotnet/
├── personapi-dotnet/
│   ├── Controllers/
│   │   ├── HomeController.cs
│   │   ├── PersonaController.cs              ← MVC (vistas)
│   │   ├── ProfesionController.cs            ← MVC (vistas)
│   │   ├── EstudiosController.cs             ← MVC (vistas)
│   │   ├── TelefonoController.cs             ← MVC (vistas)
│   │   └── Api/
│   │       ├── PersonaApiController.cs       ← REST [ApiController]
│   │       ├── ProfesionApiController.cs     ← REST [ApiController]
│   │       ├── EstudiosApiController.cs      ← REST [ApiController]
│   │       └── TelefonoApiController.cs      ← REST [ApiController]
│   ├── Database/
│   │   ├── ddl.sql                           ← DDL idempotente
│   │   └── dml.sql                           ← DML de prueba
│   ├── Interfaces/
│   │   ├── IGenericRepository.cs
│   │   ├── IPersonaRepository.cs
│   │   ├── IProfesionRepository.cs
│   │   ├── IEstudiosRepository.cs
│   │   └── ITelefonoRepository.cs
│   ├── Models/
│   │   └── Entities/
│   │       ├── Persona.cs
│   │       ├── Profesion.cs
│   │       ├── Estudios.cs                   ← PK compuesta
│   │       ├── Telefono.cs
│   │       ├── PersonaDbContext.cs
│   │       └── PersonaDbSeeder.cs            ← Auto-seed de datos
│   ├── Repositories/
│   │   ├── PersonaRepository.cs
│   │   ├── ProfesionRepository.cs
│   │   ├── EstudiosRepository.cs
│   │   └── TelefonoRepository.cs
│   ├── Views/
│   │   ├── _ViewImports.cshtml
│   │   ├── _ViewStart.cshtml
│   │   ├── Shared/
│   │   │   ├── _Layout.cshtml
│   │   │   └── _ValidationScriptsPartial.cshtml
│   │   ├── Home/Index.cshtml
│   │   ├── Persona/   { Index, Create, Edit, Details, Delete }.cshtml
│   │   ├── Profesion/ { Index, Create, Edit, Details, Delete }.cshtml
│   │   ├── Estudios/  { Index, Create, Edit, Details, Delete }.cshtml
│   │   └── Telefono/  { Index, Create, Edit, Details, Delete }.cshtml
│   ├── appsettings.json
│   ├── Program.cs
│   └── personapi-dotnet.csproj
├── scripts/
│   ├── schema.sql                            ← ejecutado por init-db.sh
│   └── seed.sql                              ← ejecutado por init-db.sh
├── .devcontainer/
│   └── docker-compose.yml
├── Directory.Packages.props                  ← versiones NuGet centralizadas
├── global.json                               ← SDK 7.0.410
└── personapi-dotnet.sln
```

> Nota sobre los controladores: la API REST usa el sufijo `…ApiController` para evitar colisión con los controladores MVC del mismo nombre. Las rutas REST son explícitas (`[Route("api/persona")]`, etc.) y no dependen de `[controller]`.

---

## Notas

- HTTP solamente (sin HTTPS) — diseñado para laboratorio.
- Sin autenticación.
- `Directory.Packages.props` es la única fuente de verdad para versiones NuGet (`ManagePackageVersionsCentrally=true`).
- Eliminar los volúmenes Docker `nuget-packages` y `sql-data` borra los paquetes y la base de datos sembrada.
