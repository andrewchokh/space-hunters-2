using Godot;
using System;

/// <summary>
/// Manages the global state of the game, ensuring valid transitions between different gameplay phases.
/// Use this to query the current state or trigger state changes across the application.
/// </summary>
public partial class GameStateManager : Node
{
	/// <summary>
	/// A current game state with default state main menu.
	/// </summary>
	private GameState _state = GameState.MainMenu;

	/// <summary>
	/// A read-only property that returns the current state of the game.
	/// </summary>
	public GameState State => _state;

	/// <summary>
	/// Invoked whenever the game state successfully changes. 
	/// UI and Audio systems should subscribe to this for automatic updates.
	/// </summary>
	public event Action<GameState> OnStateChanged;

	public static GameStateManager Instance { get; private set; }

	/// <summary>
	/// Initializes the singleton instance.
	/// </summary>
	public override void _Ready() => Instance = this;

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
		if (_state == newState)
			return;

		if (newState == GameState.Paused && _state != GameState.Playing)
			return;

		_state = newState;

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

		OnStateChanged?.Invoke(_state);
	}
}
