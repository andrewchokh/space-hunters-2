using Godot;
using System;

/// <summary>
/// Manages the global score for the player throughout the game session.
/// </summary>
public partial class ScoreManager : Node
{
    public static ScoreManager Instance { get; private set; }

    [Signal]
    public delegate void ScoreChangedEventHandler(int newScore);

    private int _score;

    public override void _Ready()
    {
        Instance = this;
    }

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
        get => _score;
        set
        {
            if (value <= _score)
                return;

            _score = value;
            EmitSignal(SignalName.ScoreChanged, _score);
            this.DebugLog("New Score: " + Score);
        }
    }
}
