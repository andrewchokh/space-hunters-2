using Godot;
using System;

/// <summary>
/// A reusable component that manages the health, protection, and invincibility of an entity.
/// </summary>
/// <remarks>
/// This component uses composition. It should be attached to an entity (like a player or enemy)
/// as a child node. It tracks health, applies damage reduction via protection, triggers
/// a temporary invincibility period upon taking damage, and destroys the entity when health reaches zero.
/// </remarks>
[GlobalClass]
public partial class HealthComponent : Node2D, IComponent
{
    [Signal]
    public delegate void HealthChangedEventHandler(int oldHealth, int newHealth);
    [Signal]
    public delegate void ActorDiedEventHandler();

    public Node2D Actor => GetParent() as Node2D;

    [Export]
    public SpaceshipData SpaceshipData;

    private int _health;
    private int _protection;
    private bool _isInvincible = false;

    /// <summary>
    /// The current health of the entity. Cannot be modified directly from outside; use TakeDamage instead.
    /// </summary>
    public int Health
    {
        get => _health;
        private set
        {
            int oldHealth = _health;
            _health = Mathf.Max(0, value);

            EmitSignal(SignalName.HealthChanged, oldHealth, _health);

            if (_health <= 0)
                EmitSignal(SignalName.ActorDied);
        }
    }

    /// <summary>
    /// The damage reduction value. This amount is subtracted from any incoming damage.
    /// </summary>
    public int Protection
    {
        get => _protection;
        private set => _protection = Mathf.Max(0, value);
    }

    /// <summary>
    /// Subscribes to necessary signals for entity death.
    /// </summary>
    public override void _Ready()
    {
        if (SpaceshipData == null) return; 
        
        _health = SpaceshipData.BaseHealth;
        _protection = SpaceshipData.BaseProtection;

        ActorDied += Actor.QueueFree;
    }

    /// <summary>
    /// Applies damage to the entity, factoring in protection and invincibility frames.
    /// </summary>
    /// <param name="damage">The raw amount of damage to inflict.</param>
    public async void TakeDamage(int damage)
    {
        if (_isInvincible) return;

        Health -= Mathf.Max(1, damage - Protection);

        if (_health <= 0)
            return;

        if (!SpaceshipData.HasInvincibilityFrames) return;

        _isInvincible = true;
        await ToSignal(Actor.GetTree().CreateTimer(2.0f), SceneTreeTimer.SignalName.Timeout);
        _isInvincible = false;
    }
}
