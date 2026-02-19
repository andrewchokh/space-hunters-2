using Godot;
using System;

public partial class MapManager : Node
{
	public static MapManager Instance { get; private set; }

	public readonly float[] FixedRows = new float[] { 2.0f, 1.0f, 0.0f, -1.0f, -2.0f };

	public override void _Ready()
	{
		Instance = this;
	}
	
	/// <summary>
	/// Retrieves the Y-coordinate for a given row index. The index is safely clamped to the available bounds.
	/// </summary>
	public float GetRowY(int rowIndex)
	{
		if (rowIndex < 0) rowIndex = 0;
		if (rowIndex >= FixedRows.Length) rowIndex = FixedRows.Length - 1;

		return FixedRows[rowIndex];
	}
	
	/// <summary>
	/// Calculates and returns the vertical center point of the map based on the highest and lowest rows.
	/// </summary>
	public float GetCenterY()
	{
		return (FixedRows[0] + FixedRows[FixedRows.Length - 1]) / 2.0f;
	}

	/// <summary>
	/// Finds the nearest row to the given node and snaps its Y-position to that row.
	/// </summary>
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
