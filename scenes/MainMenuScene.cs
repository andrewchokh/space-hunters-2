using Godot;
using System;

/// <summary>
/// Controls the main menu user interface, handling navigation to the game scene and application exit.
/// </summary>
public partial class MainMenuScene : Control
{
    [Export]
    public Button PlayButton;
    [Export]
    public Button QuitButton;

    public override void _Ready()
    {
        PlayButton.Pressed += OnPlayButtonPressed;
        QuitButton.Pressed += OnQuitButtonPressed;
    }

    /// <summary>
    /// Event handler for the play button. Transitions the active scene tree to the game scene.
    /// </summary>
    private void OnPlayButtonPressed() =>
        GetTree().ChangeSceneToFile("res://scenes/GameScene.tscn");

    /// <summary>
    /// Event handler for the quit button. Terminates the application gracefully.
    /// </summary>
    private void OnQuitButtonPressed() => GetTree().Quit();
}
