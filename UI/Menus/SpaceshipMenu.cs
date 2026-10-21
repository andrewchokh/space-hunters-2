using Godot;
using System;

/// <summary>
/// Spaceship selection screen that lets the player cycle through available spaceships with a carousel.
/// The spaceship shown when leaving the menu becomes the player's choice for the next game session.
/// </summary>
/// <remarks>
/// The selection is stored in <see cref="GameSessionManager.SelectedSpaceship"/>.
/// </remarks>
public partial class SpaceshipMenu : Control
{
    [Export]
    public PlayerSpaceshipDatabase PlayerSpaceshipDatabase;
    [Export]
    public TextureRect SpaceshipImage;
    [Export]
    public Button BackButton;
    [Export]
    public Button PreviousButton;
    [Export]
    public Button NextButton;

    private int _index;

    public override void _Ready()
    {
        BackButton.Pressed += OnBackButtonPressed;
        PreviousButton.Pressed += OnPreviousButtonPressed;
        NextButton.Pressed += OnNextButtonPressed;
        ChangeSpaceship();
    }

    /// <summary>
    /// Moves the carousel index by the given step and updates the displayed spaceship.
    /// </summary>
    /// <param name="direction">-1 for previous, 1 for next, 0 to refresh the current spaceship.</param>
    private void ChangeSpaceship(int direction = 0)
    {
        if (PlayerSpaceshipDatabase == null)
            return;

        int totalItems = PlayerSpaceshipDatabase.PlayerSpaceshipDataEntries.Count;

        if (totalItems == 0)
            return;

        _index = (_index + direction + totalItems) % totalItems;
        SpaceshipImage.Texture = PlayerSpaceshipDatabase.PlayerSpaceshipDataEntries[_index].SpaceshipTexture;
    }

    /// <summary>
    /// Saves the currently highlighted spaceship as the player's choice and returns to the main menu scene.
    /// </summary>
    private void OnBackButtonPressed()
    {
        GameSessionManager.Instance.SelectedSpaceship = PlayerSpaceshipDatabase.PlayerSpaceshipDataEntries[_index];
        GetTree().ChangeSceneToFile("uid://blyay0mcxh1r4");
    }

    private void OnPreviousButtonPressed() =>
        ChangeSpaceship(-1);

    private void OnNextButtonPressed() =>
        ChangeSpaceship(1);
}
