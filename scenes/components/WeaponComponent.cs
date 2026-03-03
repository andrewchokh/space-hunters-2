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
}
