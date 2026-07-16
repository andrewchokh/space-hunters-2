using Godot;
using Microsoft.VisualBasic;
using System;

/// <summary>
/// A node that periodically spawns entities into the game world at dynamic positions.
/// </summary>
/// <remarks>
/// This spawner reads the horizontal width of the viewport and specific vertical rows
/// from the MapManager to adaptively place entities just outside the visible screen area.
/// </remarks>
public partial class RowSpawner : Node2D
{
    /// <summary>
    /// The enemy data that will be created at each spawn interval.
    /// </summary>
    [Export]
    public EnemySpaceshipData EnemyData;

    /// <summary>
    /// The horizontal distance from the screen edge where the entity will appear.
    /// </summary>
    [Export]
    public float OffsetX = 30.0f;

    /// <summary>
    /// The timer that controls the frequency of entity spawns.
    /// </summary>
    [Export]
    public Timer Timer;

    /// <summary>
    /// Subscribes to the spawn timer and initializes the spawning cycle.
    /// </summary>

    /// <summary>
    /// The phase manager that this spawner listens to in order to start or stop spawning.
    /// </summary>
    [Export]
    public GamePhaseManager PhaseManager;

    private PackedScene _enemyScene;

    public override void _Ready()
    {
        if (EnemyData == null)
            return;

        if (PhaseManager == null)
            return;

        _enemyScene = ResourceLoader.Load<PackedScene>(EnemyData.SpaceshipScenePath);

        Timer.Timeout += SpawnEntity;
        PhaseManager.OnPhaseChanged += UpdateSpawnerState;
    }

    /// <summary>
    /// Instantiates the entity, calculates its adaptive spawn coordinates, and adds it to the scene.
    /// </summary>
    private void SpawnEntity()
    {
        if (_enemyScene == null)
            return;

        // Randomly selects a row from the MapManager to provide vertical variety.
        int rowCount = MapManager.Instance.FixedRows.Length;
        int randomRowIndex = GD.RandRange(0, rowCount - 1);

        var enemyInstance = _enemyScene.Instantiate<CharacterBody2D>();

        // Positions the entity using the fixed row height and the horizontal offset.
        enemyInstance.GlobalPosition = new Vector2(0 + OffsetX,
            MapManager.Instance.GetRowY(randomRowIndex));

        GetParent().AddChild(enemyInstance);
    }

    /// <summary>
    /// Starts or stops the spawn timer based on the current game phase.
    /// Spawning is active only during the <see cref="GamePhase.Wave"/> phase.
    /// </summary>
    private void UpdateSpawnerState(GamePhase phase)
    {
        if (phase == GamePhase.Wave && Timer.IsStopped())
            Timer.Start();
        else
            Timer.Stop();
    }
}