# Tracer Gameplay Framework

A Unity gameplay project inspired by **Tracer** from Overwatch.

The purpose of this project was **not to recreate Overwatch**, but to design a reusable gameplay framework that can be extended with new heroes, abilities and weapons.

---

# Features

## Gameplay

- First Person Controller
- Hitscan Weapon
- Blink
- Recall
- Pulse Bomb
- Ultimate Charge
- Health & Damage System
- Projectile Framework
- Ability Cooldowns
- Ability Charges

---

# Architecture

The project follows a modular architecture.

Every gameplay feature is implemented as an independent system with a single responsibility.

```
                PlayerManager
                      │
      ┌───────────────┼────────────────┐
      │               │                │
   Input          Movement        AbilitySystem
      │                                 │
      │                           AbilitySlot
      │                                 │
      ▼                                 ▼
 Camera                        HeroAbility
                                        │
                   ┌────────────────────┼────────────────────┐
                   │                    │                    │
                 Blink               Recall            Pulse Bomb
```



---

# Core Systems

## PlayerManager

The central object of the player.

It owns references to every gameplay system.

Responsibilities:
- Input
- Camera
- Character Controller
- Weapon
- Ability System
- Health
- Ultimate Charge

PlayerManager **does not implement gameplay**.

It only connects systems together.

---

## PlayerInput
Responsible only for reading player input.

It does **not** know anything about heroes or abilities.

Responsibilities:
- Movement
- Mouse Look
- Shoot
- Reload
- Shift
- E
- Ultimate

---

## AbilitySystem
The AbilitySystem receives player input and activates abilities.

It does not know how Blink or Recall work.

Responsibilities:

- Ability initialization
- Ability activation
- Ability updates

---

## AbilitySlot
Each ability is wrapped inside an AbilitySlot.

AbilitySlot is responsible for:
- Cooldowns
- Charges
- Recharge
- Activation

It does **not** know what the ability actually does.

---

# Hero Framework

Every hero ability inherits from the same base class.

```
HeroAbility
│
├── Blink
├── Recall
└── PulseBomb
```

Every ability exposes the same interface.

```csharp
Initialize()

Activate()

Tick()

Deactivate()

GetData()
```

Because of this, the AbilitySystem never depends on concrete abilities.

---

# Ability Data
Gameplay configuration is separated from gameplay logic.

```
AbilityData
│
├── BlinkData
├── RecallData
└── PulseBombData
```

AbilityData contains values such as:
- Cooldown
- Charges
- Speed
- Distance
- Radius

This allows balancing without changing code.

---

# Weapon Framework

```
Weapon
│
├── HitscanWeapon
└── ProjectileWeapon
```

Weapon defines the common interface.
Different weapon implementations provide different shooting behavior.

---

# Projectile Framework

```
Projectile
│
└── PulseBombProjectile
```

Projectile is responsible for:
- Launch
- Collision
- Lifetime

Movement is delegated to another component.

```
ProjectileMovement
│
└── RigidbodyProjectileMovement
```

This makes projectile behaviour replaceable without modifying the projectile itself.

---

# Damage System
The project uses a generic damage pipeline.

```
Weapon
      │
      ▼
DamageInfo
      │
      ▼
IDamageable
      │
      ▼
Health
```

Instead of passing only a float damage value, a DamageInfo object is used.

This makes it easy to extend the system with:
- Critical Hits
- Damage Types
- Knockback
- Status Effects
- Owner information

---

# Health System
Health is completely independent.

Responsibilities:
- Current Health
- Healing
- Damage
- Death Event

Health knows nothing about:
- Weapons
- Heroes
- AI
- Projectiles

---

# Recall System
Recall is implemented using a history recorder.

```
HistoryRecorder
        │
        ▼
HistorySnapshot
        │
        ▼
Recall
```

The recorder continuously stores player snapshots.
Recall restores one of those snapshots.
This keeps recording and restoring completely separated.

---

# Ultimate Charge
Ultimate charge is an independent gameplay system.

Responsibilities:
- Gain Charge
- Consume Charge
- Read Current Charge

Abilities do not calculate charge themselves.

---

# Bots
Three simple training bots were implemented.

```
TrainingBot
│
├── Receives Damage
├── Dies
└── Respawns

ShootingBot

WalkingBot
```

They are designed for gameplay testing rather than AI complexity.

---

# Design Principles
The project was built around several core principles.

## Single Responsibility Principle
Each class has one responsibility.

Examples:
- Health → HP only
- AbilitySlot → Cooldowns only
- Projectile → Projectile logic only
- AbilityData → Configuration only

---

## Composition over Monolithic Design

Large gameplay systems are built from smaller reusable components.

Example:

```
Player

├── Camera
├── Input
├── Weapon
├── Health
├── AbilitySystem
└── UltimateCharge
```

instead of one massive Player class.

---

## Data Driven Design
Gameplay values are stored inside ScriptableObjects.
Changing gameplay usually does not require changing code.

---

# Implemented Hero
Tracer

Abilities:
- Blink
- Recall
- Pulse Bomb

Weapon:
- Dual Hitscan Pistols

---

# Technologies

- Unity
- C#
- ScriptableObjects
- CharacterController
- Physics
- New Input System

---

# Goal
The purpose of this repository was to study gameplay architecture used in hero shooters.
The focus of the project is **clean gameplay code**, **modular systems**, and **extensible architecture**, rather than recreating every feature of the original game.
