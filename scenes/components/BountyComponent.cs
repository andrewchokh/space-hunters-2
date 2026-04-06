using Godot;
using System;

/// <summary>
/// Grants a specified amount of score to the player when the attached entity is defeated.
/// </summary>
public partial class BountyComponent : Node2D
{
   [Export]
   public int Bounty = 100;
   [Export]
   public HealthComponent HealthComponent;

   public override void _Ready()
   {
       if (!Setup()) return;

       HealthComponent.EntityDied += OnEntityDied;
   }

   private bool Setup() => this.IsAssigned(HealthComponent, nameof(HealthComponent));

   /// <summary>
   /// Handles the EntityDied event by increasing the global score.
   /// </summary>
   private void OnEntityDied() => ScoreManager.Instance.Score += ScoreForKill;
}
