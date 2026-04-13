using Godot;
using System;

[GlobalClass]
public partial class DebugAbilityData : AbilityData
{
    public override void Enter(CharacterBody2D actor) {
        GD.Print("DEBUG Ability activated!");
    }

    public override void Execute(CharacterBody2D actor, double delta) {
        GD.Print("DEBUG Ability processing!");
    }

    public override void Exit(CharacterBody2D actor) {
        GD.Print("DEBUG Ability ends!");
    }
}
