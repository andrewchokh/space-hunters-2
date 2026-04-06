using Godot;
using System;

/// <summary>
/// Manages the activation, duration, and cooldown states of an entity's ability.
/// </summary>
public partial class AbilityComponent : Node2D
{
    [Export]
    public CharacterBody2D Entity;
    [Export]
    public AbilityPattern Ability;

    private Timer _abilityTimer;
    private bool _isAbilityActive = false;

    public override void _Ready()
    {
        if(!Setup())
            return;

        _abilityTimer = new Timer();
        _abilityTimer.OneShot = true;
        AddChild(_abilityTimer);
    }

    public override void _Process(double delta) {
        if (_isAbilityActive)
            Ability.Execute(Entity, delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ability") && 
            !_isAbilityActive && _abilityTimer.TimeLeft > 0
        )
            ActivateAbility();
    }

    /// <summary>
    /// Activates the ability.
    /// </summary>
    private async void ActivateAbility()
    {
        _isAbilityActive = true;
        _abilityTimer.Start(Ability.Duration);

        Ability.Enter(Entity);

        this.DebugLog("Ability activated!");

        await ToSignal(_abilityTimer, Timer.SignalName.Timeout);

        _isAbilityActive = false;
        _abilityTimer.Start(Ability.Cooldown);

        Ability.Exit(Entity);

        this.DebugLog("Ability timed out!");
    }

    private bool Setup() => this.IsAssigned(Ability, nameof(Ability));
}
