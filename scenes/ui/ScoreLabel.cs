using Godot;
using System;

/// <summary>
/// A UI component that displays the player's current score on the screen.
/// </summary>
public partial class ScoreLabel : Label
{
    /// <summary>
    /// Updates the label's text every frame with the latest value from the ScoreManager.
    /// </summary>
    public override void _Process(double delta)
    {
        Text = ScoreManager.Instance.Score.ToString();
    }
}
