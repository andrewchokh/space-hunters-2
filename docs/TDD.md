# Technical Design Documentation - «Space Hunters 2»

## 📑 Project Structure
```
res://
├── assets/
│    ├── fonts/
│    ├── music/
│    ├── sounds/
│    └── sprites/
├── docs/
│    ├── GDD.md
│    └── TDD.md
├── recources/
│    └── tilesets/
├── scenes/
│    ├── components/
│    ├── objects/
│    ├── spaceships/
│    ├── test/
│    └── ui/
├── scripts/
│    ├── globals/
│    └── state_machine/
└── project.godot
```

### Directory `scenes`
#### Subdirectory `test`
The test scenes are used to test a feature in isolation or in pair with another. Every time you add new feature, you are obligated to create a test scene specifically for the feature. 

Note that Main scene will be defined only **after** prototyping phase (which means "until all feature are added").

## Coding Conventions

### Code Style
While writing the code, try your best to follow the [C# Style Guide](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_style_guide.html) from Godot's documentation. Also, follow these rules:
- use `snake_case` for directories;
- use `PascalCase` for scenes and scripts names;
- when creating test scene for a feature, name it: `TestScene[feature-name]`.

### Patterns

#### Component (Composition)
Composition pattern helps to make a certain mechanic reusable for all entities regardless of their class.
```cs
public partial class ExampleComponent : Node
{
    // Node of an entity to assign the component.
    // Change its type and name depending on context.
    // For example, if component is designed for the player:
    // public Player Player;
    [Export]
    public Node Entity;

    // Some code...
}
```

#### Singleton
Singleton pattern applies only for autoload scripts.
```cs
public partial class Example : Node
{
    // One and only instance of the class. 
    // Can be accessed across the project.
    public static Example Instance { get; private set; }

    // Some variables...

    public override void _Ready()
    {
        Instance = this;
    }

    // Some methods...
}
```

## Branching Workflow
[The Feature Branch](https://www.atlassian.com/git/tutorials/comparing-workflows/feature-branch-workflow) workflow is used for the repository. Ths workflow provides ease of adding new features to the project, while not being overwhelming for a small team of developers.

### Branch `main`
The `main` branch is used for stable and playable builds. Small or miscellaneous commits can be pushed straight into the branch. 

### Branch `develop`
The `develop` branch is used for feature addition and bug resolvement. The branch contains an unstable build of the game that the team is working on.

Before merging the branch into `main` branch, the developers should test the build properly: refine mechanics, fix bugs and optimize the code.

### Branches `feature/`
The `feature` branches are used for adding new content into the project. For example, if you want to add a new mechanic or an asset. These features should be small and consistent. Also, if the feature is connected to an issue directly, use issue number at the start of the name.

#### Examples
- `feature/[feature-name]`
- `feature/[issue-number]-[feature-name]`


## 📚 Dependencies
- [Godot .NET](https://godotengine.org/) 4.5.x;
- [GitHub](https://github.com) for version control;
- [Optional] [Aseprite](https://www.aseprite.org/) for pixel art.