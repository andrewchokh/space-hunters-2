using Godot;
using System;

/// <summary>
/// Grants a specified amount of score to the player when the attached entity is defeated.
/// </summary>
[GlobalClass]
public partial class BountyComponent : Node2D, IComponent
{
    public Node2D Actor => GetParent() as Node2D;

    [Export]
    public EnemySpaceshipData EnemySpaceshipData;
    [Export]
    public HealthComponent HealthComponent;

    public override void _Ready()
    {
        HealthComponent.ActorDied += () => {
            ScoreManager.Instance.Score += EnemySpaceshipData.Bounty;
        };
    }
}
