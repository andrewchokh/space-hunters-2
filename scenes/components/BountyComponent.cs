using Godot;
using System;

public partial class BountyComponent : Node2D
{
   [Export]
   public int ScoreForKill = 100;
   [Export]
   public HealthComponent HealthComponent;

   public override void _Ready()
   {
       Setup();
       HealthComponent.EntityDied += OnEntityDied;
   }

   private bool Setup() => this.IsAssigned(HealthComponent, nameof(HealthComponent));

   private void OnEntityDied() => ScoreManager.Instance.Score += ScoreForKill;
}
