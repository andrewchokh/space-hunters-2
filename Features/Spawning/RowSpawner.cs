using Godot;
using System;
using Godot.Collections;

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
    /// A collection of enemy spaceship configurations available for spawning.
    /// </summary>
    [Export]
    public Array<EnemySpaceshipData> EnemyData;

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
    /// The phase manager that this spawner listens to in order to start or stop spawning.
    /// </summary>
    [Export]
    public GamePhaseManager PhaseManager;

    public override void _Ready()
    {
        if (EnemyData == null)
            return;

        if (PhaseManager == null)
            return;

        Timer.Timeout += SpawnEntity;
        PhaseManager.OnPhaseChanged += UpdateSpawnerState;
    }

    /// <summary>
    /// Instantiates the entity, calculates its adaptive spawn coordinates, and adds it to the scene.
    /// </summary>
    private void SpawnEntity()
    {
        if (EnemyData == null || EnemyData.Count == 0)
            return;

        // Randomly selects a row from the MapManager to provide vertical variety.
        int rowCount = MapManager.Instance.FixedRows.Length;
        int randomRowIndex = GD.RandRange(0, rowCount - 1);

        var enemyData = SelectEnemyByWave();

        if (enemyData == null)
            return;

        var enemyInstance = GD.Load<PackedScene>(
            enemyData.SpaceshipScenePath).Instantiate<CharacterBody2D>();

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

    /// <summary>
    /// Selects an enemy spaceship configuration using a weighted random probability system tied to the current wave.
    /// </summary>
    /// <returns>
    /// A randomly chosen <see cref="EnemySpaceshipData"/> matching the rolled tier, 
    /// or <see langword="null"/> if the calculated weight is zero or no matching enemies are found.
    /// </returns>
    /// <remarks>
    /// The selection probability dynamically shifts as the wave number increases:
    /// Tier 3 weight decreases over time, Tier 2 begins appearing after wave 5, 
    /// and Tier 1 begins appearing after wave 10.
    /// </remarks>
    private EnemySpaceshipData SelectEnemyByWave()
    {
        int tier1 = Math.Max(0, 2 * (GameSessionManager.Instance.Wave - 10));
        int tier2 = Math.Max(0, 6 * (GameSessionManager.Instance.Wave - 5));
        int tier3 = Math.Max(0, 100 - 5 * (GameSessionManager.Instance.Wave - 1));

        int totalWeight = tier1 + tier2 + tier3;

        // Roll a random number to determine which tier spawns.
        int roll = GD.RandRange(0, totalWeight - 1);
        int chosenTier;

        // Check the roll against the accumulated weights to pick the tier.
        if (roll < tier3)
            chosenTier = 3;
        else if (roll < tier3 + tier2)
            chosenTier = 2;
        else
            chosenTier = 1;

        // Filter the master enemy list to find only ships matching the chosen tier.
        var tierEnemies = new Array<EnemySpaceshipData>();

        for (int i = 0; i < EnemyData.Count; i++)
        {
            if (EnemyData[i].Tier == chosenTier)
            {
                tierEnemies.Add(EnemyData[i]);
            }
        }

        if (tierEnemies == null || tierEnemies.Count == 0)
            return null;

        // Randomly select one enemy from the valid pool.
        return tierEnemies[GD.RandRange(0, tierEnemies.Count - 1)];
    }
}