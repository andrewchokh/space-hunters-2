using Godot;
using System;

public partial class HitboxComponent : Area2D
{
    [Export]
    public HealthComponent HealthComponent;

    public override void _Ready()
    {
        if (HealthComponent == null)
        {
            GD.PushError($"{Name}: Component is not assigned!");
            return;
        }
    }

    public void Damage(int value) => HealthComponent?.TakeDamage(value);
}
