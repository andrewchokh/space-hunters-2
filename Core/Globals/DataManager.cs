using Godot;
using System;
using System.Collections.Generic;

public partial class DataManager : Node
{
    [Export]
    public SpaceshipData[] Spaceships;
    //  [Export]
    //  public AbilityData[] Abilities;

    public static DataManager Instance { get; private set; }

    private Dictionary<string, SpaceshipData> _spaceshipData;
    // private Dictionary<string, AbilityData> _abilityData;

    public override void _Ready()
    {
        Instance = this;
        _spaceshipData = InitializeData(Spaceships);
        //   _abilityData = InitializeData(Abilities);
    }

    public SpaceshipData GetSpaceship(string id) => _spaceshipData.GetValueOrDefault(id);

    // public  AbilityData GetAbility(string id) => _abilities.GetValueOrDefault(id);

    private static Dictionary<string, T> InitializeData<T>(T[] rawData) where T : IIdentifiable
    {
        var dictionary = new Dictionary<string, T>();

        if (rawData == null)
            return dictionary;

        foreach (var item in rawData)
        {
            if (item == null)
                continue;

            dictionary.TryAdd(item.ID, item);
        }

        return dictionary;
    }
}
