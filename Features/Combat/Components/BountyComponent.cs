using Godot;
using System;

/// <summary>
/// Grants a specified amount of score to the player when the attached entity is defeated.
/// </summary>
public partial class BountyComponent : Node2D, IComponent
{
    public Node2D Actor => GetParent() as Node2D;

    [Export]
    public int Bounty = 100;
    [Export]
    public HealthComponent HealthComponent;

    public override void _Ready()
    {
        HealthComponent.EntityDied += OnEntityDied;
    }
    
    /// <summary>
    /// Handles the EntityDied event by increasing the global score.
    /// </summary>
    private void OnEntityDied() => ScoreManager.Instance.Score += Bounty;
}
