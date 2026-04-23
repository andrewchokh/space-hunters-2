# **«Space Hunters 2»** — TDD
```
Technical Design Document

Version: 2.0
Last Updated: Apr 21, 2026 
```

## 1. 📑 Project Structure
This project utilizes a **Hybrid Feature-Based Structure**, a modular architecture designed to balance long-term scalability with ease of navigation. This approach organizes the codebase into two distinct logical dimensions:

**Horizontal Layers (The Foundation):** The `Core/` and `Assets/` directories function as the infrastructure layers. These contain the fundamental building blocks of the game — global utilities, shared interfaces, and raw media — providing essential services that are consumed by all other systems.

**Vertical Features (The Gameplay):** The `Features/` and `Spaceships/` directories are organized by domain rather than file type. Each folder serves as a self-contained module encompassing the scripts, scenes, and logic specific to a single mechanic (e.g., *Combat* or *Spawning*). This **"Vertical Slicing"** ensures that modifications to specific gameplay systems remain isolated, significantly reducing technical debt and making the project more maintainable as the game grows in complexity.

By combining these two patterns, the project leverages Godot's scene-based composition while maintaining the strict decoupling and modularity required for professional C# development.

```
res://
├── Assets/                      # Raw game data (audio, fonts, etc.)
├── Core/                        # Shared backbone logic and framework
│   ├── Camera/                  # Camera nodes, management and behavior
│   ├── Components/              # Core-level reusable components
│   ├── Globals/                 # Autoloaded scripts (Singletons)
│   ├── Interfaces/              # C# Interfaces for decoupling logic
│   └── GDLog.cs                 # Centralized logging utility
├── Docs/                        # Project documentation
│   ├── GDD.md                   # «Game Design Document»
│   └── TDD.md                   # «Technical Design Document»
├── Features/                    # Modular gameplay systems
│   ├── Combat/                  # Damage, projectiles, and health logic
│   ├── Movement/                # Physics and navigation systems
│   └── Spawning/                # Entity instantiation and wave logic
├── Spaceships/                  # Ship entities and specialized logic
│   ├── Components/              # Ship-specific composition nodes
│   ├── Enemies/                 # Enemy AI variants
│   └── Players/                 # Player-controlled ship logic
├── UI/                          # Graphical User Interface
│   ├── Widgets/                 # Reusable UI widgets (buttons, bars, etc.)
│   ├── GameScene.tscn           # HUD and in-game UI overlays
│   └── MainScene.tscn       	 # Initial entry point
├── project.godot                # Godot project configuration
├── <project-name>.csproj        # C# Project file
└── <project-name>.sln           # C# Solution file
```

## 2. 🔠 Coding Conventions

This project adheres to a standardized set of coding practices to maintain consistency and readability across the codebase. By following these conventions, the development process remains efficient and integration between different modules stays seamless.

