using Godot;
using System;
using System.IO;
using System.Linq;

[GlobalClass]
public partial class PlayerMovementComponent : MovementComponent
{
    private PlayerInputPattern _playerPattern;

    public override void _Ready() 
    {
        _playerPattern = Pattern as PlayerInputPattern;

        _playerPattern.SetupPlayerPosition(Actor);
    }

    public override void _PhysicsProcess(double delta) =>
        _playerPattern.Execute(Actor as CharacterBody2D, delta);

    public override void _UnhandledInput(InputEvent @event)
    {
        int previousRowIndex = _playerPattern.RowIndex;

        if (@event.IsActionPressed("move_up"))
            _playerPattern.RowIndex++;
        else if (@event.IsActionPressed("move_down"))
            _playerPattern.RowIndex--;

        _playerPattern.RowIndex = Mathf.Clamp(_playerPattern.RowIndex, 0, MapManager.Instance.FixedRows.Length - 1);

        if (_playerPattern.RowIndex == previousRowIndex)
            return;

        _playerPattern.TargetY = MapManager.Instance.GetRowY(_playerPattern.RowIndex);
    }
}
