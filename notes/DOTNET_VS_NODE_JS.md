# Dotnet Commands Mapped to NPM

- `dotnet restore` = fetch/install NuGet packages (like `npm install` or `npm ci`).
  - Reads package references in the project file (.csproj) and downloads packages into the NuGet cache and produces a project.assets.json in obj/.
  - Use when you only need dependencies downloaded (CI step, offline preparation, or to avoid redundant network work).
  - Command:

```bash
dotnet restore
```

- `dotnet build` = compile the project (like `npm run build` or running `tsc`).

  - Restores packages if needed (modern SDKs run restore implicitly), then compiles sources and produces outputs under bin/ and intermediate files in obj/.
  - Use when you want to produce the compiled binaries/artifacts.
  - Command:

```bash
dotnet build
```

Other helpful notes:

- `dotnet run` = restore (if needed) → build → run the app (similar to `npm start` when that runs a build and then starts a server).
- In CI you often run `dotnet restore` as a separate step (parallelize/cache) and then `dotnet build` to compile — same pattern as running `npm ci` then `npm run build`.
- `dotnet restore` is quick if nothing changed; `dotnet build` actually uses the downloaded packages to compile.
- If you need reproducible installs in CI, prefer an explicit `dotnet restore` step (so caching and failure points are clear).
