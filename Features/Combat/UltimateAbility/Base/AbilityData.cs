using Godot;
using System;

[GlobalClass]
public abstract partial class AbilityData : Resource, IIdentifiable
{
    [ExportCategory("Identification")]
    [Export]
    public string ID { get; set; }

    [Export]
    public float Duration = 5.0f;
    [Export]
    public float Cooldown = 5.0f;

    

    public virtual void Enter(CharacterBody2D actor) =>
        actor.DebugLog($"\"{actor.Name}\" activated its ultimate ability!");

    public virtual void Execute(CharacterBody2D actor, double delta) =>
        actor.DebugLog($"\"{actor.Name}\"'s ability is running!");

    public virtual void Exit(CharacterBody2D actor) =>
        actor.DebugLog($"The ultimate ability of \"{actor.Name}\" finished!");
}
