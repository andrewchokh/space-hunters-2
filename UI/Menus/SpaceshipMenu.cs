using Godot;
using System;

public partial class SpaceshipMenu : Control
{
    [Export]
    public PlayerDatabase PlayerDatabase;
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

    private void ChangeSpaceship(int direction = 0)
    {
        if (PlayerDatabase == null)
            return;

        int totalItems = PlayerDatabase.PlayerDataList.Count;

        if (totalItems == 0)
            return;

        _index = (_index + direction + totalItems) % totalItems;
        SpaceshipImage.Texture = PlayerDatabase.PlayerDataList[_index].SpaceshipTexture;
    }

    private void OnBackButtonPressed() =>
        GetTree().ChangeSceneToFile("uid://blyay0mcxh1r4");

    private void OnPreviousButtonPressed() =>
        ChangeSpaceship(-1);

    private void OnNextButtonPressed() =>
        ChangeSpaceship(1);
}
