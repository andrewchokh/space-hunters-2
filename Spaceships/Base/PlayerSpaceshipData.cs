using Godot;
using System;

/// <summary>
/// Data configuration specific to the player's spaceship, including specialized movement tuning.
/// </summary>
[GlobalClass]
public partial class PlayerSpaceshipData : SpaceshipData
{
    /// <summary>
    /// The speed at which the player's ship transitions between horizontal lanes.
    /// </summary>
    [ExportGroup("Movement")]
    [Export]
    public float RowMoveSpeed = 15;
}