### Code Style and Naming
Development should align with the official [C# Style Guide](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_style_guide.html) provided by the [Godot documentation](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/index.html). In addition to the standard guide, the following project-specific naming rules apply:

* **Directories:** Use `PascalCase` for all directories names (e.g., `Combat/`, `Spawing/`).
- **Generic Files:** Use `snake_case` for all generic non-script files, like assets or configs (e.g., `Combat/`, `Spawing/`).
* **Scenes and Scripts:** Use `PascalCase` for all `.tscn` and `.cs` files to match class naming conventions (e.g., `PlayerShip.tscn`, `MovementController.cs`).

### Documentation and XML Comments
Code must be self-documenting to ensure full IntelliSense support and ease of onboarding. Public members require XML documentation to explain their purpose and usage.

* **Summary:** Every public class, method, and signal requires a `<summary>` tag describing its primary function.
* **Parameters and Returns:** Methods involving inputs or outputs must utilize `<param>` and `<returns>` tags to define expected data ranges and nullability.
* **Architectural Remarks:** For complex logic, the `<remarks>` tag should be used to explain the "why" behind an implementation or any technical constraints.

#### Implementation Examples

The following examples demonstrate the expected level of detail for class and method documentation:

```csharp
/// <summary>
/// Manages the global grid and coordinate conversions for the game world.
/// Use this to translate mouse positions into tile coordinates.
/// </summary>
public partial class MapManager : Node
{
    // Implementation details...
}
```

```csharp
/// <summary>
/// Retrieves the Y-coordinate for a given row index. The index is safely clamped to the available bounds.
/// </summary>
/// <param name="rowIndex">The index of the desired row.</param>
/// <returns>The global Y-coordinate position.</returns>
/// <remarks>
/// This logic assumes the map origin is centered at (0,0). 
/// Offset calculations must be updated if the map anchor changes.
/// </remarks>
public float GetRowY(int rowIndex)
{
    // Implementation details...
    return FixedRows[rowIndex];
}
```

## 3. 🖼️ Architectural Patterns

The architecture of the project is built on three core pillars: **Composition**, **Data-Driven Design**, and the **Strategy Pattern**. This combination ensures that adding a new enemy or a unique weapon requires minimal new code.

---

### 🧩 Composition (The Component Pattern)
Instead of using deep inheritance (e.g., `Player : Ship : PhysicsBody`), we use **Composition**. Mechanics are broken down into small, specialized nodes called **Components**. 

* **Rule of Thumb:** A component should do one thing (e.g., manage health, handle movement, or control weapons).
* **Decoupling:** Components find their "Actor" (the ship they belong to) automatically via the parent-child relationship.

```cs
[GlobalClass]
public partial class HealthComponent : Node2D, IComponent
{
    // The "Actor" property ensures the component knows who it's serving
    // without needing a manual [Export] assignment.
    public Node2D Actor => GetParent() as Node2D;

    [Export] public SpaceshipData Data;

    private int _currentHealth;

    public override void _Ready()
    {
        if (Data == null) return; // Always check for null in DDA!
        _currentHealth = Data.BaseHealth;
    }
}
```

---

### 📊 Data-Driven Design (Resources)
We separate **Logic** (C# scripts) from **Configurations** (Godot Resources). By using `.tres` files, we can create dozens of different spaceships or projectiles without writing new classes.

* **Modular Data:** You can swap a `SpaceshipData` resource in the Inspector to instantly turn a weak "Rookie" into a tanky "Elite."
* **Versioning:** Since resources are text-based files, they are easy to track in Git.

```cs
[GlobalClass]
public partial class SpaceshipData : Resource
{
    [ExportGroup("Base Stats")]
    [Export] public int BaseHealth = 10;
    [Export] public int BaseProtection = 1;
    [Export] public float MoveSpeed = 200.0f;
}
```

---

### 🧠 Strategy Pattern (Behavioral Resources)
This is the "Brain" of our DDA. While `SpaceshipData` holds numbers, `Patterns` hold **Logic**. By treating logic as a resource, we can change how an enemy moves or shoots at runtime without modifying the `AIComponent`.

* **Attack Patterns:** Determine the rhythm of fire (e.g., Burst vs. Single Shot).
* **Movement Patterns:** Determine the flight path (e.g., Zig-Zag vs. Lane-Switching).

```cs
public abstract partial class AttackPattern : Resource
{
    // Patterns use 'virtual' or 'abstract' methods to define 
    // "How" an action is performed.
    public abstract void ProcessAttack(EnemyWeaponComponent weapon, double delta);
}
```

---

### 🌉 Interface Decoupling (The Bridge)
To prevent the "God Object" problem and avoid `InvalidCastException` errors, components communicate with Actors through **Interfaces**. 

Instead of a `MovementComponent` asking for a `PlayerShip`, it asks for an `IMovable`. This allows the same component to move a player, an enemy, or even a falling asteroid.

> **Key Interface: `IDirectable`**
> Used to bridge the gap between AI Patterns and Weapon/Movement components. It allows a "Brain" (Pattern) to set a `Velocity` or an `IsFiring` state on a "Body" (Actor) without knowing the Body's specific type.

---

### ✅ Summary Table: Which to Use?

| Scenario | Pattern to Use | Why? |
| :--- | :--- | :--- |
| Adding a "Shield" mechanic | **Component** | It’s a reusable behavior that can be "plugged into" any ship. |
| Balancing Enemy health/speed | **Resource-Based Data** | No code changes; just edit the `.tres` values. |
| Creating a unique Boss attack | **Strategy Pattern** | Allows custom logic scripts to be swapped into a standard weapon shell. |
| Making AI talk to Physics | **Interfaces** | Keeps the AI "smart" but the physics "dumb" and decoupled. |

## 4. 📂 Git Workflow & Contribution

To maintain a clean and manageable codebase, the project follows a strict version control protocol. This ensures that the `main` branch remains playable while new features are developed in isolation.

---

### 🌿 Branching Strategy
The **Feature Branch Workflow** isolates new development from the stable codebase, allowing for thorough testing and code review before integration.

| Branch Type | Name | Purpose |
| :--- | :--- | :--- |
| **Production** | `main` | Contains stable, production-ready builds. Miscellaneous minor hotfixes may be pushed directly, but core changes must come from `develop`. |
| **Integration** | `develop` | The staging area for the next release. Features are merged here for integration testing and optimization. Expect this branch to be "bleeding edge." |
| **Development** | `feature/*` | Created for specific tasks or assets. These must be kept small and focused. If a feature addresses an issue, prepend the issue number. |

**Naming Convention:**
* `feature/<feature-name>` (e.g., `feature/enemy-zigzag-logic`)
* `feature/<issue-number>-<feature-name>` (e.g., `feature/42-health-component-fix`)

---

### 📝 Commit Guidelines
Commits are the "history book" of tge project. To keep them readable and searchable, follow the **Conventional Commits** specification.

#### The Golden Rules
1.  **Imperative Mood:** Write the summary as if you are giving a command to the code (e.g., `add` not `added`).
2.  **Lowercase:** Start the summary with a lowercase letter.
3.  **Conciseness:** Keep the subject line under 50 characters. Use the body for extended details.

#### Commit Types
To categorize changes at a glance, use the following prefixes:

* `feat:` A new feature or mechanic.
* `fix:` A bug fix.
* `refactor:` Rewriting code logic without changing external behavior (e.g., implementing an interface).
* `assets:` Adding or updating sprites, sounds, or `.tres` files.
* `docs:` Changes to documentation or code comments.
* `style:` Formatting, white-space, or minor optimization (no logic changes).
* `chore:` Maintenance tasks, moving files, or updating `.gitignore`.

**Examples:**
* `feat: add smooth zoom to GameCamera`
* `refactor: implement IHDirectable in AIComponent`
* `chore: move GameCamera scene to objects/`

---

### 🚀 Pull Request (PR) Protocol
All code intended for `develop` must undergo a Pull Request. This is our primary line of defense against "breaking the build."

#### PR Requirements
Every PR must be populated with the following sections:
1.  **Summary:** A high-level overview of what this PR accomplishes.
2.  **Key Changes:** A bulleted list of modified files or logic shifts.
3.  **Testing Steps:** Clear instructions on how to verify the changes in the Godot Editor.
4.  **Visual Evidence:** Screenshots or GIFs are **mandatory** for any UI or visual gameplay changes.

#### Review Process
All PRs must be reviewed and approved by [@andrewchokh](https://github.com/andrewchokh) before they are merged. 

> **Pro-Tip:** Small, frequent PRs are merged much faster than "mega-PRs." If your feature is getting too large, consider breaking it into sub-tasks!

---

This structure ensures that as the project grows, we can trace every line of code back to a specific intent, making debugging and collaboration significantly easier. 

## 5. 🛠️ Tech Stack & Ecosystem

To ensure high-performance execution and a streamlined development workflow, the project leverages a modern, robust tech stack. The project is built to take full advantage of the latest advancements in the .NET ecosystem and the Godot engine.

---

### 🚀 Core Runtime & Frameworks
The following dependencies form the backbone of the project’s architecture. These are required to compile and run the source code.

| Dependency | Version | Role |
| :--- | :--- | :--- |
| **.NET SDK** | `10.0.x` | Provides the modern C# runtime, high-performance JIT compilation, and advanced language features. |
| **Godot Engine (.NET Edition)** | `4.6.x` | The primary game engine, utilizing the C# bridge for all gameplay logic and systems. |

---

### 📂 Version Control & Collaboration
The project utilizes a cloud-based infrastructure to manage source code and facilitate collaborative development.

* **GitHub:** Serves as the central repository host. It manages the **Feature Branch Workflow**, handles code reviews through Pull Requests, and tracks project issues.
* **Git:** The underlying version control system required for local development and synchronization with the remote repository.

---

### 🎨 Creative Tools (Optional)
While not strictly required to run the code, the following tools are recommended for contributors working on the visual and aesthetic direction of the game.

* **Aseprite:** The designated industry standard for pixel art and animation. It is used to generate the project's spritesheets, UI elements, and frame-by-frame visual effects. 
    > *Note: While alternatives exist, Aseprite is preferred for maintaining consistency in the project's "low-fi" aesthetic.*

---

This stack was selected to balance developer productivity with the granular control needed for a fast-paced, bullet-dense shooter.
