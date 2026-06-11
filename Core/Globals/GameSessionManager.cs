using Godot;
using System;

/// <summary>
/// A passive data container that tracks and broadcasts the player's progress during a single game session.
/// </summary>
/// <remarks>
/// This class does not contain gameplay logic. It relies on other managers (such as ScoreManager) to calculate values,
/// while it focuses purely on storing the data and notifying UI elements through signals.
/// </remarks>
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

	/// <summary>
	/// Processes the active session time every frame.
	/// </summary>
	/// <param name="delta">The time elapsed since the last frame, in seconds.</param>
	/// <remarks>
	/// (e.g., in a menu, game over screen, or paused), the time increment is ignored.
	/// </remarks>
	public override void _Process(double delta)
	{
		if (GameStateManager.Instance.State != GameState.Playing)
			return;

		SessionTime += (float)delta;
	}

	/// <summary>
	/// Resets all tracked session data fields to their default values (zero).
	/// </summary>
	/// <remarks>
	/// This method must be called at the very beginning of a new session 
	/// to ensure the player starts with a clean slate, regardless of previous states.
	/// </remarks>
	public void Reset()
	{
		Wave = 0;
		Score = 0;
		DefeatedBosses = 0;
		SessionTime = 0;
	}
}
