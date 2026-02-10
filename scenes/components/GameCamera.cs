using Godot;
using System;

public partial class GameCamera : Camera2D
{
    public override void _Ready()
    {
        CenterCameraOnRows();
    }
    
    private void CenterCameraOnRows()
    {
        float centerY = GameConfig.GetCenterY();
        
        GlobalPosition = new Vector2(GlobalPosition.X, centerY);
    }
}
