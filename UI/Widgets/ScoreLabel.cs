using Godot;
using System;

/// <summary>
/// A UI component that displays the player's current score on the screen.
/// </summary>
public partial class ScoreLabel : Label
{

    public override void _Ready()
    {
        Text = ScoreManager.Instance.Score.ToString();
        ScoreManager.Instance.ScoreChanged += OnScoreChanged;
    }

    /// <summary>
    /// Updates the label's text only when the score actually changes.
    /// </summary>
    private void OnScoreChanged(int newScore) => Text = newScore.ToString();
}
