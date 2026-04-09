using Godot;
using System;

public partial class MainMenuScene : Control
{
    public override void _Ready()
    {
        Button playButton = GetNode<Button>("VBoxContainer/PlayButton");
        Button quitButton = GetNode<Button>("VBoxContainer/QuitButton");

        playButton.Pressed += OnPlayButtonPressed;
        quitButton.Pressed += OnQuitButtonPressed;
    }

    private void OnPlayButtonPressed() =>
        GetTree().ChangeSceneToFile("res://scenes/GameScene.tscn");

    private void OnQuitButtonPressed() => GetTree().Quit();
}
