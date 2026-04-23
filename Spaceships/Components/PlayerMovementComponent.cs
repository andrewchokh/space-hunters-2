using Godot;
using System;
using System.IO;
using System.Linq;

/// <summary>
/// Handles player input to switch between horizontal lanes and updates the movement pattern.
/// </summary>
[GlobalClass]
public partial class PlayerMovementComponent : MovementComponent
{
    private PlayerInputPattern _playerPattern;

    /// <summary>
    /// Casts the assigned pattern and initializes the player's starting lane.
    /// </summary>
    public override void _Ready() 
    {
        _playerPattern = Pattern as PlayerInputPattern;

        _playerPattern.SetupPlayerPosition(Actor);
    }

    /// <summary>
    /// Delegates the frame-by-frame interpolation logic to the movement pattern.
    /// </summary>
    public override void _PhysicsProcess(double delta) =>
        _playerPattern.Execute(Actor as CharacterBody2D, delta);

    /// <summary>
    /// Listens for lane-switching inputs and updates the target vertical position.
    /// </summary>
    public override void _UnhandledInput(InputEvent @event)
    {
        int previousRowIndex = _playerPattern.RowIndex;

        if (@event.IsActionPressed("move_up"))
            _playerPattern.RowIndex++;
        else if (@event.IsActionPressed("move_down"))
            _playerPattern.RowIndex--;

        // Restricts the RowIndex to ensure the player stays within the map's defined rows.
        _playerPattern.RowIndex = Mathf.Clamp(_playerPattern.RowIndex, 0, MapManager.Instance.FixedRows.Length - 1);

        if (_playerPattern.RowIndex == previousRowIndex)
            return;

        // Updates the target Y-coordinate for the movement pattern to interpolate toward.
        _playerPattern.TargetY = MapManager.Instance.GetRowY(_playerPattern.RowIndex);
    }
}