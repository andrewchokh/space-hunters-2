using Godot;
using System;

/// <summary>
/// Data configuration specific to enemy spaceships, including rewards for the player.
/// </summary>
[GlobalClass]
public partial class EnemySpaceshipData : SpaceshipData
{
    /// <summary>
    /// The amount of score or currency awarded to the player when this enemy is defeated.
    /// </summary>
    [Export]
    public int Bounty = 100;
}