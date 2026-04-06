using Godot;
using System;

public partial class ScoreManager : Node
{
    public static ScoreManager Instance { get; private set; }

    private int _score;

    public override void _Ready()
    {
        Instance = this;
    }

    public int Score
    {
        get => _score;
        set
        {
            if (value <= _score)
            {
                return;
            }
            _score = value;
            this.DebugLog("New Score: " + Score);
        }
    }
}
