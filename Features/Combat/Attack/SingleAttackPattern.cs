using Godot;
using System;

/// <summary>
/// A combat pattern that creates a single projectile at each of the weapon's designated spawn points.
/// </summary>
[GlobalClass]
public partial class SingleAttackPattern : AttackPattern
{
    /// <summary>
    /// The projectile prefab to be spawned when this pattern is executed.
    /// </summary>
    [Export]
    public PackedScene ProjectileScene;

    /// <summary>
    /// Instantiates the projectile at every shot point and sets its initial horizontal direction.
    /// </summary>
    /// <param name="weapon">The component providing the spawn locations and orientation.</param>
    public override void Execute(WeaponComponent weapon)
    {
        foreach (var shotPoint in weapon.ShotPoints) {
            var projectile = ProjectileScene.Instantiate<Projectile>();
            weapon.GetTree().CurrentScene.AddChild(projectile);

            projectile.GlobalPosition = shotPoint.GlobalPosition;
            projectile.SetHDirection(weapon.Direction.X);
        }
    }
}