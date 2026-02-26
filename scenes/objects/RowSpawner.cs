using Godot;
using System;

public partial class RowSpawner : Node2D
{
    [Export]
    public PackedScene Entity;
    [Export]
    public float OffsetX = 30.0f;

    public override void _Ready()
    {
        Timer timer = GetNode<Timer>("Timer");
        timer.Timeout += SpawnEntity;
    }

    private void SpawnEntity()
    {
        int rowCount = MapManager.Instance.FixedRows.Length;
        int randomRowIndex = GD.RandRange(0, rowCount - 1);

        var enemyInstance = Entity.Instantiate<CharacterBody2D>();
        enemyInstance.GlobalPosition = new Vector2(GetViewportRect().Size.X + OffsetX,
            MapManager.Instance.GetRowY(randomRowIndex));

        GetParent().AddChild(enemyInstance);
    }
}
