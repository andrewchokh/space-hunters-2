using Godot;
using System;

/// <summary>
/// Root controller for the main gameplay scene.
/// Manages session lifecycle, locates the player spaceship, and handles the game-over transition.
/// </summary>
public partial class Game : Node2D
{
	/// <summary>
	/// Resets the session, sets the game state to Playing,
	/// and subscribes to the player's death event to trigger game over.
	/// </summary>
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

	/// <summary>
	/// Transitions the game to the Game Over state and loads the Game Over scene.
	/// Uses <see cref="GodotObject.CallDeferred"/> to avoid removing physics objects mid-callback.
	/// </summary>
	public void GameOver()
	{
		GameStateManager.Instance.ChangeState(GameState.GameOver);
		GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "uid://ncfyugwd3gnm");
	}
}
