using Godot;
using System;

public partial class SpaceshipMenu : Control
{
    [Export]
    public Button BackButton;

	public override void _Ready()
    {
        BackButton.Pressed += OnBackButtonPressed;
    }

    private void OnBackButtonPressed() =>
        GetTree().ChangeSceneToFile("uid://blyay0mcxh1r4");

}
