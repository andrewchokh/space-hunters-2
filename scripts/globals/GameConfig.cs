using Godot;
using System;

public static class GameConfig
{
    public static readonly float[] FixedRows = new float[] { 2.0f, 1.0f, 0.0f, -1.0f, -2.0f };
    
    public static float GetRowY(int rowIndex)
    {
        if (rowIndex < 0) rowIndex = 0;
        if (rowIndex >= FixedRows.Length) rowIndex = FixedRows.Length - 1;

        return FixedRows[rowIndex];
    }
    
    public static float GetCenterY()
    {
        return (FixedRows[0] + FixedRows[FixedRows.Length - 1]) / 2.0f;
    }
}