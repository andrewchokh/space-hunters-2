using Godot;
using System;

public partial class RowSpawner : Node2D
{
    public override void _Ready()
    {
        Timer timer = GetNode<Timer>("Timer");
        timer.Timeout += SpawnEntity;
    }

    private void SpawnEntity()
    {
        int randomRowIndex = GD.RandRange(0, 4);
    }
}
