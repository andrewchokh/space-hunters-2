using Godot;
using System;

[GlobalClass]
public partial class PlayerSpaceshipData : SpaceshipData
{
    [ExportGroup("Movement")]
    [Export]
    public float RowMoveSpeed = 15;
}
