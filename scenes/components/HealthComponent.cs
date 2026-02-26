using Godot;
using System;

public partial class HealthComponent : Node2D
{
    [Export]
    public int Health = 6;
    [Export]
    public int Protection = 1;

    public void TakeDamage(int damage) => Health -= Mathf.Max(1, damage - Protection);
}
