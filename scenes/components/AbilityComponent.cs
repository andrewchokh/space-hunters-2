using Godot;
using System;

public partial class AbilityComponent : Node2D
{
    [Export]
    public double AbilityTime = 3.0;
    [Export]
    public double Cooldown = 10.0;

    private Timer _timer;
    private Timer _cooldownTimer;

    public override void _Ready()
    {
        _timer = new Timer();
        _timer.OneShot = true;
        AddChild(_timer);

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
        _timer.Start(AbilityTime);

        await ToSignal(_timer, Timer.SignalName.Timeout);

        this.DebugLog("Ability timed out!");
        _cooldownTimer.Start(Cooldown);
    }
}
