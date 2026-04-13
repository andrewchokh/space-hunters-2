using Godot;
using System;

/// <summary>
/// A detection area that acts as a non-physical hit box for the entity.
/// </summary>
/// <remarks>
/// This component is responsible for registering incoming attacks (e.g., from projectiles)
/// and safely routing the damage values to the entity's dedicated HealthSystem.
/// Using a separate Area2D decouples combat triggers from the entity's physical collision.
/// </remarks>
[GlobalClass]
public partial class HitboxComponent : Area2D, IComponent
{
    public Node2D Actor => GetParent() as Node2D;

    [Export]
    public HealthComponent HealthComponent;

    /// <summary>
    /// Validates required dependencies and subscribes to collision events to handle fatal ship-to-ship collisions.
    /// </summary>
    /// <remarks>
    /// Listens for incoming areas and triggers a catastrophic damage response (999 damage)
    /// if both the current and colliding areas belong to root nodes flagged with the "Ship" global group.
    /// </remarks>
    public override void _Ready()
    {
        AreaEntered += (area) =>
        {
            if (area is not HitboxComponent)
                return;

            if (GetParent().IsInGroup("Ship") && area.GetParent().IsInGroup("Ship"))
                HealthComponent.TakeDamage(999);
        };
    }

    /// <summary>
    /// Receives damage from external sources and forwards it to the linked health system safely.
    /// </summary>
    /// <param name="value">The raw amount of damage to apply.</param>
    public void ReceiveDamage(int value) => HealthComponent?.TakeDamage(value);
}
