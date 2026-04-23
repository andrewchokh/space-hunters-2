# 🎮 **«Space Hunters 2»** — GDD

```text
Game Design Document
«Hold the line against the endless hoards of foes!»

Version: 2.0
Last Updated: Apr 21, 2026
```

---

## 1. ⚔️ Project Vision & Summary

### 📜 Description
Take the place of a lone soldier standing against impending doom. An alien civilization has set its course directly for Earth, and you have volunteered to be the first and last line of defense. As a space guardian, your courage knows no bounds as you march into the heart of evil. No matter how many foes you slay, the horde seems endless—which is perfect, because there will always be more enemies to hunt.

### 📄 Summary
**Space Hunters 2** is a high-octane arcade shooter set in deep space. It blends classic arcade "shmup" action with modern roguelite variety to ensure every run feels unique. Players pilot powerful spaceships against waves of diverse enemies, ranging from simple rookies to complex bosses requiring tactical precision.

---

## 2. 🕹️ Core Gameplay Mechanics

The game utilizes a **Hybrid Feature-Based Structure** where players and enemies interact within a strictly defined battlefield.

### ↕️ The Five-Row System
The battlefield is locked into **5 horizontal rows**. All movement and combat occur within these bounds.
* **Player Movement:** Players switch between rows with a slight, intentional delay to reward timing and prediction.
* **Enemy Navigation:** Enemies spawn on random rows and move forward. If ignored, they eventually exit the screen.

### 🔫 Combat & Shooting
Combat follows a "Shoot and Reload" rhythm, creating windows of vulnerability for both the player and the AI.

| Entity | Shooting Style | Strategic Note |
| :--- | :--- | :--- |
| **Player** | Rapid-fire laser machine guns (variant-dependent). | Weaponry changes based on the chosen ship. |
| **Enemies** | Pattern-based bursts with clear reload windows. | Players should strike while the enemy is reloading. |

---

## 3. 🌪️ Dynamic Systems

To keep the gameplay loop engaging, several systems introduce randomness and tactical depth.

### ⚠️ Random Events
Temporary "World Events" can trigger at any time. These are neutral modifiers that can be exploited for gain or struggled against, significantly shifting the battlefield's behavior for a limited duration.

### ⚡ Power-Ups
Destroyed enemies have a chance to drop temporary boosts.
* **Targeting:** Only accessible to the player.
* **Effect:** Provides a short-term buff to specific attributes like fire rate or speed.

### 💥 Ultimate Abilities
Every ship in the **Players/** and related categories features a unique "ability".
* **Flavor-Driven:** Abilities are tailored to the ship's specific role (e.g., area clear vs. high damage).
* **Resource:** Governed by a cooldown timer to prevent spamming.

---

## 4. 👺 Boss Fights & Progression

### 👹 The Boss Encounter
Bosses are the ultimate test of a player's skill. Upon reaching a score milestone, a Boss ship appears, suspending all standard spawns and events.

* **Phases:** Bosses have two distinct phases (Phase 2 triggers at 50% Health).
* **Patterns:** Each phase introduces 2-3 unique attack patterns.
* **Mobility:** Unlike standard enemies, Bosses can switch rows just like the player.

### 💰 The Shop
*(Currently under refinement)* — This system will allow players to trade score or currency for permanent or semi-permanent ship upgrades between combat waves.

---

## 5. 📈 Development Scope

The project follows a modular **Vertical Slicing** approach to ensure that each milestone is playable and polished.

### 🛠️ Demo Milestone
To reach a successful *DEMO* state, the following content must be fully implemented and integrated:

| Category | Requirement |
| :--- | :--- |
| **Player Ships** | 3 distinct models with unique gameplay styles. |
| **Enemy Ships** | 5 unique variants with distinct attack/move patterns. |
| **Bosses** | 1 fully realized Boss ship with two phases. |
| **Systems** | 3 Random Events and 5 unique Power-Ups. |

---

### 🎉 Full Release
*(To be corrected and expanded...)*

This document serves as the creative guide for all contributors, ensuring the "feel" of the game remains consistent even as the technical architecture evolves.