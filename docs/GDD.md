# 🎮 Game Design Document — «Space Hunters 2»
##### *«Hold the line against the endless hoards of foes!»*

## ⚔️ Vision
### 📜 Description
*Take a place of the soldier standing againts the impending doom! The alien civilization took its course right at the Earth. You have volunteered to become the space guardian —  a brave soldier that defendes not only their home, but the galaxy against its offenders. Your courage knows no bounds, as you marching at the heart of the very evil all by yourself. Hovewer, no matter how hard you try, you just don't see the end of it... Which is good, because there will always be a plenty of foes to slay!* 

## 📄 Summary
**Space Hunters 2** is an arcade shooter in the outer space. It combines both classic arcade shooter and roguelite genres, providing dynamic gameplay and variety for every run. The player will take on role of a space soldier, fighting their foes without an end. 

On the battlefield, the player will encounter hoards of enemies: starting from one-pattern rookies, ending with multi-pattern enemies that to which a special treatment is advised. The various upgrades of the spaceship and random event system are meant to spice up the gameplay with new possibilities. 

## 🏛️ Core Mechanics

### ↕️ Movement
The battlefield is divided in 5 horisontal rows. All entities will move only in bounds of these rows.

#### 🤖 Player
The player can move from one row to another, up and down with a slight delay.

#### 👽 Enemy
The most enemies appears and moves on a random-selected row. They are constantly moving forward, eventually leaving the battlefield if player ignores them.

### 🔫 Shooting

#### 🤖 Player
The spaceship of the player is armed with laser machine gun that allows to fight back against enemies. Weapon and ammunition can differ depending on a spaceship the player is steering.

#### 👽 Enemy
Most enemies share the same shooting principle: shoot and reload. While they reloading their weapons, it gives a moment of opportunity for player to strike them. Shoot patterns will differ depending on enemy type.

### 🎯 Score

### ⚠️ Random Events
While the player spends their time on the battlefield, there is a chance for a accidental event to occur. The event itself is neither good nor bad: the player may exploit it or struggle against it. In either case, events can severely impact the gameplay. Hovewer, they are always temporary and will dissaper after some time.

### ⚡ Power-Ups
Upon killing an enemy, there are a slight chance for them to drop a power-up. The power-up is used only by the player. It gives a slight boost to the specific attribute of the player for the short period of time.  

### 💥 Ultimate Ability
Each spaceship of the player has its own ultimate ability. The features of every ability are unique and supporting the flavor of the spaceship it belongs to. The player can use the ability at the any time on the battlefield. After usage, it comes to cooldown which are depending on specific ability.

### 👺 Boss Fights
Upon collecting specific amounts of score, the boss ship will appear. After that, the boss fight will begin. While the boss fight lasts, there can be no any enemy spawn (besides if it is intended by a specific boss ship) and random events. The boss ship is the ultimate enemy that is strongest of all possible units. The

The boss fight is going with two different phases. The first phase is started at the beggining of the boss fight. Second phase is starter when boss ship is left with half of its max health. The boss ship has 2-3 patterns of attack per phase. Also, unlike all other enemies, the boss can move between different rows, just like the player do.

### 💰 Shop
*Is to be corrected...*

## 📈 Scope

### 🛠️ Prototype
In order for build to count as a prototype, it must have the following things implemented:
- 3 player spaceships; each having different style of the gameplay.
- 5 enemies spaceships; each with unique attack patterns.
- 1 boss ship.
- 3 random events.
- 5 power-ups. 

### 🎉 Full Release
*Is to be corrected...*