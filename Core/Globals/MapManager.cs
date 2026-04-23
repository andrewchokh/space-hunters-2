using Godot;
using System;

/// <summary>
/// A central controller that handles how objects are positioned on a specific grid or set of rows.
/// </summary>
public partial class MapManager : Node
{
    /// <summary>
    /// A quick way to access the MapManager from any other script without needing a direct reference.
    /// </summary>
    public static MapManager Instance { get; private set; }

    /// <summary>
    /// A list of pre-set vertical (Y) positions that objects can sit on.
    /// </summary>
    public readonly float[] FixedRows = new float[] { 50.0f, 25.0f, 0.0f, -25.0f, -50.0f };

    /// <summary>
    /// Sets up the Instance so other scripts can find this manager as soon as the game starts.
    /// </summary>
    public override void _Ready()
    {
        Instance = this;
    }

    /// <summary>
    /// Looks up the height (Y value) of a specific row by its number. 
    /// If the number is too high or too low, it just picks the closest available row.
    /// </summary>
    /// <param name="rowIndex">The number of the row you want to check.</param>
    /// <returns>The vertical position of that row.</returns>
    public float GetRowY(int rowIndex)
    {
        if (rowIndex < 0) rowIndex = 0;
        if (rowIndex >= FixedRows.Length) rowIndex = FixedRows.Length - 1;

        return FixedRows[rowIndex];
    }

    /// <summary>
    /// Finds the exact middle point between the very top row and the very bottom row.
    /// </summary>
    /// <returns>The middle height value.</returns>
    public float GetCenterY()
    {
        return (FixedRows[0] + FixedRows[FixedRows.Length - 1]) / 2.0f;
    }

    /// <summary>
    /// Takes a game object and force-moves its height to the row it is currently closest to.
    /// </summary>
    /// <param name="node">The 2D object you want to align to the grid.</param>
    public void SnapToRow(Node2D node)
    {
        if (node == null) return;

        float currentY = node.GlobalPosition.Y;
        float nearestY = FixedRows[0];
        float minDifference = Mathf.Abs(currentY - nearestY);

        for (int i = 1; i < FixedRows.Length; i++)
        {
            float diff = Mathf.Abs(currentY - FixedRows[i]);
            if (diff < minDifference)
            {
                minDifference = diff;
                nearestY = FixedRows[i];
            }
        }
        node.GlobalPosition = new Vector2(node.GlobalPosition.X, nearestY);
    }
}