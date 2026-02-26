using Godot;
using System;

public partial class RowSpawner : Node2D
{
    [Export]
    public PackedScene Entity;

    public override void _Ready()
    {
        if (Entity == null)
        {
            GD.PushError($"{Name}: Entity is not assigned!");
            return;
        }
    }
}
