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
public partial class WeaponComponent : Node2D
{
    [Export]
    public PackedScene BulletScene;

    /// <summary>
    /// Listens for the designated fire action and safely instantiates the projectile at the current global position.
    /// </summary>
    /// <param name="event">The captured input event used to match against the "fire" keybind.</param>
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("fire"))
        {
            var bullet = BulletScene.Instantiate<Bullet>();
            bullet.GlobalPosition = GlobalPosition;
            GetTree().CurrentScene.AddChild(bullet);
        }
    }

    /// <summary>
    /// Verifies that all required dependencies and exported fields are correctly assigned.
    /// </summary>
    /// <returns>True if the component is safe to initialize; false if a critical assignment is missing.</returns>
    private bool Setup()
    {
        if (!this.IsAssigned(BulletScene, nameof(BulletScene))) return false;
        return true;
    }
}
