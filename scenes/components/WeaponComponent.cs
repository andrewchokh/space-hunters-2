using Godot;
using System;

public partial class WeaponComponent : Node2D
{
    [Export]
    public PackedScene BulletScene;

    public override void _Ready()
    {
        if (BulletScene == null)
        {
            GD.PushError($"{Name}: Component is not assigned!");
            return;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("fire"))
        {
            var bullet = BulletScene.Instantiate<Bullet>();
            bullet.GlobalPosition = GlobalPosition;
            GetTree().CurrentScene.AddChild(bullet);
        }
    }
}
