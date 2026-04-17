using Godot;
using System;

/// <summary>
/// A component responsible for spawning projectile scenes into the game world upon receiving a fire input.
/// </summary>
/// <remarks>
/// Operates independently of the entity's movement logic via composition.
/// It uses _UnhandledInput to avoid firing when interacting with UI elements,
/// and adds projectiles to the global scene tree so their trajectories remain decoupled from the shooter.
/// </remarks>
public abstract partial class WeaponComponent : Node2D, IComponent, IDirectable
{
    public Node2D Actor => GetParent() as Node2D;
    
    /// <summary>
    /// The attack behavior (like single shot or burst) this weapon uses.
    /// </summary>
    [Export]
    public AttackPattern Pattern;

    /// <summary>
    /// The delay in seconds between each shot.
    /// </summary>
    [Export]
    public float Cooldown = 0.5f;

    /// <summary>
    /// The specific locations on the weapon where projectiles will appear.
    /// </summary>
    [Export]
    public Marker2D[] ShotPoints;

    /// <summary>
    /// The current direction the weapon is aiming.
    /// </summary>
    [Export]
    public Vector2 Direction { get; set; } = new Vector2(1, 0);

    protected Timer CooldownTimer;

    /// <summary>
    /// Configures the internal cooldown timer based on the exported Cooldown value.
    /// </summary>
    public override void _Ready() {
        CooldownTimer = new Timer {
            OneShot = true,
            WaitTime = Cooldown
        };

        AddChild(CooldownTimer);
    }

    /// <summary>
    /// Checks every frame if the weapon should fire and if the cooldown has finished.
    /// </summary>
    public override void _Process(double delta)
    {
        if (ShouldFire() && CooldownTimer.IsStopped())
        {
            Fire();
            CooldownTimer.Start();
        }
    }

    /// <summary>
    /// Defines the specific condition (like a button press or AI trigger) required to shoot.
    /// </summary>
    /// <returns>True if the weapon wants to fire.</returns>
    public abstract bool ShouldFire();

    /// <summary>
    /// Triggers the assigned AttackPattern logic.
    /// </summary>
    public virtual void Fire() => Pattern.Execute(this);
}