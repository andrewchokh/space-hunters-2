using Godot;
using System;

/// <summary>
/// Represents a basic combat projectile that travels horizontally across the screen at a constant speed.
/// </summary>
/// <remarks>
/// Built on top of Area2D to serve as a lightweight combat trigger rather than a heavy physics body.
/// Movement is tied to the engine's physics step to ensure reliable hit registration without clipping through targets.
/// </remarks>
public partial class Bullet : Area2D
{
    [Export]
    public float Speed = 200.0f;

    public override void _PhysicsProcess(double delta) =>
        GlobalPosition += new Vector2(Speed * (float) delta, 0);
}
