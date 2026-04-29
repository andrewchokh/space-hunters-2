using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// A global database singleton that stores and provides access to core game entity resources.
/// Use this to retrieve resource files for spaceships and abilities from any point in the project.
/// </summary>
/// <remarks>
/// PowerupData implementation is currently skipped as the type does not exist in the project yet.
/// </remarks>
public partial class DataManager : Node
{
    [Export]
    public SpaceshipData[] Spaceships;
    [Export]
    public AbilityData[] Abilities;

    public static DataManager Instance { get; private set; }

    public readonly Dictionary<string, SpaceshipData> SpaceshipData = new();
    public readonly Dictionary<string, AbilityData> AbilityData = new();

    /// <summary>
    /// Initializes the singleton instance and populates the lookup dictionaries
    /// from the exported resource arrays.
    /// </summary>
    public override void _Ready()
    {
        Instance = this;

        if(Spaceships != null)
            foreach (var s in Spaceships) SpaceshipData[s.ID] = s;

        if (Abilities != null)
            foreach (var s in Abilities) AbilityData[s.ID] = s;
    }
}
