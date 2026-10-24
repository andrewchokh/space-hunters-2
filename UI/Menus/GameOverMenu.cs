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
    /// Subscribes to button press and visibility events.
    /// </summary>
	public override void _Ready()
    {
		TryAgainButton.Pressed += TryAgainButtonPressed;
		BackToMainMenuButton.Pressed += BackToMainMenuButtonPressed;
		QuitTheGameButton.Pressed += QuitTheGameButtonPressed;
        VisibilityChanged += OnVisibilityChanged;
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

    /// <summary>
    /// Refreshes the final score text each time the menu is shown.
    /// </summary>
    private void OnVisibilityChanged()
    {
        if (!Visible)
            return;

        ScoreLabel.Text = "Your score - " + ScoreManager.Instance.Score;
    }
}
