using Godot;
using System;
using System.Collections.Generic;

public partial class Database : Node
{
    public static Database Instance { get; private set; }

    [Export]
    public SpaceshipData[] Spaceships;

    private Dictionary<string, SpaceshipData> SpaceshipData;

    public override void _Ready()
    {
        Instance = this;
    }
}
