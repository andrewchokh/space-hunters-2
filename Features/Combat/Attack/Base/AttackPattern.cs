using Godot;
using System;

public abstract partial class AttackPattern : Resource
{
    public abstract void Execute(WeaponComponent weapon);
}
