using Godot;
using System;

public partial class AbilityComponent : Node2D
{
    [Export]
    public double Duration = 3.0;
    [Export]
    public double Cooldown = 10.0;

    private Timer _durationTimer;
    private Timer _cooldownTimer;

    public override void _Ready()
    {
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
            Ability();
    }

    private async void Ability()
    {
        if (_cooldownTimer.TimeLeft > 0)
        {
            this.DebugLog($"Ability cooldown!!! ({Math.Round(_cooldownTimer.TimeLeft, 1)}s left)");
            return;
        }

        this.DebugLog("Ability activated!");
        _durationTimer.Start(Duration);

        await ToSignal(_durationTimer, Timer.SignalName.Timeout);

        this.DebugLog("Ability timed out!");
        _cooldownTimer.Start(Cooldown);
    }
}
