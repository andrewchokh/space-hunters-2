# Technical Design Documentation v1.1 - «Space Hunters 2»

## 1. 📑♻️ Project Structure
```
res://
├── assets/
│    ├── fonts/
│    ├── music/
│    ├── sounds/
│    └── sprites/
├── docs/
│    ├── GDD.md                 # Game Design Document
│    └── TDD.md                 # Technical Design Document
├── resources/                  # Godot Resource Files
│    └── tilesets/          
├── scenes/                     # Nodes and their Scripts
│    ├── components/            # Composition Components
│    ├── objects/           
│    ├── spaceships/            # Player and Enemy spaceships
│    ├── test/                  # Test Scenes for Features
│    └── ui/                    # Various UI Elements
├── scripts/                    # Orphan Scripts with No Attached Nodes
│    ├── globals/               # Autoload Scripts
│    └── state_machine/         # State Machine related Logic
├── project.godot
├── <project-name>.csproj
└── <project-name>.sln
```

## 2. 🔠 Coding Conventions

### Code Style
While writing the code, try your best to follow the [C# Style Guide](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_style_guide.html) from Godot's documentation.
- Use `snake_case` for directories and generic godot/.net files;
- Use `PascalCase` for scenes and scripts names;
- When creating test scene for a feature, name it: `TestScene<FeatureName>`.

### Documentation Standard
All code must be self-documenting through XML comments to ensure IntelliSense support in IDEs.
- `<summary>`: Required for all public classes, methods, and signals.
- `<remarks>`: Used to explain the "intent" or architectural "why" behind a complex system.
- `<param>/<returns>`: Required for any method with inputs or outputs to define valid ranges or nullability.

#### Examples
```cs
/// <summary>
/// Manages the global grid and coordinate conversions for the game world.
/// Use this to translate mouse positions into tile coordinates.
/// </summary>
public partial class MapManager : Node
{
    // Implementaion...
}
```

```cs
/// <summary>
/// Retrieves the Y-coordinate for a given row index. The index is safely clamped to the available bounds.
/// </summary>
/// <param name="rowIndex">The index of Y-coordinate global position.</param>
/// <returns>The Y-coordinate in global position.</returns>
/// <remarks>
/// This assumes the map is centered at (0,0). If the map shifts, 
/// this logic will need to be updated to include the offset.
/// </remarks>
public float GetRowY(int rowIndex)
{
	// Implementaion...

	return FixedRows[rowIndex];
}
```

### 3. Patterns

#### Component (Composition)
Composition pattern helps to make a certain mechanic reusable for all entities regardless of their class. Components are disposable: they are tied to node, and will die with it.
```cs
public partial class ExampleComponent : Node
{
    // Use [Export] to assign the owner in the editor.
	// Tip: Use a specific base class like 'Node2D' or 'CharacterBody2D' 
    // if the component requires advanced data.
    [Export] 
    public Node2D Entity; 

    public override void _Ready()
    {
        if (Entity == null) 
            GD.PushError($"{Name}: Entity is not assigned!");
    }
}
```

#### Singleton (Autoload)
The Singleton pattern is a useful tool for solving the common use case where you need to store persistent information between scenes. In our case, it's possible to reuse the same scene or class for multiple singletons as long as they have different names.
```cs
public partial class ExampleAutoload : Node
{
	// Static keyword is used only for this particular field.
	// Do not use it for any other field or method!
	public static ExampleAutoload Instance { get; private set; }

	// Other fields...

	public override void _Ready()
	{
		Instance = this;

		// Rest of the code...
	}

	// Other methods...
}
```

#### Finite State Machine
A finite-state machine system will be used to track and change the behavior of most of the characters and objects. The state machine will be implemented as component (Composition pattern). As for states themselves, they will be an orhan abstract script to create unique states for every node. As a general rule, specific `State` implementation is unique to its node, and cannot be reused elsewhere.
```cs
/// <summary>
/// Base class for all states. Logic is handled by the state, 
/// while the StateMachine handles the transitions.
/// </summary>
public abstract partial class State : Node
{
	public abstract void Enter();
	public abstract void Exit();
	public abstract void Update(double delta);
}
```

#### Signal Bus
Is to be implemented...

#### Resource-Based Data
Resources are valuable and flexible data structure of Godot. They are allowing to create modular data chunks, store them in the project as a separate files, and easily use them in scripts. Instead of hardcoding specific values to every node that shares the same functional, we can use `Resourse` to store these values. For example, we can write a bunch of resources for every spaceship's stats. 
```cs
/// <remarks>
/// [GlobalClass] is required for the editor, so it would show 
/// the resource in the selection menu.
/// </remarks>
[GlobalClass]
public partial class ExampleRecource : Resource
{
    // Fields and methods...
    // Use [Export] for fields, so they would show up in the editor
}
```

## 4. ♻️ Workflow

### Branches
[The Feature Branch](https://www.atlassian.com/git/tutorials/comparing-workflows/feature-branch-workflow) workflow is used for the repository. It provides ease of adding new features to the project, while not being overwhelming for a small team of developers.

- `main`  branch is used for stable and playable builds. Small or miscellaneous commits can be pushed straight into the branch. 

-  `develop` branch is used for feature addition and bug resolvement. The branch contains an unstable build of the game that the team is working on.
Before merging the branch into `main` branch, the developers should test the build properly: refine mechanics, fix bugs and optimize the code.

- `feature/` branches are used for adding new content into the project. For example, if you want to add a new mechanic or an asset. These features should be small and consistent. Also, if the feature is connected to an issue directly, use issue number at the start of the name.

#### Examples
- `feature/<feature-name>`
- `feature/<issue-number>-<feature-name>`

### Commits

#### Naming
The commmits name must be written using the imperative mood and start with the lower case. Also, they must be consice (under 50 characters) and informative. If you need to provide additional information or clarification, use the commit's body to write a paragraph.

##### Examples
- `add player movement`;
- `implement player UI health bar`
- `access MapManager via Singleton instance`

#### Conventional Commits
When commiting a specific change, you should specify its 'type'. It helps to differentiate the commits by their purpose.
- `feat:` for a new feature;
- `fix:` for a bug fix;
- `refactor:` for miscellaneous changes, that is not involving the code;
- `docs:` for documentation changes;
- `assets:` for assets addition;
- `style:` for code optimizing and formatting. 

##### Examples
- `feat: add smooth zoom to GameCamera`;
- `docs: add summary to MapManager class and methods`;
- `refactor: move GameCamera scene to objects/`.

### Pull Requests
PRs must include a Summary, a list of Key Changes, and Testing Steps. Include a screenshot or GIF for any visual/UI changes.

All PRs must be reviewed by @andrewchokh before merging into `develop`.

PRs for the default branch (`main`) are done only when a Milestone is completed.

### Testing Policy
During the feature addition, it is crucial to make a separate test scene. The purpose of this scene is to test the implemented changes and an isolated enviroment, to ensure their stability and functionality.

In order for Testing Process to be counted as "successful", they should met the following criteria:
- Every new feature branch should include a corresponding `TestScene`;
- Test Scene runs without console errors or warnings;
- Implemented content should work as intended.

**Warning:** Do NOT make test scene a default scene to run in Godot Editor! The test scene will not be included in the final release. Therefore, they should not be used as a default scene to run the project.

## 5. 📚 Dependencies
- [.NET](https://dotnet.microsoft.com/en-us/download) 10.0.x
- [Godot .NET](https://godotengine.org/) 4.6.x;
- [GitHub](https://github.com) for version control;
- [Optional] [Aseprite](https://www.aseprite.org/) for pixel art.
