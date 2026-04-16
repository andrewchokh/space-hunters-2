using Godot;
using System;

[GlobalClass]
public partial class EnemyMovementComponent : MovementComponent
{
    public override void _PhysicsProcess(double delta) 
    {
        Pattern.Execute(Actor as CharacterBody2D, delta);
    }
}
