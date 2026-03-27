[Export]
    public PackedScene Entity;
    [Export]
    public float OffsetX = 30.0f;
    [Export]
    public Timer Timer;

    /// <summary>
    /// subscribes to the spawn timer.
    /// </summary>
    public override void _Ready()
    {
        if (Entity == null)
        {
            GD.PushError($"{Name}: Entity is not assigned!");
            return;
        }

        Timer.Timeout += SpawnEntity;
    }

    /// <summary>
    /// Instantiates the entity, calculates its adaptive spawn coordinates, and adds it to the scene.
    /// </summary>
    private void SpawnEntity()
    {
        int rowCount = MapManager.Instance.FixedRows.Length;
        int randomRowIndex = GD.RandRange(0, rowCount - 1);

        var enemyInstance = Entity.Instantiate<CharacterBody2D>();
        enemyInstance.GlobalPosition = new Vector2(GetViewportRect().Size.X + OffsetX,
            MapManager.Instance.GetRowY(randomRowIndex));

        GetParent().AddChild(enemyInstance);
    }