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
        GameSessionManager.Instance.ScoreUpdated += OnScoreChanged;
    }

    /// <summary>
    /// Updates the label's text only when the score actually changes.
    /// </summary>
    private void OnScoreChanged(int newScore) => Text = newScore.ToString();

    /// <summary>
    /// Unsubscribes from the score update signal to prevent access to a disposed node after scene change.
    /// </summary>
    public override void _ExitTree() => GameSessionManager.Instance.ScoreUpdated -= OnScoreChanged;
}
