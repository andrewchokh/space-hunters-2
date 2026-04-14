using Godot;
using System;

[GlobalClass]
public abstract partial class SpaceshipData : Resource, IIdentifiable, IDescriptable
{
    [ExportGroup("Identification")]
    [Export]
    public string ID { get; set; }
    [Export]
    public string DisplayName { get; set; }
    [Export]
    public string Description { get; set; }

    [ExportGroup("Base Stats")]
    [Export]
    public int BaseHealth = 10;
    [Export]
    public int BaseProtection = 1;
    [Export]
    public bool HasInvincibilityFrames = false;

    [ExportGroup("Weapon")]
    [Export]
    public float ProjectileSpeed = 230f;
    [Export]
    public int ProjectileDamage = 3;

}
