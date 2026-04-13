using Godot;
using System;

/// <summary>
/// Manages the activation, duration, and cooldown states of an entity's ability.
/// </summary>
public partial class AbilityComponent : Node2D, IComponent
{
    public Node2D Actor => GetParent() as Node2D;

    [Export]
    public AbilityPattern AbilityData;

    private Timer _abilityTimer;
    private bool _isAbilityActive = false;

    public override void _Ready()
    {
        _abilityTimer = new Timer();
        _abilityTimer.OneShot = true;
        AddChild(_abilityTimer);
    }

    public override void _PhysicsProcess(double delta) {
        if (!_isAbilityActive) return;

        AbilityData.Execute(Actor as CharacterBody2D, delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ability") && 
            !_isAbilityActive && _abilityTimer.TimeLeft == 0
        )
            ActivateAbility();
    }

    public override string[] _GetConfigurationWarnings()
    {
        if (GetParent() is not CharacterBody2D)
            return new string[] { "This component requires a CharacterBody2D parent to execute physics patterns." };
            
        return base._GetConfigurationWarnings();
    }

    /// <summary>
    /// Activates the ability.
    /// </summary>
    private async void ActivateAbility()
    {
        _isAbilityActive = true;
        _abilityTimer.Start(AbilityData.Duration);

        AbilityData.Enter(Actor as CharacterBody2D);

        this.DebugLog("Ability activated!");

        await ToSignal(_abilityTimer, Timer.SignalName.Timeout);

        _isAbilityActive = false;
        _abilityTimer.Start(AbilityData.Cooldown);

        AbilityData.Exit(Actor as CharacterBody2D);

        this.DebugLog("Ability timed out!");

        await ToSignal(_abilityTimer, Timer.SignalName.Timeout);

        this.DebugLog("Ability is recharged!");
    }
}
