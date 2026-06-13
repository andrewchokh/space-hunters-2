using Godot;
using System;

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

	public override void _Ready()
	{
		ScoreLabel.Text = "Your score - " + ScoreManager.Instance.Score.ToString();
		TryAgainButton.Pressed += TryAgainButtonPressed;
		BackToMainMenuButton.Pressed += BackToMainMenuButtonPressed;
		QuitTheGameButton.Pressed += QuitTheGameButtonPressed;
	}

	private void TryAgainButtonPressed()
	{
		GameStateManager.Instance.ChangeState(GameState.Playing);
		GetTree().ChangeSceneToFile("uid://1jaqfvkfofdo");
	}

	private void BackToMainMenuButtonPressed()
	{
		GameStateManager.Instance.ChangeState(GameState.MainMenu);
		GetTree().ChangeSceneToFile("uid://booe2en3u4wmp");
	}

	private void QuitTheGameButtonPressed() => GetTree().Quit();
}