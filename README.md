# Maksym Leonovych — Full-Stack .NET Portfolio

Built from scratch:
- **frontend/** — Angular 18 (standalone components), "engineering blueprint" themed portfolio page.
- **backend/PortfolioApi/** — ASP.NET Core 8 Web API with a single `POST /api/contact` endpoint that validates and logs contact form submissions.

## Run the backend

Requires the .NET 8 SDK.

```bash
cd backend/PortfolioApi
dotnet restore
dotnet run
```

The API listens on **http://localhost:5050** (see `Properties/launchSettings.json`).
Swagger UI is available at `http://localhost:5050/swagger` in development.

## Run the frontend

Requires Node.js 18+.

```bash
cd frontend
npm install
npm start
```

Open **http://localhost:4200**. The contact form posts to `http://localhost:5050/api/contact` by default (see `src/environments/environment.ts`).

## CORS

The backend only accepts requests from the origin set in `FRONTEND_ORIGIN`
(`appsettings.json`, default `http://localhost:4200`). Set it via
`appsettings.Production.json` or an environment variable when deploying.

## Deploying

- **Frontend**: `npm run build`, then serve the `dist/portfolio-frontend/browser` folder as static files. Set `src/environments/environment.prod.ts` → `apiUrl` to your public API URL before building.
- **Backend**: `docker build -t portfolio-api backend/PortfolioApi` (Dockerfile included) or `dotnet publish`. Set `FRONTEND_ORIGIN` to your public frontend URL.

## Customize

- Profile, notes, tech stack, and projects: `frontend/src/app/app.component.ts`
- Visual tokens (colors, fonts): `frontend/src/styles.scss` and `frontend/src/app/app.component.scss`
- Contact form handling (currently logs only): `backend/PortfolioApi/Controllers/ContactController.cs`
