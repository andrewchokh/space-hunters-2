using Godot;
using System;

public partial class DebugAbility : AbilityPattern
{
    public override void Enter(CharacterBody2D entity) {
        GD.Print("[Debug Ability-kun] I am a debug ability! Nice to meet you!");
    }

    public override void Execute(CharacterBody2D entity, double delta) {
        GD.Print("[Debug Ability-kun] I am a talking ability, isn't it cool?");
    }

    public override void Exit(CharacterBody2D entity) {
        GD.Print("[Debug Ability-kun] Goodbye! See you soon!");
    }
}
