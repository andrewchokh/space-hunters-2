using Godot;
using System;

public partial class ScoreLabel : Label
{

    public override void _Process(double delta)
    {
        Text = ScoreManager.Instance.Score.ToString();
    }
}
