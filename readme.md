# Anvendelse af Git og GitHub under udvikling af kodebasen

Dette dokument beskriver en universel og ensartet arbejdsproces for gruppemedlemmer, der arbejder med **Git** og **GitHub**.  
Vi bruger **Pull Requests** som vores primære samarbejdsværktøj, hvilket sikrer at kode altid bliver gennemset af mindst ét andet gruppemedlem, inden det lander i `main`.

---

## Grundprincipper

Tænk på din `main`-branch som den færdige, stabile version af projektet. Den repræsenterer altid kode der virker og er godkendt af gruppen.

- Du redigerer **aldrig direkte i `main`**
- Al kode går igennem en **Pull Request** på GitHub
- Mindst ét gruppemedlem skal godkende din kode, inden den merges
- `main` eksisterer kun lokalt som reference — GitHub klarer selve mergingen

---

## Rytmikken for brugen af Git

Følg denne rækkefølge hver gang du arbejder på en ny feature eller opgave.

---

## Trin 1–3: Forberedelse og arbejde

Start altid med at opdatere din lokale `main`, så du arbejder på den nyeste version af projektet.

### 1. Gå til main og opdater den

```bash
git switch main
git pull
2. Opret en ny feature-branch
git switch -c feature/add-contact-page
Her arbejder du

Opret og rediger filer...

3. Stage og commit dit arbejde
git add .
git commit -m "Opretter kontaktside"

✅ Tip: Commit ofte med små, beskrivende beskeder fremfor én stor commit til sidst.
Det gør det nemmere for andre at gennemse din kode.

Trin 4: Push din branch til GitHub

Når du er færdig med din feature, pusher du din branch til GitHub — ikke main.
Det er her Pull Request-processen begynder.

git push origin feature/add-contact-page
Trin 5–6: Opret en Pull Request på GitHub

Gå ind på GitHub og opret en Pull Request fra din feature-branch mod main.

Klik på "Compare & pull request" (GitHub foreslår det automatisk efter et push)
Skriv en kort beskrivelse af hvad du har lavet og hvorfor
Tildel en eller flere fra gruppen som reviewers under "Reviewers" i højre side
Klik "Create pull request"
Trin 7: Code Review

Reviewerens opgave er at gennemse koden og give feedback.
Det er en vigtig del af samarbejdet — ikke en kontrol, men en mulighed for at lære af hinanden og fange fejl tidligt.

Som reviewer har du tre muligheder:

Approve — koden er god, den kan merges
Request changes — noget skal rettes eller forklares inden merge
Comment — en neutral observation der ikke blokerer for merge
Trin 8: Merge og oprydning

Når mindst ét gruppemedlem har godkendt din Pull Request, kan den merges direkte på GitHub.

Klik "Merge pull request" på GitHub
Klik "Confirm merge"
GitHub tilbyder at slette branchen — det kan du roligt gøre

Derefter opdaterer alle lokalt:

Opdater din lokale main
git switch main
git pull
Slet den lokale feature-branch
git branch -d feature/add-contact-page
Konventioner for commit-beskeder

Gode commit-beskeder gør historikken læsbar for alle i gruppen.
Brug et verbum i nutid og vær konkret.

✅ Gode eksempler:

Opretter login-side
Tilføjer validering til Person-model
Fikser bug i TodoController

❌ Dårlige eksempler:

Lavet noget med login
Fix
Karstens rod
Simon har skidt i bukserne
Hvis det hele går galt

Hvis du sidder med lokale ændringer du ikke har brug for, og bare vil have den nyeste version fra GitHub:

git fetch origin
git reset --hard origin/main

Dette vil:

Hente de nyeste data fra GitHub (fetch)
Slette alle dine lokale commits og ændringer, så main peger præcis på det samme som origin/main

⚠️ Bemærk: Denne kommando er irreversibel.
Brug den kun hvis du er sikker på at du ikke har brug for dine lokale ændringer.

.gitignore

En .gitignore-fil fortæller Git hvilke filer der skal ignoreres.
Den skal oprettes og committes som det allerførste i projektet, så ingen i gruppen nogensinde pusher unødvendige filer.

Standard konfiguration for et C#-projekt:

bin/
obj/
.idea/
*.user

Hvis gruppemedlemmer bruger forskellige IDE'er kan hver push indeholde uønskede IDE-filer.
Tilføj dem til .gitignore så snart I opdager dem.

Git commands i terminalen

Command	Beskrivelse
git clone <url>	Kopierer et remote repository ned på din lokale maskine. Mappen oprettes i den aktive mappe.
git pull	Henter og fletter ændringer fra GitHub ned i din lokale version. Kør altid denne når du starter.
git status	Giver et overblik over filernes tilstand: untracked (kun lokalt), staged (klar til commit).
git add .	Tilføjer alle ændrede filer til staging area, klar til at blive committet.
git commit -m "besked"	Gemmer dine staged ændringer som et commit med en beskrivende besked.
git push origin <branch>	Uploader din feature-branch til GitHub, så du kan oprette en Pull Request.
git switch main	Skifter til din main-branch.
git switch -c <navn>	Opretter en ny branch og skifter til den. -c står for create.
git branch	Viser alle lokale branches og fremhæver den, du er på.
git branch -d <navn>	Sletter en lokal branch efter den er merget.
git fetch origin	Henter nyeste data fra GitHub uden at merge.
git reset --hard origin/main	Nulstiller din lokale main til at matche GitHub. Sletter lokale ændringer.
git remote -v	Viser URL'en på dit GitHub repository.
git log --oneline	Viser en kompakt liste over seneste commits.

Ressourcer
https://docs.github.com/en/get-started/start-your-journey/hello-world
https://learngitbranching.js.org/
https://docs.github.com/en/pull-requests/collaborating-with-pull-requests
