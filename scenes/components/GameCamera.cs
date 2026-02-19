using Godot;
using System;

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
        float centerY = MapManager.GetCenterY();
        
        GlobalPosition = new Vector2(GlobalPosition.X, centerY);
    }
}
