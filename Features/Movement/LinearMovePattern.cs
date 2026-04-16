using Godot;
using System;

[GlobalClass]
public partial class LinearMovePattern : MovePattern, IDirectable
{
    public Vector2 Direction { get; set; } = new Vector2(-1.0f, 0);

    public override void Execute(CharacterBody2D body, double delta) 
    {
        body.Velocity = Direction.Normalized() * MoveSpeed;
        body.MoveAndSlide();
    }
}
