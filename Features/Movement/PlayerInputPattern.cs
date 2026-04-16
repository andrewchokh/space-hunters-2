using Godot;
using System;

[GlobalClass]
public partial class PlayerInputPattern : MovePattern
{
    public int RowIndex;
    public float TargetY;

    public override void Execute(CharacterBody2D body, double delta) 
    {
        var newY = Mathf.Lerp(body.GlobalPosition.Y, TargetY, Mathf.Min(MoveSpeed * (float)delta, 1.0f));
        body.GlobalPosition = new Vector2(body.GlobalPosition.X, newY);

        body.MoveAndSlide();
    }

    public void SetupPlayerPosition(Node2D actor) 
    {
        var mapManager = MapManager.Instance; 

        RowIndex = (int)Mathf.Floor(mapManager.FixedRows.Length / 2);
        TargetY = MapManager.Instance.GetRowY(RowIndex);

        actor.GlobalPosition = new Vector2(actor.GlobalPosition.X, TargetY);
    }
}
