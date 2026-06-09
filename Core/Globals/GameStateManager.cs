using Godot;
using System;

public partial class GameStateManager : Node
{
	private GameState _currentState;
	public GameState CurrentState => _currentState;
	public static GameStateManager Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}

	public bool IsInputAllowed()
	{
		if (_currentState is GameState.Cutscene or GameState.GameOver)
			return false;

		return true;
	}

	public enum GameState
	{
		MainMenu,
		Playing,
		Paused,
		GameOver,
		Cutscene
	}
}
