using Godot;
using System;

/// <summary>
/// An AI pattern that forces the entity to move constantly along a horizontal axis.
/// </summary>
/// <remarks>
/// The Y-axis velocity is intentionally zeroed out within the execution logic
/// to ensure the entity strictly stays on its initial spawn row without drifting.
/// </remarks>
[GlobalClass]
public partial class LinearPattern : AIPattern
{
    [Export]
    public float Speed = 10.0f;
    [Export]
    public float DirectionX = -1.0f;

    /// <summary>
    /// Calculates the horizontal velocity and applies it to the entity using MoveAndSlide.
    /// </summary>
    /// <param name="entity">The entity to move.</param>
    /// <param name="delta">The physics process delta time.</param>
    public override void Execute(CharacterBody2D entity, double delta)
    {
        var direction = new Vector2(DirectionX, 0.0f);
        entity.Velocity = direction.Normalized() * Speed;
        entity.MoveAndSlide();
    }
}
