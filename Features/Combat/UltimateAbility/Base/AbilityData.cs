using Godot;
using System;

[GlobalClass]
public abstract partial class AbilityData : Resource
{
    [Export]
    public int Duration = 5;
    [Export]
    public int Cooldown = 5;

    public abstract void Enter(CharacterBody2D actor);
    public abstract void Execute(CharacterBody2D actor, double delta);
    public abstract void Exit(CharacterBody2D actor);
}
