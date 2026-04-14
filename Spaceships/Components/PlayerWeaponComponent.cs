using Godot;
using System;

[GlobalClass]
public partial class PlayerWeaponComponent : WeaponComponent
{
    [Export] public string FireAction = "fire";

    public override bool ShouldFire() => Input.IsActionPressed(FireAction);
}
