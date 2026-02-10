using Godot;
using System;

[Tool]
public partial class RowSnapper : Node2D
{ 
    private int _rowIndex = 2;

    [Export(PropertyHint.Range, "0,4")]
    public int RowIndex
    {
        get => _rowIndex;
        set
        {
            _rowIndex = value;
            SnapParentToRow(); 
        }
    }

    public override void _Ready()
    {
        if (!Engine.IsEditorHint()) 
        {
            SnapParentToRow(); 
            QueueFree();       
        }
    }

    public void SnapParentToRow()
    {
        var parent = GetParent() as Node2D;
        
        if (parent == null) return;

        float targetY = GameConfig.GetRowY(_rowIndex);
        
        parent.GlobalPosition = new Vector2(parent.GlobalPosition.X, targetY);
    }
}