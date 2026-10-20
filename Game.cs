using Godot;
using System;

/// <summary>
/// Root controller for the main gameplay scene.
/// Manages session lifecycle, locates the player spaceship, and handles the game-over transition.
/// </summary>
public partial class Game : Node2D
{
	[Export]
	public Control GameOverMenu;
    [Export]
    public Marker2D PlayerSpawnpoint;
    [Export]
    public PlayerDatabase PlayerDatabase;
	/// <summary>
	/// Resets the session, sets the game state to Playing,
	/// and subscribes to the player's death event to trigger game over.
	/// </summary>
	public override void _Ready()
	{
		GameSessionManager.Instance.Reset();
		GameStateManager.Instance.ChangeState(GameState.Playing);

        if (PlayerDatabase == null)
            return;

        if(PlayerSpawnpoint == null)
            return;

        var spaceshipData = GameSessionManager.Instance.SelectedSpaceship;

        if (spaceshipData == null)
        {
            spaceshipData = PlayerDatabase.PlayerDataList[0];
            GameSessionManager.Instance.SelectedSpaceship = spaceshipData;
        }

        var packedScene = GD.Load<PackedScene>(spaceshipData.SpaceshipScenePath);

        if(packedScene == null)
            return;

        var player = packedScene.Instantiate<Spaceship>();
        
		if (player == null)
		{
			this.SoftWarn("Player spaceship not found in scene!");
			return;
		}

        AddChild(player);
        player.GlobalPosition = PlayerSpawnpoint.GlobalPosition;

        if (GameOverMenu == null)
            return;

		player.HealthComponent.ActorDied += GameOver;
		GameStateManager.Instance.OnStateChanged += HandleStateChanged;
	}

	/// <summary>
	/// Transitions the game to the Game Over state.
	/// </summary>
	public void GameOver() => GameStateManager.Instance.ChangeState(GameState.GameOver);

	/// <summary>
	/// Processes changes in the global game state to update scene-level elements,
	/// such as displaying the game-over user interface.
	/// </summary>
	public void HandleStateChanged(GameState state)
	{
		if (state != GameState.GameOver)
			return;

		GameOverMenu.Visible = true;
	}
}
