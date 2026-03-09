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
