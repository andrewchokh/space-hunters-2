using Godot;
using System;

public partial class GameStateManager : Node
{
	private GameState _currentState;
	public GameState CurrentState => _currentState;
	public static GameStateManager Instance { get; private set; }

	public override void _Ready() => Instance = this;
	

	public void ChangeState(GameState newState)
	{
		if (_currentState == newState)
			return;

		if (newState == GameState.Paused && _currentState != GameState.Playing)
			return;

		_currentState = newState;

		switch (newState)
		{
			case GameState.MainMenu:
				break;
			case GameState.Playing:
				break;
			case GameState.Paused:
				GetTree().Paused = true;
				break;
			case GameState.GameOver:
				break;
			case GameState.Cutscene:
				break;
		}
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
