using Godot;
using System;

/// <summary>
/// A specialized movement component that automatically executes its assigned MovePattern every physics frame.
/// </summary>
[GlobalClass]
public partial class EnemyMovementComponent : MovementComponent
{
    /// <summary>
    /// Drives the actor's movement by executing the movement pattern logic.
    /// </summary>
    /// <param name="delta">The time elapsed since the last physics frame.</param>
    public override void _PhysicsProcess(double delta) =>
        Pattern.Execute(Actor as CharacterBody2D, delta);
}