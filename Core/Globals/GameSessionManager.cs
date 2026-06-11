using Godot;
using System;

public partial class GameSessionManager : Node
{
	public static GameSessionManager Instance { get; private set; }

	public int Wave = 1;
	public int Score { get; set; }
	public int DefeatedBosses;
	public float SessionTime;

    public override void _Ready() => Instance = this;

	public void Reset()
	{
		if (GameStateManager.Instance.State != GameState.GameOver)
			return;

		Wave = 1;
		Score = 0;
		DefeatedBosses = 0;
		SessionTime = 0;
	}
}
