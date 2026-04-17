using Godot;
using System;

/// <summary>
/// Manages the activation, duration, and cooldown states of an entity's ability.
/// </summary>
[GlobalClass]
public partial class AbilityComponent : Node2D, IComponent
{
    public Node2D Actor => GetParent() as Node2D;

    /// <summary>
    /// The configuration data and logic for the specific ability.
    /// </summary>
    [Export]
    public AbilityData Data;

    private Timer _abilityTimer;
    private bool _isAbilityActive = false;

    /// <summary>
    /// Sets up the internal timer for managing ability timing.
    /// </summary>
    public override void _Ready()
    {
        _abilityTimer = new Timer();
        _abilityTimer.OneShot = true;
        AddChild(_abilityTimer);
    }

    /// <summary>
    /// Runs the active logic of the ability every physics frame.
    /// </summary>
    public override void _PhysicsProcess(double delta) {
        if (Engine.IsEditorHint()) return;

        if (!_isAbilityActive) return;

        Data.Execute(Actor as CharacterBody2D, delta);
    }

    /// <summary>
    /// Checks for player input to trigger the ability.
    /// </summary>
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ability") && 
            !_isAbilityActive && _abilityTimer.IsStopped()
        )
            ActivateAbility();
    }

    /// <summary>
    /// Processes the ability lifecycle: activation, duration, and the cooldown wait.
    /// </summary>
    private async void ActivateAbility()
    {
        _isAbilityActive = true;
        _abilityTimer.Start(Data.Duration);

        Data.Enter(Actor as CharacterBody2D);

        this.DebugLog("Ability activated!");

        await ToSignal(_abilityTimer, Timer.SignalName.Timeout);

        _isAbilityActive = false;
        _abilityTimer.Start(Data.Cooldown);

        Data.Exit(Actor as CharacterBody2D);

        this.DebugLog("Ability timed out!");

        await ToSignal(_abilityTimer, Timer.SignalName.Timeout);

        this.DebugLog("Ability is recharged!");
    }
}