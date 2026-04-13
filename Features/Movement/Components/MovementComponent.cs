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
[GlobalClass]
public partial class MovementComponent : Node
{
    public Node2D Actor => GetParent() as Node2D;

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
        var body = Actor as CharacterBody2D;

        _rowIndex = Mathf.Clamp(StartingRow, 0, MapManager.Instance.FixedRows.Length - 1);
        _targetY = MapManager.Instance.GetRowY(_rowIndex);
        body.GlobalPosition = new Vector2(body.GlobalPosition.X, _targetY);
    }

    /// <summary>
    /// Updates the entity's position every frame using linear interpolation.
    /// </summary>
    /// <param name="delta">The time elapsed since the previous frame in seconds.</param>
    public override void _PhysicsProcess(double delta)
    {
        var body = Actor as CharacterBody2D;

        float newY = Mathf.Lerp(body.GlobalPosition.Y, _targetY, Mathf.Min(Speed * (float)delta, 1.0f));
        body.GlobalPosition = new Vector2(body.GlobalPosition.X, newY);

        body.MoveAndSlide();
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

    public override string[] _GetConfigurationWarnings()
    {
        if (GetParent() is not CharacterBody2D)
            return new string[] { "This component requires a CharacterBody2D parent to execute physics patterns." };
            
        return base._GetConfigurationWarnings();
    }
}
