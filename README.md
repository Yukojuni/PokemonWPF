Pokemon en C# avec WPF
Bienvenue dans le projet Pokemon-Like, une application développée en C# avec WPF. Ce projet consiste à créer une simulation de combats au tour par tour inspirée de l'univers Pokémon, intégrant une gestion de base de données et une interface utilisateur intuitive.

🎯 Objectifs du Projet

  1. POO (Programmation Orientée Objet) : Organisation du code en classes clairement séparées.
  2. Respect du Modèle MVVM : Séparation des responsabilités entre la Vue, le Modèle et le ViewModel.
  3. Base de Données SQL Server Express :
      Gestion des utilisateurs (connexion, hash des mots de passe).
      Stockage des monstres et des sorts dans une structure normalisée.
  4. Combat au Tour par Tour :
    Utilisation des sorts pour infliger des dégâts.
    Barre de santé visible pour chaque monstre.
    Génération dynamique d'ennemis avec des statistiques améliorées.

💻 Fonctionnalités

  1. Écran de Connexion (Login)
    Permet aux utilisateurs de se connecter en renseignant un nom d'utilisateur et un mot de passe.
    Les mots de passe sont hashés avant d'être stockés en base (sécurisation des données).
  2. Écran Paramètres de la Base de Données
    Configuration de la connexion à la base de données via un champ dans les paramètres.
    Initialisation automatique des données par défaut :
      Monstres, sorts et utilisateurs.
  3. Gestion des Monstres
  Liste des Monstres : Affichage de tous les monstres disponibles.
  Détails d'un Monstre : Visualisation des informations comme le nom, les HP et les sorts associés.
  Choix du Monstre Joueur : Sélectionnez le monstre avec lequel vous souhaitez combattre.
  4. Gestion des Sorts
    Liste des Sorts : Tous les sorts disponibles dans le jeu.
    Détails d'un Sort : Nom, dégâts et description du sort.
    Tri des Sorts : Par monstre qui les possède.
  5. Combat au Tour par Tour
    Système de Combat :
      Utilisation des sorts pour infliger des dégâts à l'ennemi.
      Barre de santé visible pour le joueur et l'ennemi.
      Génération d'ennemis avec des statistiques légèrement améliorées à chaque combat (+10% HP, +5% dégâts).
    Score : Incrémentation du score pour chaque monstre vaincu.
    Rejouer : Un bouton permet de relancer un combat avec un nouvel ennemi.

⚙️ Installation

Prérequis
Visual Studio (avec support pour WPF et .NET 6 ou supérieur).
SQL Server Express.
Git.
Étapes
Cloner le Dépôt
'[git clone https://github.com/...](https://github.com/Yukojuni/PokemonWPF.git)'
cd PokemonWPF

Exécuter l'Application
Dans l'application, renseignez la chaîne de connexion à votre base de données via l'onglet Settings.

📦 Packages Utilisés

Entity Framework Core : Gestion de la base de données (lecture, écriture).
CommunityToolkit.Mvvm : Implémentation simplifiée du modèle MVVM.
Microsoft.EntityFrameworkCore :
Microsoft.EntityFrameworkCore.Design : 
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools : 

🗂️ Architecture

Modèle MVVM
Model : Classes représentant les données (Monsters, Spells, etc.).
ViewModel : Gestion des données et des interactions pour chaque vue (ex. : GameVM, FightVM).
View : Fichiers XAML représentant l'interface utilisateur (ex. : GameView.xaml, FightView.xaml).
Organisation des Fichiers
/Model : Définitions des classes (Monster, Spell, Player, etc.).
/ViewModel : Logique métier et gestion des données.
/View : Interfaces utilisateur en XAML.

🔍 Features Avancées

Hashing des Mots de Passe : Sécurisation avec un algorithme de hachage pour le stockage des mots de passe.
Statistiques Améliorées des Ennemis : Les ennemis deviennent plus puissants à mesure que le joueur avance.
Connexion au Choix : Permet de basculer entre plusieurs bases de données via une simple modification dans les paramètres.
