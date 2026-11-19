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

## Backend-struktur

- `backend/Insurance.Api/Program.cs`: opstart, controllerregistrering og OpenAPI i udviklingsmiljoet.
- `backend/Insurance.Api/Controllers/`: HTTP-endpoints for `/api/health` og `/api/products`.
- `backend/Insurance.Api/Data/`: produktkataloget, som API'et returnerer.
- `backend/Insurance.Api/Calculations/`: praemieberegning uden afhaengighed af HTTP.
- `backend/Insurance.Api.Tests/`: xUnit-tests med Arrange-Act-Assert og navne som `CalculateMonthlyPremium_Scenario_ExpectedResult`.

## Test backend

Koer fra repository-roden:

```powershell
dotnet test backend/Insurance.Api.Tests/Insurance.Api.Tests.csproj
```

Praemieberegningen bruger `decimal`, tillaegger 20 procent for kunder under 25 og giver derefter 10 procent rabat ved mindst fem skadefri aar. Maanedspraemien afrundes til to decimaler med midpoint rounding away from zero. Negative praemier, aldre uden for 0-120 og skadefri aar uden for 0-alder afvises.

## Versioner og release notes

`v0.1.0` markerer den foerste version paa `main`. Arbejd paa en feature-branch, og lav et commit pr. fil med en beskrivende commit-besked. Opret derefter en pull request mod `main`, og beskriv aendringer og testresultater.

Naar en pull request merges til `main`, opretter GitHub Actions automatisk det naeste patch-tag og en GitHub Release. Den foerste merge efter `v0.1.0` giver `v0.1.1`. Tagget peger paa PR'ens merge-commit, ikke paa en senere udgave af `main`.

Release notes genereres af GitHub ud fra aendringerne siden det foregaaende versionstag og indeholder links til inkluderede pull requests. Brug derfor meningsfulde PR-titler. Workflowet kraever et eksisterende `vMAJOR.MINOR.PATCH`-tag og `contents: write` for workflowets `GITHUB_TOKEN`.

Workflowet publicerer ikke ved aabning eller opdatering af en PR, ved lukning uden merge eller ved direkte pushes til `main`. Automatisk versionsloeft er kun patch; major- og minor-versioner planlaegges separat.
