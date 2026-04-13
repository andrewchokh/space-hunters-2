using Godot;
using System;

/// <summary>
/// Component responsible for executing AI behaviors on a designated physical entity.
/// </summary>
/// <remarks>
/// This uses the Strategy design pattern. By assigning different Resource-based AIPatterns
/// in the Godot Inspector, the entity's behavior can be changed dynamically without editing this script.
/// </remarks>
[GlobalClass]
public partial class AIComponent : Node2D, IComponent
{
    public Node2D Actor => GetParent() as Node2D;

    [Export]
    public AIPattern Pattern;

    /// <summary>
    /// Executes the assigned pattern's logic every physics frame.
    /// </summary>
    /// <param name="delta">The elapsed time since the previous physics frame.</param>
    public override void _PhysicsProcess(double delta) 
    {
        if (Pattern == null) return;

        Pattern.Execute(Actor as CharacterBody2D, delta);
    }
}
