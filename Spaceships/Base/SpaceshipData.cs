using Godot;
using System;

[GlobalClass]
public partial class SpaceshipData : Resource
{
    [ExportGroup("Base Stats")]
    [Export]
    public int BaseHealth = 10;
    [Export]
    public int BaseProtection = 1;
    [Export]
    public bool HasInvincibilityFrames = false;
}
