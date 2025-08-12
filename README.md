# 🎮 Plateformer 2D – Projet Unity

## 📌 Présentation
Ce projet est un **jeu de plateformer 2D** développé avec Unity.  
Le joueur incarne un personnage capable de courir, sauter, grimper et interagir avec des objets afin de progresser dans différents niveaux.  
Le but est de vaincre le **boss** final du niveau en le poussant dans l’eau **3 fois**, puis de passer par la porte pour accéder au niveau suivant.

---

## 🚀 Fonctionnalités principales
- Déplacement fluide du joueur (gauche/droite, saut, escalade d’échelle).
- **Système de vie** avec barre de PV et invincibilité temporaire après un coup.
- **Checkpoints** pour sauvegarder la position de respawn.
- **Collecte de pièces** affichée dans l’UI.
- **Boss avec 3 vies** : meurt après 3 chutes dans l’eau.
- **Système de respawn** en cas de chute dans l’eau ou de mort.
- **Porte de fin** qui téléporte vers le niveau suivant.
- **Menu paramètres** : réglage volume, résolution, plein écran.

---

## 🎮 Commandes
| Action               | Touche par défaut |
|----------------------|-------------------|
| Se déplacer à gauche | **Flèche gauche** ou **A** |
| Se déplacer à droite | **Flèche droite** ou **D** |
| Sauter               | **Espace** |
| Activer un levier / utiliser un objet | **E** |
| Monter/descendre échelle | **E** + flèches directionnelles |
| Pause / Menu         | **Échap** |

---

## 🗺 Système de jeu
### Pièces
- Collectibles répartis dans le niveau.
- Augmentent le score du joueur.

### Vie & Respawn
- PV max : **100**.
- Perte de vie en touchant un ennemi ou en tombant dans l’eau (optionnel).
- Respawn au dernier checkpoint activé.

### Checkpoints
- Activation en les touchant.
- Sauvegarde la position de respawn.

### Boss
- 3 chutes dans l’eau = mort.
- Se déplace et attaque selon son pattern.

### Eau
- Si joueur → respawn.
- Si boss → perte d’une vie.

### Porte de fin
- Active après mort du boss.
- Téléporte vers le niveau suivant.

### Menu paramètres
- Volume (AudioMixer).
- Résolution (Dropdown).
- Plein écran ON/OFF.

---

## 📜 Scripts principaux
| Script                  | Rôle |
|-------------------------|------|
| `PlayerMovement`        | Déplacement et sauts |
| `PlayerHealth`          | PV, dégâts, respawn |
| `HealthBar`             | UI de la vie |
| `HealPowerUp`           | Cœurs / soins |
| `PickUpObject`          | Gestion pièces |
| `EnemyPatrol`           | Mouvement ennemis |
| `WeakSpot`              | Détection saut sur tête ennemi |
| `BossHealth`            | PV du boss |
| `Checkpoint`            | Sauvegarde position de respawn |
| `WaterZone`             | Gère mort boss ou respawn joueur |
| `DoorNextLevel`         | Téléportation vers niveau suivant |
| `GameOverManager`       | Affiche Game Over |
| `SettingsMenu`          | Volume / Résolution / Plein écran |

---

## 🛠 Technologies utilisées
- **Unity** (version recommandée : 2022.x ou supérieure)
- **C#**
- **Unity UI** / TextMeshPro
- **AudioMixer** pour le son

---

## 📌 Installation
1. Cloner le dépôt :
   ```bash
   git clone https://github.com/votre-compte/plateformer-2d.git
   ```
2. Ouvrir le projet dans Unity.
3. Lancer la scène principale.

---
