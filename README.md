# Spell‑Forge Ability System

A modular **ScriptableObject‑driven** ability framework for Unity 2022.3 LTS.
The repo contains a tiny top‑down prototype (blue "hero" cube vs. red "enemy" cubes) that demonstrates three chainable abilities and a minimal win‑screen UI.

---

## ✨ Features

| Module                 | Description                                                                                                        |
| ---------------------- | ------------------------------------------------------------------------------------------------------------------ |
| **Ability**            | Triggers its behaviour if the cooldown allows and no other ability is running.                                     |
| **AbilityBehaviour**   | Walks through **AbilityPhases** sequentially.                                                                      |
| **AbilityPhase**       | Runs a timer; at configured **normalised moments** fires lists of **Consequences**.                                |
| **Consequences**       | Gameplay effects that can chain further consequences. – Implemented: `RectOverlap`, `SphereOverlap`, `DealDamage`. |
| **Stat**               | ScriptableObject wrapper around `float` for designer‑friendly tuning.                                              |
| **Game State Manager** | Tracks live enemies, shows **Win Panel**, handles scene reload.                                                    |
| **Object Pool**        | Re‑uses VFX objects to avoid GC allocations.                                                                       |

---

## 🎮 Demo Scene

*Scene*            `Assets/SpellForge/Scenes/AbilitySystemTest.unity`

```
Main Camera (isometric follow)
_Systems/
  GameStateManager  ← detects victory
_Actors/
  Hero (blue)
  Enemies (red)
_GUI/
  Canvas → WinPanel
```

| Controls    | Action                                                             |
| ----------- | ------------------------------------------------------------------ |
| **W A S D** | Move hero                                                          |
| **1**       | *Circular Slash* (360° sphere overlap, deals damage)               |
| **2**       | *Quick Slash* (frontal box overlap, deals damage)                  |
| **3**       | *Slash and Stomp*<br>Phase 1 – box slash<br>Phase 2 – ground stomp |
| **R**       | Reload scene after win                                             |

Beat both enemies → “**Win!**” panel appears.

---

## 📦 Requirements

* **Unity 2022.3 LTS** (URP / Built‑in both work)
* Packages

  * [`Cysharp/UniTask`](https://github.com/Cysharp/UniTask)
  * (optional) [`Extenject`](https://github.com/Extenject/Extenject) for dependency injection

Clone and open with Unity Hub – no extra setup needed.

```
git clone https://github.com/zyzykin/spell-forge-ability-system.git
```

---

## 🗂️ Project Layout

```
SpellForge/
  Materials/
  Prefabs/
  Scenes/
  ScriptableObjects/
    Abilities/        ← CircularSlash, QuickSlash, …
    Consequences/     ← RectOverlap, DealDamage, …
  Scripts/
    AbilitySystem/
      Components/     ← AbilitySystem (MonoBehaviour), HeroController
      Consequences/   ← runtime logic
      Presentation/   ← WinPanelView, CameraFollow
    Application/      ← GameStateManager
```

> **asmdef recommendation**
>
> * Game.Domain – pure SO definitions & logic
> * Game.Application – managers, pools, game state
> * Game.Presentation – MonoBehaviours & UI

---

## 🚀 Running & Extending

1. Open **AbilitySystemTest** scene and press *Play*.
2. To create a new ability:

   1. `Create → AbilitySystem → Ability`.
   2. Define cooldown, key binding and phases.
   3. Drop ready‑made consequences or author new ones by inheriting `Consequence`.
3. Add more enemies – `EnemyHealth` handles death & victory flow automatically.


Feel free to fork, extend and submit PRs!
