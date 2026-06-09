using Godot;
using System;

public partial class GameStateManager : Node
{
	public enum GameState
	{
		MainMenu,
		Playing,
		Paused,
		GameOver,
		Cutscene
	}
}
