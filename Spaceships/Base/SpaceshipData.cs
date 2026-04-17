using Godot;
using System;

/// <summary>
/// A base data template defining the core attributes and statistics for any spaceship.
/// </summary>
[GlobalClass]
public abstract partial class SpaceshipData : Resource, IIdentifiable, IDescriptable
{
    [ExportGroup("Identification")]
    /// <summary>
    /// The unique technical identifier for this ship type.
    /// </summary>
    [Export]
    public string ID { get; set; }

    /// <summary>
    /// The name of the ship as it appears to the player in the UI.
    /// </summary>
    [Export]
    public string DisplayName { get; set; }

    /// <summary>
    /// A flavor text description of the ship's history or capabilities.
    /// </summary>
    [Export]
    public string Description { get; set; }

    [ExportGroup("Base Stats")]
    /// <summary>
    /// The starting health pool for this ship.
    /// </summary>
    [Export]
    public int BaseHealth = 10;

    /// <summary>
    /// A damage reduction value applied to incoming hits.
    /// </summary>
    [Export]
    public int BaseProtection = 1;

    /// <summary>
    /// Determines if the ship gains a brief period of invulnerability after taking damage.
    /// </summary>
    [Export]
    public bool HasInvincibilityFrames = false;

    [ExportGroup("Weapon")]
    /// <summary>
    /// The travel velocity of projectiles fired by this ship.
    /// </summary>
    [Export]
    public float ProjectileSpeed = 230f;

    /// <summary>
    /// The amount of health deducted from a target upon a successful hit.
    /// </summary>
    [Export]
    public int ProjectileDamage = 3;
}