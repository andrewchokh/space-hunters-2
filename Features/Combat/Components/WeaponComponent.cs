using Godot;
using System;

/// <summary>
/// A component responsible for spawning projectile scenes into the game world upon receiving a fire input.
/// </summary>
/// <remarks>
/// Operates independently of the entity's movement logic via composition.
/// It uses _UnhandledInput to avoid firing when interacting with UI elements,
/// and adds projectiles to the global scene tree so their trajectories remain decoupled from the shooter.
/// </remarks>
public partial class WeaponComponent : Node2D, IComponent
{
    public Node2D Actor => GetParent() as Node2D;

    [Export]
    public PackedScene ProjectileScene;

    /// <summary>
    /// Listens for the designated fire action and safely instantiates the projectile at the current global position.
    /// </summary>
    /// <param name="event">The captured input event used to match against the "fire" keybind.</param>
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("fire"))
        {
            var projectile = ProjectileScene.Instantiate<Projectile>();
            projectile.GlobalPosition = GlobalPosition;
            GetTree().CurrentScene.AddChild(projectile);
        }
    }
}
