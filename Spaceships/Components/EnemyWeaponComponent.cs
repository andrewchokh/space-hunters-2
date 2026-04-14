using Godot;
using System;

[GlobalClass]
public partial class EnemyWeaponComponent : WeaponComponent
{
    [ExportGroup("Delay Time")]
    [Export]
    public float MinDelayTime = 1.0f;
    [Export]
    public float MaxDelayTime = 3.0f;


    private RandomNumberGenerator _random = new RandomNumberGenerator();
    private Timer _delayTimer;
    private bool _isDelayApplied = false;

    public override void _Ready() 
    {
        base._Ready();

        _delayTimer = new Timer {
            OneShot = true,
            Autostart = false
        };

        AddChild(_delayTimer);
    }

    public override bool ShouldFire() 
    {
        if (!CooldownTimer.IsStopped()) return false;

        if (_delayTimer.IsStopped() && !_isDelayApplied) {
            StartDelay();
            _isDelayApplied = true;
            return false;
        }
        
        if (!_delayTimer.IsStopped() && _isDelayApplied) return false;
        
        _isDelayApplied = false;
        return true;
    }

    private async void StartDelay() 
    {
        _delayTimer.Start(_random.RandfRange(MinDelayTime, MaxDelayTime));
        await ToSignal(_delayTimer, Timer.SignalName.Timeout);
    }
}
