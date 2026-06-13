using Godot;
using System;

public partial class Game : Node2D
{
	public override void _Ready()
	{
		GameSessionManager.Instance.Reset();
		GameStateManager.Instance.ChangeState(GameState.Playing);

		var player = GetTree().GetFirstNodeInGroup("PlayerSpaceship") as Spaceship;

		if (player == null)
		{
			this.SoftWarn("Player spaceship not found in scene!");
			return;
		}
		player.HealthComponent.ActorDied += GameOver;
	}

	public void GameOver()
	{
		GameStateManager.Instance.ChangeState(GameState.GameOver);
		GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "uid://ncfyugwd3gnm");
	}
}
