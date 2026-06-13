using Godot;
using System;

public partial class Spaceship : CharacterBody2D
{
	[Export]
	public HealthComponent HealthComponent;
	[Export]
	public HitboxComponent HitboxComponent;
}
