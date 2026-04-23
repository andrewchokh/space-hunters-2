using Godot;
using System;

/// <summary>
/// A weapon component for AI that introduces randomized delays between shots to vary firing rhythm.
/// </summary>
[GlobalClass]
public partial class EnemyWeaponComponent : WeaponComponent
{
    [ExportGroup("Delay Time")]
    /// <summary>
    /// The minimum randomized wait time before the weapon can fire again after a cooldown.
    /// </summary>
    [Export]
    public float MinDelayTime = 1.0f;

    /// <summary>
    /// The maximum randomized wait time before the weapon can fire again after a cooldown.
    /// </summary>
    [Export]
    public float MaxDelayTime = 3.0f;


    private RandomNumberGenerator _random = new RandomNumberGenerator();
    private Timer _delayTimer;
    private bool _isDelayApplied = false;

    /// <summary>
    /// Initializes the base weapon cooldown and sets up the internal randomization timer.
    /// </summary>
    public override void _Ready() 
    {
        base._Ready();

        _delayTimer = new Timer {
            OneShot = true,
            Autostart = false
        };

        AddChild(_delayTimer);
    }

    /// <summary>
    /// Determines if the enemy is ready to fire based on both the standard cooldown and the random delay.
    /// </summary>
    /// <returns>True if both the weapon cooldown and the randomized delay have finished.</returns>
    public override bool ShouldFire() 
    {
        if (!CooldownTimer.IsStopped()) return false;

        // If the weapon is off cooldown but hasn't started its random delay yet, trigger it.
        if (_delayTimer.IsStopped() && !_isDelayApplied) {
            StartDelay();
            _isDelayApplied = true;
            return false;
        }
        
        // Block firing while the random delay timer is still ticking.
        if (!_delayTimer.IsStopped() && _isDelayApplied) return false;
        
        _isDelayApplied = false;
        return true;
    }

    /// <summary>
    /// Starts the timer with a random duration picked from the exported range.
    /// </summary>
    private async void StartDelay() 
    {
        _delayTimer.Start(_random.RandfRange(MinDelayTime, MaxDelayTime));
        await ToSignal(_delayTimer, Timer.SignalName.Timeout);
    }
}