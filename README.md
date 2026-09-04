# MonProjetReservation

Application web de gestion des réservations avec :

* **Frontend** : Angular
* **Backend** : ASP.NET Core Web API (C#)
* **Base de données** : SQL Server
* **Authentification** : JWT

## Structure du projet

```text
MonProjetReservation/
├── backend/
│   └── webapisecond/
│       └── webapisecond/
│
├── frontend/
│   └── frontReservation/
│
├── tests/
│   └── api-tests.http
│
├── .gitignore
└── README.md
```

## Prérequis

Avant de lancer le projet, installer :

* .NET 8 SDK
* Node.js
* Angular CLI
* SQL Server / SQL Server LocalDB
* Git

Vérifier les installations :

```bash
dotnet --version
node --version
npm --version
ng version
```

---

# 1. Lancer le Backend

Ouvrir un terminal dans le dossier du backend :

```powershell
cd backend/webapisecond/webapisecond
```

Restaurer les dépendances :

```powershell
dotnet restore
```

Si nécessaire, appliquer les migrations de la base de données :

```powershell
dotnet ef database update
```

Puis lancer l'API :

```powershell
dotnet run
```

Le backend sera disponible à l'adresse indiquée dans le terminal, par exemple :

```text
https://localhost:7000
```

L'API peut également être testée avec Swagger :

```text
https://localhost:7000/swagger
```

---

# 2. Lancer le Frontend Angular

Ouvrir un **deuxième terminal** :

```powershell
cd frontend/frontReservation
```

Installer les dépendances :

```powershell
npm install
```

Puis lancer Angular :

```powershell
ng serve
```

Le frontend sera accessible à :

```text
http://localhost:4200
```

---

# 3. Lancer les deux serveurs

Il faut avoir **deux terminaux ouverts**.

### Terminal 1 — Backend

```powershell
cd backend/webapisecond/webapisecond
dotnet run
```

### Terminal 2 — Frontend

```powershell
cd frontend/frontReservation
ng serve
```

Ensuite ouvrir :

```text
http://localhost:4200
```

Le frontend Angular communique avec l'API ASP.NET Core.

---

# 4. Configuration de l'API

L'URL de l'API utilisée par Angular est définie dans le fichier :

```text
frontend/frontReservation/src/environments/environment.ts
```

Exemple :

```typescript
export const environment = {
  apiUrl: 'https://localhost:7000/api'
};
```

Si le backend utilise un autre port, modifier cette URL en conséquence.

---

# 5. Authentification

L'application utilise **JWT** pour sécuriser les endpoints.

Pour accéder aux endpoints protégés, l'utilisateur doit d'abord se connecter afin d'obtenir un token JWT.

Exemple :

```text
POST /api/Auth/login
```

Le token est ensuite envoyé dans les requêtes HTTP avec :

```text
Authorization: Bearer <token>
```

---

# 6. Tests de l'API

Les principaux scénarios peuvent être testés avec le fichier :

```text
tests/api-tests.http
```

Les tests couvrent notamment :

* Connexion utilisateur
* Création d'une réservation
* Tentative de réservation sur un créneau déjà occupé → **409 Conflict**
* Accès à une ressource protégée sans token → **401 Unauthorized**

---

# 7. Développement

Pour arrêter un serveur :

```text
Ctrl + C
```

Pour installer les dépendances Angular après un nouveau clone :

```powershell
cd frontend/frontReservation
npm install
```

Pour restaurer les dépendances .NET :

```powershell
cd backend/webapisecond/webapisecond
dotnet restore
```
