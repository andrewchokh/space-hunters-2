using Godot;
using System;

public partial class Bullet : Area2D
{
    [Export]
    public float Speed = 200.0f;

    public override void _PhysicsProcess(double delta) =>
        GlobalPosition += new Vector2(Speed * (float) delta, 0);
}
