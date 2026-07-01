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

    private int _tier = 1;

    /// <summary>
    /// The power tier of the enemy. 0 is reserved for bosses, while 1 to 3 represent regular enemies from weakest to strongest.
    /// </summary>
    [Export(PropertyHint.Range, "0,3")]
    public int Tier
    {
        get => _tier;
        set
        {
            if (value < 0 || value > 3)
                return;

            _tier = value;
        }
    }
}