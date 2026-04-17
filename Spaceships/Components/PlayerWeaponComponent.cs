using Godot;
using System;

/// <summary>
/// A weapon component that allows the player to trigger attacks via input actions.
/// </summary>
[GlobalClass]
public partial class PlayerWeaponComponent : WeaponComponent
{
    /// <summary>
    /// The name of the input action (defined in Input Map) used to trigger the weapon.
    /// </summary>
    [Export] public string FireAction = "fire";

    /// <summary>
    /// Polls the input system to check if the fire button is currently held down.
    /// </summary>
    /// <returns>True if the action is active.</returns>
    public override bool ShouldFire() => Input.IsActionPressed(FireAction);
}