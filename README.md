# 🎮 Pokemon en C# avec WPF

Bienvenue dans **Pokemon**, une application développée en **C# avec WPF**. Ce projet propose une simulation de combats au tour par tour, inspirée de l'univers Pokémon, avec une interface utilisateur intuitive et une gestion complète de la base de données.

---

## 🎯 Objectifs du Projet

- **Programmation Orientée Objet (POO)** : Organisation propre et modulaire du code.
- **Respect du Modèle MVVM** : Séparation des responsabilités entre Vue, Modèle et ViewModel.
- **Base de Données SQL Server Express** :
  - Gestion des utilisateurs (connexion sécurisée avec hash des mots de passe).
  - Stockage des monstres et des sorts dans une structure normalisée.
- **Système de Combat au Tour par Tour** :
  - Utilisation des sorts pour infliger des dégâts.
  - Affichage des barres de santé pour chaque monstre.
  - Génération dynamique d'ennemis plus puissants au fil des combats.

---

## 💻 Fonctionnalités

### 1. Écran de Connexion (Login)
- Les utilisateurs peuvent se connecter avec un **nom d'utilisateur** et un **mot de passe**.
- Les mots de passe sont **hashés** pour une sécurisation optimale avant stockage.

### 2. Écran des Paramètres
- Configuration de la chaîne de connexion à la base de données via un champ dédié.
- Initialisation automatique d'un jeu de données par défaut : **monstres**, **sorts** et **utilisateurs**.

### 3. Gestion des Monstres
- **Liste des Monstres** : Affichez tous les monstres disponibles.
- **Détails d'un Monstre** : Consultez son nom, ses HP, son image et ses sorts associés.
- **Sélection du Monstre Joueur** : Choisissez le monstre que vous voulez incarner.

### 4. Gestion des Sorts
- **Liste des Sorts** : Visualisez tous les sorts disponibles dans le jeu.
- **Détails d'un Sort** : Nom, dégâts et description.
- **Tri des Sorts** : Par monstre qui les possède.

### 5. Combat au Tour par Tour
- **Mécanique de Combat** :
  - Utilisez les sorts pour infliger des dégâts à l'adversaire.
  - Visualisez les barres de santé de chaque monstre.
- **Génération Dynamique d'Ennemis** :
  - Les ennemis deviennent plus puissants à chaque combat (ex. : +10% HP, +5% dégâts).
- **Score** : Gagnez des points pour chaque monstre vaincu.
- **Rejouer** : Relancez un combat avec un nouvel adversaire.

---

## ⚙️ Installation

### Prérequis
- **Visual Studio** (avec support pour WPF et .NET 6 ou supérieur).
- **SQL Server Express**.
- **Git**.

### Étapes d'Installation

1. **Clonez le dépôt GitHub** :
   ```bash
   git clone https://github.com/Yukojuni/PokemonWPF.git
   cd PokemonWPF/
   Executer l'app
  

## 📦 Packages Utilisés

- **Entity Framework Core** : Gestion de la base de données (lecture, écriture).
- **CommunityToolkit.Mvvm** : Implémentation simplifiée du modèle MVVM.
- **Microsoft.EntityFrameworkCore.SqlServer** : Fournisseur pour SQL Server.
- **Microsoft.EntityFrameworkCore.Design** : Outils pour le développement avec Entity Framework.
- **Microsoft.EntityFrameworkCore.Tools** : Outils pour les migrations et la gestion de base de données.

---

## 🗂️ Architecture

### Modèle MVVM
- **Model** : Représente les données du jeu, telles que les monstres, les sorts et les joueurs.
- **ViewModel** : Gestion des données et des interactions pour chaque vue (ex. : `GameVM`, `FightVM`).
- **View** : Fichiers XAML pour l'interface utilisateur (ex. : `GameView.xaml`, `FightView.xaml`).

### Organisation des Fichiers
```plaintext
/Model       -> Définitions des classes (Monster, Spell, Player, etc.).
/ViewModel   -> Logique métier et gestion des données pour chaque vue.
/View        -> Interfaces utilisateur en XAML.
