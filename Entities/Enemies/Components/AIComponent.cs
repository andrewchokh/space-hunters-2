using Godot;
using System;

/// <summary>
/// Component responsible for executing AI behaviors on a designated physical entity.
/// </summary>
/// <remarks>
/// This uses the Strategy design pattern. By assigning different Resource-based AIPatterns
/// in the Godot Inspector, the entity's behavior can be changed dynamically without editing this script.
/// </remarks>
[Tool]
[GlobalClass]
public partial class AIComponent : Node2D, IComponent
{
    public Node2D Actor => GetParent() as Node2D;

    [Export]
    public AIPattern Pattern;

    public override string[] _GetConfigurationWarnings()
    {
        if (GetParent() is not CharacterBody2D)
            return new string[] { "This component requires a CharacterBody2D parent to execute physics patterns." };
            
        return base._GetConfigurationWarnings();
    }

    /// <summary>
    /// Executes the assigned pattern's logic every physics frame.
    /// </summary>
    /// <param name="delta">The elapsed time since the previous physics frame.</param>
    public override void _PhysicsProcess(double delta) => Pattern.Execute(Actor as CharacterBody2D, delta);
}
