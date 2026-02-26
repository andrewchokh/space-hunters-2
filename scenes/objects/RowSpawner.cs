using Godot;
using System;

/// <summary>
/// A factory node that periodically spawns entities into the game world at dynamic positions.
/// </summary>
/// <remarks>
/// This spawner reads the horizontal width of the viewport and specific vertical rows
/// from the MapManager to adaptively place entities just outside the visible screen area.
/// </remarks>
public partial class RowSpawner : Node2D
{
    [Export]
    public PackedScene Entity;
    [Export]
    public float OffsetX = 30.0f;

    /// <summary>
    /// subscribes to the spawn timer.
    /// </summary>
    public override void _Ready()
    {
        if (Entity == null)
        {
            GD.PushError($"{Name}: Entity is not assigned!");
            return;
        }

        if (GetNodeOrNull<Timer>("Timer") is Timer timer)
            timer.Timeout += SpawnEntity;
        else
        {
            GD.PushError($"{Name}: Timer node is missing!");
            return;
        }
    }

    /// <summary>
    /// Instantiates the entity, calculates its adaptive spawn coordinates, and adds it to the scene.
    /// </summary>
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
