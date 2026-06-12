using Godot;
using System;

public partial class Game : Node2D
{
	public override void _Ready()
	{
		GameSessionManager.Instance.Reset();
		GameStateManager.Instance.ChangeState(GameState.Playing);
	}
}
