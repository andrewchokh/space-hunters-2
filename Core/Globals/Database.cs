using Godot;
using System;
using System.Collections.Generic;

public partial class Database : Node
{
    public static Database Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }
}
