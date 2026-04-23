using Godot;
using System;

/// <summary>
/// A movement pattern that handles smooth transitions between horizontal lanes for the player.
/// </summary>
[GlobalClass]
public partial class PlayerInputPattern : MovePattern
{
    /// <summary>
    /// A convenient shortcut to access the global MapManager instance.
    /// </summary>
    public MapManager MapInstance => MapManager.Instance;

    /// <summary>
    /// The current row number the player is located in or moving toward.
    /// </summary>
    public int RowIndex;

    /// <summary>
    /// The target vertical (Y) position corresponding to the current RowIndex.
    /// </summary>
    public float TargetY;

    /// <summary>
    /// Smoothly slides the player's Y-position toward the TargetY using linear interpolation.
    /// </summary>
    /// <param name="body">The player's physics body.</param>
    /// <param name="delta">The time elapsed since the last physics frame.</param>
    public override void Execute(CharacterBody2D body, double delta) 
    {
        // Smoothly calculates the next step toward the target row height.
        var newY = Mathf.Lerp(body.GlobalPosition.Y, TargetY, Mathf.Min(MoveSpeed * (float)delta, 1.0f));
        body.GlobalPosition = new Vector2(body.GlobalPosition.X, newY);

        body.MoveAndSlide();
    }

    /// <summary>
    /// Centers the player on the map and synchronizes their initial position with the middle row.
    /// </summary>
    /// <param name="actor">The player object to be positioned.</param>
    public void SetupPlayerPosition(Node2D actor) 
    {
        RowIndex = (int)Mathf.Floor(MapInstance.FixedRows.Length / 2);
        TargetY = MapManager.Instance.GetRowY(RowIndex);

        actor.GlobalPosition = new Vector2(actor.GlobalPosition.X, TargetY);
    }
}