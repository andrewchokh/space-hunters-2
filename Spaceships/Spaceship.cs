using Godot;
using System;

/// <summary>
/// Base class for all spaceships in the game, both player-controlled and enemy.
/// Provides shared exported components required by any ship entity.
/// </summary>
public partial class Spaceship : CharacterBody2D
{
	[Export]
	public HealthComponent HealthComponent;
	[Export]
	public HitboxComponent HitboxComponent;
}
