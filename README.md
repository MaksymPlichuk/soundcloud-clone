# SoundCloud Clone

Навчальний проєкт — клон SoundCloud. Backend на ASP.NET Web API (3-layer architecture), frontend на TypeScript (React).

## Технології

**Backend**
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Mapperly - (маппінг DTO - Entity)
- ASP.NET Identity + JWT (Authentication)
- Swagger

**Frontend**
- TypeScript
- React
- React Router
- RTK Query
- Zod
- react-hook-form

**архітектура**
- Backend: класична 3-layer (Presentation > BLL > DAL)
- Git-flow: `main` + `develop` + `feature/*`  — деталі в [WORKFLOW.md](./WORKFLOW.md)

## Структура проєкту

```
/Backend
  /API              > контролери, Program.cs, конфігурація
  /BLL              > сервіси, бізнес-логіка, DTO
  /DAL              > DbContext, Entities, Fluent API конфігурація, міграції
/Frontend
  /src
    /components
    /pages
    /api            > RTK Query
    /types
```

## як запустити

### Backend

```bash
cd back/Soundcloud_Clone.API
dotnet restore
dotnet ef database update    # застосувати міграції
dotnet run
```

API буде доступне на `http://localhost:5087` (або порт у `launchSettings.json`)
Swagger - `http://localhost:5087/swagger`.

Змінити бд `Soundcloud_Clone.API/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=SoundCloudClone;..."
  }
}
```

### Frontend

```bash
cd Frontend
npm i
npm run dev
```

Фронт  буде доступний на `http://localhost:5173`

Змінити `.env`:

```
VITE_API_URL=http://localhost:5087
```

## Основні сутності

- **User** (Artist / Listener)
- **Songs** (треки)
- **Albums** (плейлісти)
- **Comment**
