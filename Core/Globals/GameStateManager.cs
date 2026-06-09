using Godot;
using System;

public partial class GameStateManager : Node
{
	public static GameStateManager Instance { get; private set; }

	public override void _Ready() => Instance = this;

	public enum GameState
	{
		MainMenu,
		Playing,
		Paused,
		GameOver,
		Cutscene
	}
}
