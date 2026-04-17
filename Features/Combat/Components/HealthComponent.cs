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
    public Node2D Actor => GetParent() as Node2D;
    
    /// <summary>
    /// Triggered whenever the health value changes. Useful for updating health bars or UI.
    /// </summary>
    [Signal]
    public delegate void HealthChangedEventHandler(int oldHealth, int newHealth);

    /// <summary>
    /// Triggered when health reaches zero.
    /// </summary>
    [Signal]
    public delegate void ActorDiedEventHandler();

    /// <summary>
    /// The data resource containing the base health and protection stats for this entity.
    /// </summary>
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
    /// Initializes health and protection from the data resource and prepares the death cleanup.
    /// </summary>
    public override void _Ready()
    {
        if (SpaceshipData == null) return; 
        
        _health = SpaceshipData.BaseHealth;
        _protection = SpaceshipData.BaseProtection;

        // Automatically removes the actor from the game world when it dies.
        ActorDied += Actor.QueueFree;
    }

    /// <summary>
    /// Handles reducing health based on incoming damage, armor, and temporary invincibility.
    /// </summary>
    /// <param name="damage">The raw amount of damage to inflict.</param>
    public async void TakeDamage(int damage)
    {
        if (_isInvincible) return;

        // Damage is reduced by protection, but will always deal at least 1 damage.
        Health -= Mathf.Max(1, damage - Protection);

        if (_health <= 0) return;

        if (!SpaceshipData.HasInvincibilityFrames) return;

        // Start temporary invincibility period.
        _isInvincible = true;
        await ToSignal(Actor.GetTree().CreateTimer(2.0f), SceneTreeTimer.SignalName.Timeout);
        _isInvincible = false;
    }
}