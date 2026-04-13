# Kom i gang med projektet

## 1. Forudsætninger

Installer følgende inden du starter:

| Værktøj | Version | Link |
|---|---|---|
| .NET SDK | 10.0.x | https://dotnet.microsoft.com/download |
| JetBrains Rider | nyeste | https://www.jetbrains.com/rider/ |
| MongoDB | nyeste | https://www.mongodb.com/try/download/community |
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

## 3. Installér pakker

Kør følgende kommando fra rodmappen. Den henter automatisk alle NuGet-pakker:

```bash
dotnet restore
```

Pakker der bruges i projektet:
- `MongoDB.Driver` — forbindelse til MongoDB
- `Microsoft.EntityFrameworkCore` — ORM til databaseadgang
- `Microsoft.AspNetCore.OpenApi` — API-dokumentation

---

## 4. Åbn i Rider

1. Åbn Rider
2. Vælg **Open**
3. Naviger til mappen og vælg filen `SUMSemesterProject.sln`
4. Rider genkender automatisk alle projekter i solutionen

---

## 5. Start projektet

Kør API'et fra terminalen:
```bash
dotnet run --project LajmiAPI
```

Eller brug **Run**-knappen i Rider med `LajmiAPI` valgt som startup-projekt.

---

## 6. Projektstruktur

```
SUMSemesterProjekt/
├── LajmiAPI/        # Web API (startpunkt)
├── LajmiWebApp/     # Web-applikation
├── BusninessLogic/  # Forretningslogik
├── DataAcces/       # Databaseadgang og MongoDB-kontekst
├── DTO/             # Data Transfer Objects
└── Models/          # Datamodeller
```

---

## 7. Problemer?

**`dotnet restore` fejler** — Tjek at .NET 10 SDK er installeret (`dotnet --version`)

**MongoDB forbindelsesfejl** — Tjek at MongoDB kører lokalt (`mongod` i terminalen)

**Rider finder ikke projekter** — Åbn `.sln`-filen i stedet for mappen
