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
    public PackedScene ProjectileScene;
    [Export]
    public float Speed = 10.0f;
    [Export]
    public float DirectionX = -1.0f;

    private RandomNumberGenerator _shootTimer = new RandomNumberGenerator();
    private double _cooldownTimer = 0;
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

        _cooldownTimer -= delta;

        if(_cooldownTimer > 0)
            return;

        Shoot(entity);

        _cooldownTimer = _shootTimer.RandfRange(3.0f, 5.0f);
    }

    private void Shoot(CharacterBody2D entity)
    {
        var projectile = ProjectileScene.Instantiate<Projectile>();
        projectile.GlobalPosition = entity.GlobalPosition;
        entity.GetTree().CurrentScene.AddChild(projectile);
    }
}
