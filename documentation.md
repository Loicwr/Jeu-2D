# 📜 Documentation – Plateformer 2D

## 🎮 Commandes
| Action               | Touche par défaut |
|----------------------|-------------------|
| Se déplacer à gauche | **Flèche gauche**  |
| Se déplacer à droite | **Flèche droite**  |
| Sauter               | **Espace** |
| Activer un levier / utiliser un objet | **E** |
| Monter/descendre échelle | **E** + flèches directionnelles |
| Pause / Menu         | **Échap** |

---

## 💰 Pièces
- Collectibles répartis dans le niveau.
- Augmentent le score du joueur.
- Gérés par `PickUpObject.cs` et affichés dans l’UI.

---

## ❤️ Vie & Respawn
- **PV de départ** : 100 (`PlayerHealth.cs`).
- Prendre un coup d’un ennemi ou d’un piège réduit la vie.
- Après un coup : **invincibilité 3 s** (effet clignotant).
- **Respawn** :
  - Si on tombe dans l’eau → vie réduite (optionnel) et retour au **dernier checkpoint activé**.
  - Si vie ≤ 0 → animation de mort + menu Game Over.

---

## 🏁 Checkpoints
- Le joueur active un checkpoint en le touchant (`Checkpoint.cs`).
- Sauvegarde la position actuelle.
- À la mort ou chute dans l’eau → respawn à ce checkpoint.

---

## 🐉 Boss – Mécanique “Pousser dans l’eau”
- PV : **3 “chutes” dans l’eau**.
- Le joueur doit utiliser l’environnement (plateforme mobile, levier…) pour **pousser le boss dans l’eau**.
- Chaque chute = -1 PV boss → il respawn sur sa zone de combat.
- Quand PV boss = 0 → mort du boss → apparition/activation de la porte de fin.

---

## 🌊 Eau
- Zone avec un `BoxCollider2D` en **isTrigger**.
- Scripts liés :
  - **Si c’est le joueur** → respawn au checkpoint.
  - **Si c’est le boss** → appel à `BossHealth.Hit()`.

---

## 🚪 Porte de fin
- Porte présente dans la scène.
- Quand le boss est mort, la porte s’active.
- Collision avec le joueur → `SceneManager.LoadScene()` vers le **niveau suivant**.

---

## 🪜 Échelles
- Interaction avec **E** près d’une échelle (`Ladder.cs`).
- Pendant l’escalade, le joueur peut se déplacer verticalement.
- Sortir de la zone = escalade arrêtée.

---

## 🎚 Menu Paramètres
Accessible depuis le menu principal ou pause.
- **Volume** : géré via `AudioMixer` (`SetFloat("volume", ...)`).
- **Résolution** : liste déroulante avec `Dropdown` ou `TMP_Dropdown`.
- **Plein écran** : booléen modifiant `Screen.fullScreen`.

---

## 📜 Organisation des scripts
| Script                  | Rôle |
|-------------------------|------|
| `PlayerMovement`        | Déplacement et sauts |
| `PlayerHealth`          | PV, dégâts, respawn |
| `HealthBar`             | UI de la vie |
| `HealPowerUp`           | Cœurs / soins |
| `PickUpObject`          | Gestion pièces |
| `EnemyPatrol`           | Mouvement ennemis |
| `WeakSpot`              | Détection saut sur tête ennemi |
| `BossHealth`            | PV du boss (3 chutes dans l’eau) |
| `Checkpoint`            | Sauvegarde position de respawn |
| `WaterZone`             | Gère mort boss ou respawn joueur |
| `DoorNextLevel`         | Téléportation vers niveau suivant |
| `GameOverManager`       | Affiche Game Over |
| `SettingsMenu`          | Volume / Résolution / Plein écran |

---


