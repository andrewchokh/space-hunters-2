using Godot;
using System;

/// <summary>
/// A specialized 2D camera that maintains a fixed vertical alignment.
/// Automatically centers itself on the map's rows at startup to ensure 
/// consistent framing of the gameplay area.
/// </summary>
public partial class GameCamera : Camera2D
{
    public override void _Ready()
    {
        CenterCameraOnRows();
    }
    
    /// <summary>
    /// Aligns the camera's vertical position to the exact center of the defined map rows.
    /// </summary>
    private void CenterCameraOnRows()
    {
        float centerY = MapManager.Instance.GetCenterY();
        
        GlobalPosition = new Vector2(GlobalPosition.X, centerY);
    }
}
