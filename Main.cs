using Godot;
using System;

/// <summary>
/// Root controller of the main menu scene.
/// Owns the menu screens and switches between them without changing the scene.
/// </summary>
public partial class Main : Node2D
{
    [Export]
    public MainMenu MainMenu;
    [Export]
    public SpaceshipMenu SpaceshipMenu;

    public override void _Ready()
    {
        SpaceshipMenu.BackButtonPressed += ShowMainMenu;
        MainMenu.SpaceshipSelectButtonPressed += ShowSpaceshipMenu;
    }

    private void ShowMainMenu()
    {
        SpaceshipMenu.Hide();
        MainMenu.Show();
    }

    private void ShowSpaceshipMenu()
    {
        SpaceshipMenu.Show();
        MainMenu.Hide();
    }
}
