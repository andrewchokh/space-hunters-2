using Godot;
using System;

/// <summary>
/// Component responsible for executing AI behaviors on a designated physical entity.
/// </summary>
/// <remarks>
/// This uses the Strategy design pattern. By assigning different Resource-based AIPatterns
/// in the Godot Inspector, the entity's behavior can be changed dynamically without editing this script.
/// </remarks>
public partial class AIComponent : Node
{
    [Export]
    public CharacterBody2D Entity;
    [Export]
    public AIPattern Pattern;

    public override void _Ready()
    {
        if (!Setup()) return;
    }

    /// <summary>
    /// Executes the assigned pattern's logic every physics frame.
    /// </summary>
    /// <param name="delta">The elapsed time since the previous physics frame.</param>
    public override void _PhysicsProcess(double delta) => Pattern.Execute(Entity, delta);

    /// <summary>
    /// Verifies that all required dependencies and exported fields are correctly assigned.
    /// </summary>
    /// <returns>True if the component is safe to initialize; false if a critical assignment is missing.</returns>
    private bool Setup()
    {
        if (!this.IsAssigned(Entity, nameof(Entity))) return false;
        if (!this.IsAssigned(Pattern, nameof(Pattern))) return false;
        return true;
    }
}
