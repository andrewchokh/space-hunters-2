using Godot;
using System;

public partial class GameSessionManager : Node
{
	public static GameSessionManager Instance { get; private set; }

	[Signal]
	public delegate void WaveUpdatedEventHandler(int wave);
	[Signal]
	public delegate void DefeatedBossesUpdatedEventHandler(int defeatedBosses);
	[Signal]
	public delegate void ScoreUpdatedEventHandler(int score);
	[Signal]
	public delegate void SessionTimeUpdatedEventHandler(float sessionTime);

	private int _wave;
	public int Wave
	{
		get => _wave;
		set
		{
			_wave = value;
			EmitSignal(SignalName.WaveUpdated, _wave);
		}
	}

	private int _score;
	public int Score
	{
		get => _score;
		set
		{
			_score = value;
			EmitSignal(SignalName.ScoreUpdated, _score);
		}
	}

	private int _defeatedBosses;
	public int DefeatedBosses
	{
		get => _defeatedBosses;
		set
		{
			_defeatedBosses = value;
			EmitSignal(SignalName.DefeatedBossesUpdated, _defeatedBosses);
		}
	}

	private float _sessionTime;
	public float SessionTime
	{
		get => _sessionTime;
		set
		{
			_sessionTime = value;
			EmitSignal(SignalName.SessionTimeUpdated, _sessionTime);
		}
	}

	public override void _Ready() => Instance = this;

	public override void _Process(double delta)
	{
		if (GameStateManager.Instance.State != GameState.Playing)
			return;

		SessionTime += (float)delta;
	}

	public void Reset()
	{
		Wave = 0;
		Score = 0;
		DefeatedBosses = 0;
		SessionTime = 0;
	}
}
