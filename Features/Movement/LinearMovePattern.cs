using Godot;
using System;

/// <summary>
/// A movement pattern that moves an object in a straight line at a constant speed.
/// </summary>
[GlobalClass]
public partial class LinearMovePattern : MovePattern, IDirectable
{
    [Export]
    public float MoveSpeed = 45.0f;

    /// <summary>
    /// The specific direction the object will travel in.
    /// </summary>
    public Vector2 Direction { get; set; } = new Vector2(-1.0f, 0);

    /// <summary>
    /// Applies the constant velocity to the character body and handles physics-based movement.
    /// </summary>
    /// <param name="body">The physics body to be moved.</param>
    /// <param name="delta">The time elapsed since the last physics frame.</param>
    public override void Execute(CharacterBody2D body, double delta) 
    {
        // Normalizing the direction ensures the speed remains consistent even if the vector is diagonal.
        body.Velocity = Direction.Normalized() * MoveSpeed;
        body.MoveAndSlide();
    }
}