using Godot;
using System;

/// <summary>
/// Defines the data and timing parameters for a specific ability.
/// </summary>
[GlobalClass]
public abstract partial class AbilityPattern : Resource
{
    [Export]
    public double Duration = 3.0;
    [Export]
    public double Cooldown = 10.0;

    public abstract void Enter(CharacterBody2D entity);
    public abstract void Execute(CharacterBody2D entity, double delta);
    public abstract void Exit(CharacterBody2D entity);
}
