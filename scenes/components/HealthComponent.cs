using Godot;
using System;

public partial class HealthComponent : Node2D
{
    [Signal]
    public delegate void HealthChangedEventHandler(int oldHealth, int newHealth);
    [Signal]
    public delegate void EntityDiedEventHandler();

    [Export]
    public CharacterBody2D Entity;

    [Export] public Timer Timer;
    private bool _isInvincible;

    private int _health = 6;
    [Export]
    public int Health
    {
        get => _health;
        private set
        {
            int oldHealth = _health;
            _health = Mathf.Max(0, value);

            EmitSignal(SignalName.HealthChanged, oldHealth, _health);

            if (_health <= 0)
                EmitSignal(SignalName.EntityDied);
        }
    }

    private int _protection = 1;
    [Export]
    public int Protection
    {
        get => _protection;
        private set => _protection = Mathf.Max(0, value);
    }

    public override void _Ready()
    {
        if (Entity == null)
        {
            GD.PushError($"{Name}: Entity is not assigned!");
            return;
        }

        Timer.Timeout += () => _isInvincible = false;

        EntityDied += Entity.QueueFree;
    }

    public void TakeDamage(int damage)
    {
        if (_isInvincible)
            return;

        Health -= Mathf.Max(1, damage - Protection);
        _isInvincible = true;
        Timer.Start();
    }
}
