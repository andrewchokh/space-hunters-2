using Godot;
using System;

/// <summary>
/// Component responsible for discrete object movement between fixed horizontal lanes.
/// </summary>
/// <remarks>
/// This system decouples input and target calculation from the visual representation.
/// It relies on <see cref="MapManager"/> to translate row indices into Y-coordinates
/// and uses <see cref="Mathf.Lerp"/> to provide smooth transitions between lanes.
/// </remarks>
public partial class MovementComponent : Node
{
    [Export]
    public CharacterBody2D Entity;
    [Export]
    public float Speed = 15.0f;
    [Export]
    public int StartingRow = 2;

    private int _rowIndex;
    private float _targetY;

    /// <summary>
    /// Initializes the entity's position based on the starting row index.
    /// </summary>
    /// <remarks>
    /// Retrieves the initial Y-coordinate from <see cref="MapManager"/> and
    /// instantly snaps the <see cref="Entity"/> to avoid sliding during spawn.
    /// </remarks>
    public override void _Ready()
    {
        if (Entity == null)
        {
            GD.PrintErr("MovementComponent: Entity node is not assigned.");
            SetPhysicsProcess(false);
            return;
        }

        _rowIndex = Mathf.Clamp(StartingRow, 0, MapManager.Instance.FixedRows.Length - 1);
        _targetY = MapManager.Instance.GetRowY(_rowIndex);
        Entity.GlobalPosition = new Vector2(Entity.GlobalPosition.X, _targetY);
    }

    /// <summary>
    /// Updates the entity's position every frame using linear interpolation.
    /// </summary>
    /// <param name="delta">The time elapsed since the previous frame in seconds.</param>
    public override void _PhysicsProcess(double delta)
    {
        float newY = Mathf.Lerp(Entity.GlobalPosition.Y, _targetY, Mathf.Min(Speed * (float)delta, 1.0f));
        Entity.GlobalPosition = new Vector2(Entity.GlobalPosition.X, newY);

        Entity.MoveAndSlide();
    }

    /// <summary>
    /// Detects player input and updates the target row index.
    /// </summary>
    /// <remarks>
    /// Uses Godot's event-driven input method to guarantee no dropped inputs
    /// regardless of framerate or physics tick rate.
    /// </remarks>
    public override void _UnhandledInput(InputEvent @event)
    {
        int previousRowIndex = _rowIndex;

        if (@event.IsActionPressed("move_up"))
            _rowIndex++;
        else if (@event.IsActionPressed("move_down"))
            _rowIndex--;

        _rowIndex = Mathf.Clamp(_rowIndex, 0, MapManager.Instance.FixedRows.Length - 1);

        if (_rowIndex == previousRowIndex)
            return;

        _targetY = MapManager.Instance.GetRowY(_rowIndex);
    }
}
