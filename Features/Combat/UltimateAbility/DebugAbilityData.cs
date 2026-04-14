using Godot;
using System;

[GlobalClass]
public partial class DebugAbilityData : AbilityData
{
    public override void Execute(CharacterBody2D actor, double delta) {
        GD.Print("DEBUG Ability processing!");
    }
}
