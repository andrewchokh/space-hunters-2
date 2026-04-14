using Godot;
using System;

[GlobalClass]
public partial class SingleAttackPattern : AttackPattern
{
    [Export]
    public PackedScene ProjectileScene;

    public override void Execute(WeaponComponent weapon)
    {
        foreach (var shotPoint in weapon.ShotPoints) {
            var projectile = ProjectileScene.Instantiate<Projectile>();
            projectile.GlobalPosition = shotPoint.GlobalPosition;
            weapon.GetTree().CurrentScene.AddChild(projectile);

            projectile.SetHDirection(weapon.Direction.X);
        }
    }
}
