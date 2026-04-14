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

    [Export]
    public AttackPattern Pattern;
    [Export]
    public float Cooldown = 0.5f;
    [Export]
    public Marker2D[] ShotPoints;
    [Export]
    public Vector2 Direction { get; set; } = new Vector2(1, 0);

    protected Timer CooldownTimer;
    public Node2D Actor => GetParent() as Node2D;

    public override void _Ready() {
        CooldownTimer = new Timer {
            OneShot = true,
            WaitTime = Cooldown
        };

        AddChild(CooldownTimer);
    }

    public override void _Process(double delta)
    {
        if (ShouldFire() && CooldownTimer.IsStopped())
        {
            Fire();
            CooldownTimer.Start();
        }
            
    }

    public abstract bool ShouldFire();

    public virtual void Fire() => Pattern.Execute(this);
}
