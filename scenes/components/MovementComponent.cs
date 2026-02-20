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
    public Node2D Target;
    [Export]
    public float Speed = 15.0f;
    [Export]
    public int StartingRow = 2;

    private int _rowIndex;
    private float _targetY;

    /// <summary>
    /// Initializes the target's position based on the starting row index.
    /// </summary>
    /// <remarks>
    /// Retrieves the initial Y-coordinate from <see cref="MapManager"/> and
    /// instantly snaps the <see cref="Target"/> to avoid sliding during spawn.
    /// </remarks>
    public override void _Ready()
    {
        if (Target == null)
        {
            GD.PrintErr("MovementComponent: Target node is not assigned.");
            SetPhysicsProcess(false);
            return;
        }

        _rowIndex = Mathf.Clamp(StartingRow, 0, MapManager.Instance.FixedRows.Length - 1);
        _targetY = MapManager.Instance.GetRowY(_rowIndex);
        Target.GlobalPosition = new Vector2(Target.GlobalPosition.X, _targetY);
    }

    /// <summary>
    /// Updates the target's position every frame using linear interpolation.
    /// </summary>
    /// <param name="delta">The time elapsed since the previous frame in seconds.</param>
    public override void _PhysicsProcess(double delta)
    {
        HandleInput();

        float newY = Mathf.Lerp(Target.GlobalPosition.Y, _targetY, Mathf.Min(Speed * (float)delta, 1.0f));
        Target.GlobalPosition = new Vector2(Target.GlobalPosition.X, newY);
    }

    /// <summary>
    /// Detects player input and updates the target row index.
    /// </summary>
    /// <remarks>
    /// Uses discrete input detection (JustPressed) to ensure one keypress equals one lane shift.
    /// The index is safely bounded using <see cref="Mathf.Clamp"/> based on the <see cref="MapManager"/> row count.
    /// </remarks>
    private void HandleInput()
    {
        int previousRowIndex = _rowIndex;

        if (Input.IsActionJustPressed("move_up"))
            _rowIndex++;
        else if (Input.IsActionJustPressed("move_down"))
            _rowIndex--;

        _rowIndex = Mathf.Clamp(_rowIndex, 0, MapManager.Instance.FixedRows.Length - 1);

        if (_rowIndex == previousRowIndex)
            return;

        _targetY = MapManager.Instance.GetRowY(_rowIndex);
    }
}
