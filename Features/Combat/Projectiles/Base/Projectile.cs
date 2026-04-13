using Godot;
using System;

/// <summary>
/// Represents a basic combat projectile that travels horizontally across the screen at a constant speed.
/// </summary>
/// <remarks>
/// Built on top of Area2D to serve as a lightweight combat trigger rather than a heavy physics body.
/// Movement is tied to the engine's physics step to ensure reliable hit registration without clipping through targets.
/// </remarks>
public abstract partial class Projectile : Area2D, IIdentifiable, IHDirectable
{
    [ExportGroup("Identification")]
    [Export]
    public string ID { get; set; }

    [ExportGroup("Data")]
    [Export]
    public SpaceshipData SpaceshipData;
    [Export]
    public HDirection HorizontalDirection { get; set; } = HDirection.Right;

    private int _damage;

    public abstract void Enter(Projectile projectile);
    public abstract void Execute(Projectile projectile, double delta);
    public abstract void Exit(Projectile projectile);

    public override void _Ready()
    {
        _damage = SpaceshipData.ProjectileDamage;

        Enter(this);

        AreaEntered += OnAreaEntered;
    }

    public override void _PhysicsProcess(double delta) {
        Execute(this, delta);
    }

    /// <summary>
    /// Handles the collision event when the projectile enters another valid detection area.
    /// </summary>
    /// <param name="area">The detecting Area2D node that this projectile has physics-overlapped with.</param>
    /// <remarks>
    /// Employs an early return pattern to safely ignore non-hitbox triggers. Upon validating a target hitbox,
    /// it applies its designated damage and immediately destroys the projectile object to prevent phantom hits.
    /// </remarks>
    private void OnAreaEntered(Area2D area)
    {
        if (area is not HitboxComponent hitbox)
            return;

        Exit(this);

        hitbox.ReceiveDamage(_damage);
        QueueFree();
    }
}
