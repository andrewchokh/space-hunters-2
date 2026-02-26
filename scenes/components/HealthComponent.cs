using Godot;
using System;

public partial class HealthComponent : Node2D
{
    [Signal]
    public delegate void HealthChangedEventHandler(int oldHealth, int newHealth);

    private int _health = 6;
    [Export]
    public int Health
    {
        get => _health;
        set
        {
            int oldHealth = _health;
            _health = Mathf.Max(0, value);

            EmitSignal(SignalName.HealthChanged, oldHealth, _health);
        }
    }
    [Export]
    public int Protection = 1;

    public void TakeDamage(int damage) => Health -= Mathf.Max(1, damage - Protection);
}
