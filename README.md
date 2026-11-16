# Forsikring – AI-assisteret udviklingsdemo

En dansk demo med en Vue 3-single-page-app, Tailwind CSS og et ASP.NET Core API. Forsikringskortene hentes fra API'et. Logo-mark, illustrationer og produktindhold er originale demo-elementer.

## Krav

- Node.js 20.19 eller nyere
- .NET 10 SDK

## Start appen

```powershell
cd frontend
npm install
npm run dev
```

`npm run dev` starter API'et på `http://localhost:5037`, venter på API'ets health-check og starter derefter Vite. Åbn URL'en, som Vite viser i terminalen. Vite videresender `/api`-kald til API'et.

Produktdata findes på `/api/products`, og `/api/health` viser API-status.

## Build frontend

```powershell
cd frontend
npm run build
```
