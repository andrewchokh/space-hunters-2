using Godot;
using System;

/// <summary>
/// Manages the activation, duration, and cooldown states of an entity's ability.
/// </summary>
public partial class AbilityComponent : Node2D
{
    [Export]
    public AbilityPattern Ability;

    private Timer _durationTimer;
    private Timer _cooldownTimer;

    /// <summary>
    /// Dynamically creating required timers.
    /// </summary>
    public override void _Ready()
    {
        if(!Setup())
            return;

        _durationTimer = new Timer();
        _durationTimer.OneShot = true;
        AddChild(_durationTimer);

        _cooldownTimer = new Timer();
        _cooldownTimer.OneShot = true;
        AddChild(_cooldownTimer);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ability"))
            ActivateAbility();
    }

    /// <summary>
    /// Attempts to activate the ability, checking against current active duration and cooldown timers.
    /// </summary>
    private async void ActivateAbility()
    {
        if (_durationTimer.TimeLeft > 0)
        {
            this.DebugLog("Ability already activated!");
            return;
        }

        if (_cooldownTimer.TimeLeft > 0)
        {
            this.DebugLog($"Ability cooldown!!! ({Math.Round(_cooldownTimer.TimeLeft, 1)}s left)");
            return;
        }

        this.DebugLog("Ability activated!");
        _durationTimer.Start(Ability.Duration);

        await ToSignal(_durationTimer, Timer.SignalName.Timeout);

        this.DebugLog("Ability timed out!");
        _cooldownTimer.Start(Ability.Cooldown);
    }

    private bool Setup() => this.IsAssigned(Ability, nameof(Ability));
}
