using Godot;
using System;

/// <summary>
/// Manages the global score for the player throughout the game session.
/// </summary>
public partial class ScoreManager : Node
{
    /// <summary>
    /// A quick way for other scripts to access the ScoreManager without searching the scene tree.
    /// </summary>
    public static ScoreManager Instance { get; private set; }

    /// <summary>
    /// Gets or sets the player's current score.
    /// </summary>
    /// <value>The total score accumulated by the player.</value>
    /// <remarks>
    /// The score logic is strictly increasing. If a newly assigned value is less than
    /// or equal to the current score, the assignment is ignored. This prevents accidental
    /// score deductions during standard gameplay.
    /// </remarks>
    public int Score
    {
        get => GameSessionManager.Instance.Score;
        set
        {
            if (value <= GameSessionManager.Instance.Score)
                return;

            GameSessionManager.Instance.Score = value;
            this.DebugLog("New Score: " + Score);
        }
    }

    /// <summary>
    /// Sets up the Instance when the game starts so the score can be tracked.
    /// </summary>
    public override void _Ready()
    {
        Instance = this;
    }
}