using Godot;
using System;

/// <summary>    
/// Defines the available global states within the game.    
/// </summary>
public enum GameState
{
	MainMenu,
	Playing,
	Paused,
	GameOver,
	Cutscene
}

/// <summary>
/// Manages the global state of the game, ensuring valid transitions between different gameplay phases.
/// Use this to query the current state or trigger state changes across the application.
/// </summary>
public partial class GameStateManager : Node
{
	private GameState _currentState;

	/// <summary>
	/// A read-only property that returns the current state of the game.
	/// </summary>
	public GameState CurrentState => _currentState;

	/// <summary>
	/// Invoked whenever the game state successfully changes. 
	/// UI and Audio systems should subscribe to this for automatic updates.
	/// </summary>
	public event Action<GameState> OnStateChanged;

	public static GameStateManager Instance { get; private set; }

	/// <summary>
	/// Initializes the singleton instance and sets the default state to MainMenu upon application launch.
	/// </summary>
	public override void _Ready()
	{
		Instance = this;
		_currentState = GameState.MainMenu;
		GD.Print("Base state:" + _currentState);
	}

	/// <summary>
	/// Attempts to transition the game to a new state.
	/// </summary>
	/// <param name="newState">The target state to transition into.</param>
	/// <remarks>
	/// This method includes guard checks to prevent illogical transitions. 
	/// For instance, it strictly prevents entering the Paused state unless the game is currently in the Playing state.
	/// It also automatically toggles the Godot SceneTree pause state when applicable.
	/// </remarks>
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

		GD.Print("Chane state to: " + newState);
		OnStateChanged?.Invoke(_currentState);
	}

	/// <summary>
	/// Determines whether global player input should be processed based on the current game state.
	/// </summary>
	/// <returns>
	/// <c>false</c> if the game is in a Cutscene or GameOver state; otherwise, <c>true</c>.
	/// </returns>
	public bool IsInputAllowed()
	{
		if (_currentState is GameState.Cutscene or GameState.GameOver)
			return false;

		return true;
	}
}
