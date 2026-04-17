using Godot;
using System;

/// <summary>
/// Grants a specified amount of score to the player when the attached entity is defeated.
/// </summary>
[GlobalClass]
public partial class BountyComponent : Node2D, IComponent
{
    public Node2D Actor => GetParent() as Node2D;

    /// <summary>
    /// The data resource containing the score value (bounty) for this specific enemy.
    /// </summary>
    [Export]
    public EnemySpaceshipData EnemySpaceshipData;

    /// <summary>
    /// Reference to the health system used to detect when the entity dies.
    /// </summary>
    [Export]
    public HealthComponent HealthComponent;

    /// <summary>
    /// Connects the death event to the score manager to reward the player upon defeat.
    /// </summary>
    public override void _Ready()
    {
        HealthComponent.ActorDied += () => {
            ScoreManager.Instance.Score += EnemySpaceshipData.Bounty;
        };
    }
}