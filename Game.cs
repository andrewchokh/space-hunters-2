using Godot;
using System;

/// <summary>
/// Root controller for the main gameplay scene.
/// Spawns the player's selected spaceship, manages the session lifecycle, and handles the game-over transition.
/// </summary>
/// <remarks>
/// The spaceship is taken from <see cref="GameSessionManager.SelectedSpaceship"/>. If nothing was selected
/// (for example, when this scene is launched directly from the editor), the first spaceship
/// from <see cref="PlayerSpaceshipDatabase"/> is used as a fallback.
/// </remarks>
public partial class Game : Node2D
{
    [Export]
    public Control GameOverMenu;
    [Export]
    public Marker2D PlayerSpawnpoint;
    [Export]
    public PlayerSpaceshipDatabase PlayerSpaceshipDatabase;

    /// <summary>
    /// Resets the session, spawns the player spaceship, and subscribes to game-over events.
    /// </summary>
    public override void _Ready()
    {
        GameSessionManager.Instance.Reset();

        var player = SpawnPlayer();

        if(player == null)
            return;

        if (GameOverMenu == null)
            return;

        player.HealthComponent.ActorDied += GameOver;

        GameStateManager.Instance.OnStateChanged += HandleStateChanged;
        GameStateManager.Instance.ChangeState(GameState.Playing);
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

    /// <summary>
    /// Unsubscribes from the game state event to prevent access to disposed nodes after a scene change.
    /// </summary>
    public override void _ExitTree() => GameStateManager.Instance.OnStateChanged -= HandleStateChanged;

    /// <summary>
    /// Creates the selected player spaceship at <see cref="PlayerSpawnpoint"/>.
    /// </summary>
    /// <returns>The spawned spaceship, or <c>null</c> if the database, spawnpoint, or spaceship scene is missing.</returns>
    private Spaceship SpawnPlayer()
    {
        if (PlayerSpaceshipDatabase == null)
            return null;

        if (PlayerSpawnpoint == null)
            return null;

        var spaceshipData = GameSessionManager.Instance.SelectedSpaceship;

        if (spaceshipData == null)
        {
            spaceshipData = PlayerSpaceshipDatabase.PlayerSpaceshipDataEntries[0];
            GameSessionManager.Instance.SelectedSpaceship = spaceshipData;
        }

        var packedScene = GD.Load<PackedScene>(spaceshipData.SpaceshipScenePath);

        if (packedScene == null)
            return null;

        var player = packedScene.Instantiate<Spaceship>();
        AddChild(player);
        player.GlobalPosition = PlayerSpawnpoint.GlobalPosition;

        return player;
    }
}
