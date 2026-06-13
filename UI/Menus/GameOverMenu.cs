using Godot;
using System;

/// <summary>
/// UI controller for the Game Over screen.
/// Displays the player's final score and provides navigation options
/// to retry, return to the main menu, or quit the application.
/// </summary>
public partial class GameOverMenu : Control
{
	[Export]
	public Button TryAgainButton;
	[Export]
	public Button BackToMainMenuButton;
	[Export]
	public Button QuitTheGameButton;
	[Export]
	public Label ScoreLabel;

	/// <summary>
	/// Initializes the menu by displaying the final score and subscribing to button press events.
	/// </summary>
	public override void _Ready()
	{
		ScoreLabel.Text = "Your score - " + ScoreManager.Instance.Score.ToString();
		TryAgainButton.Pressed += TryAgainButtonPressed;
		BackToMainMenuButton.Pressed += BackToMainMenuButtonPressed;
		QuitTheGameButton.Pressed += QuitTheGameButtonPressed;
	}

	/// <summary>
	/// Restarts the game session by transitioning back to the Game scene.
	/// </summary>
	private void TryAgainButtonPressed()
	{
		GameStateManager.Instance.ChangeState(GameState.Playing);
		GetTree().ChangeSceneToFile("uid://1jaqfvkfofdo");
	}

	/// <summary>
	/// Returns the player to the main menu screen.
	/// </summary>
	private void BackToMainMenuButtonPressed()
	{
		GameStateManager.Instance.ChangeState(GameState.MainMenu);
		GetTree().ChangeSceneToFile("uid://booe2en3u4wmp");
	}

	/// <summary>
	/// Terminates the application.
	/// </summary>
	private void QuitTheGameButtonPressed() => GetTree().Quit();
}