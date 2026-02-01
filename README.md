
# Credit Card Management Application

## 📌 Description

Cette application web permet de **gérer des cartes bancaires**. Chaque utilisateur peut :

- Créer un compte et se connecter (authentification JWT simplifiée)
- Ajouter, modifier et supprimer ses cartes
- Visualiser uniquement ses propres cartes


---

## 💻 Stack technique

**Backend :**

- ASP.NET Core Web API (.NET 8)  
- Entity Framework Core  
- SQL Server  
- JWT pour l’authentification

**Frontend :**

- Vue.js 3 + Composition API  
- Axios pour les requêtes HTTP  

---

## ⚙️ Backend - Installation et lancement

1. **Lancer les migrations EF Core et créer la base :**

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
````

2. **Démarrer le backend :**

```bash
dotnet run
```

Le backend sera accessible sur : `https://localhost:5213`

---

## ⚙️ Frontend - Installation et lancement

1. Ouvrir le dossier `frontend` :

```bash
cd frontend
```

2. Installer les dépendances :

```bash
npm install
```

3. Lancer le frontend :

```bash
npm run dev
```

Le frontend sera accessible sur : `http://localhost:5173`

---

## 🛠 Fonctionnalités

### 🔐 Authentification

* Inscription (Register)
* Connexion (Login)
* Déconnexion (Logout)
* JWT simplifié pour sécuriser les endpoints

### 💳 Gestion des cartes (CRUD)

* Ajouter une carte
* Supprimer une carte
* Liste des cartes de l’utilisateur connecté uniquement
* Masquage des numéros : `**** **** **** 1234`

### 🎨 Interface

* Interface simple et responsive
* Formulaire d’ajout de carte
* Liste des cartes avec bouton de suppression
* Bouton de déconnexion

### 🔒 Sécurité

* Endpoints protégés via `[Authorize]`
* Chaque carte est liée à un `UserId`
* Le token JWT contient l’ID de l’utilisateur

---

## 📂 Structure du projet

CreditCardProject/
├── CreditCardApi/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   └── CreditCardsController.cs
│   ├── Data/
│   │   └── AppDbContext.cs
│   ├── DTOs/
│   │   ├── LoginRequest.cs
│   │   └── RegisterRequest.cs
│   ├── Models/
│   │   ├── CreditCard.cs
│   │   └── User.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── appsettings.json
│   ├── Program.cs
│   └── CreditCardApi.csproj
│
├── frontend/
│   ├── public/
│   │   └── index.html
│   ├── src/
│   │   ├── App.vue
│   │   ├── main.js
│   │   ├── router/
│   │   │   └── index.js
│   │   ├── stores/
│   │   │   └── auth.js
│   │   ├── services/
│   │   │   └── api.js
│   │   └── views/
│   │       ├── Login.vue
│   │       └── Dashboard.vue
│   └── package.json
│  
└── .gitignore
