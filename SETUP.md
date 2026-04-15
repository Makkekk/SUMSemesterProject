# Kom i gang med projektet

## 1. Forudsætninger

Installer følgende inden du starter:

| Værktøj | Version | Link |
|---|---|---|
| .NET SDK | 10.0.x | https://dotnet.microsoft.com/download |
| JetBrains Rider | nyeste | https://www.jetbrains.com/rider/ |
| PostgreSQL / Supabase | nyeste | https://supabase.com/ |
| Git | nyeste | https://git-scm.com/ |

Tjek at .NET er installeret korrekt:
```bash
dotnet --version
# Skal vise 10.0.x
```

---

## 2. Klon projektet

```bash
git clone https://github.com/Makkekk/SUMSemesterProjekt.git
cd SUMSemesterProjekt
```

---

## 3. Konfigurér databasen

Projektet bruger Supabase/PostgreSQL via connection string i `LajmiAPI/appsettings.json`.

1. Åbn `LajmiAPI/appsettings.json`
2. Udfyld `ConnectionStrings:DefaultConnection` med den rigtige Supabase/PostgreSQL connection string
3. Læg din lokale version i `LajmiAPI/appsettings.Development.json`, hvis du vil holde den ude af Git

Eksempel:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=...;Database=postgres;Username=postgres;Password=...;SSL Mode=Require;Trust Server Certificate=true"
}
```

---

## 4. Installér pakker

Kør følgende kommando fra rodmappen. Den henter automatisk alle NuGet-pakker:

```bash
dotnet restore
```

Pakker der bruges i projektet:
- `Npgsql.EntityFrameworkCore.PostgreSQL` — forbindelse til PostgreSQL/Supabase
- `Microsoft.EntityFrameworkCore` — ORM til databaseadgang
- `Microsoft.AspNetCore.OpenApi` — API-dokumentation

---

## 5. Åbn i Rider

1. Åbn Rider
2. Vælg **Open**
3. Naviger til mappen og vælg filen `SUMSemesterProject.sln`
4. Rider genkender automatisk alle projekter i solutionen

---

## 6. Start projektet

Kør API'et fra terminalen:
```bash
dotnet run --project LajmiAPI
```

Eller brug **Run**-knappen i Rider med `LajmiAPI` valgt som startup-projekt.

---

## 7. Projektstruktur

```
SUMSemesterProjekt/
├── LajmiAPI/        # Web API (startpunkt)
├── LajmiWebApp/     # Web-applikation
├── BusninessLogic/  # Forretningslogik
├── DataAcces/       # Databaseadgang og databasekontekst
├── DTO/             # Data Transfer Objects
└── Models/          # Datamodeller
```

---

## 8. Problemer?

**`dotnet restore` fejler** — Tjek at .NET 10 SDK er installeret (`dotnet --version`)

**Databaseforbindelsen fejler** — Tjek at connection string i `LajmiAPI/appsettings.json` er korrekt, og at password ikke er tomt

**Rider finder ikke projekter** — Åbn `.sln`-filen i stedet for mappen
