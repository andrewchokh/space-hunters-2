using Godot;
using System;

/// <summary>
/// Defines the data and timing parameters for a specific ability.
/// </summary>
[GlobalClass]
public partial class AbilityPattern : Resource
{
    [Export]
    public double Duration = 3.0;
    [Export]
    public double Cooldown = 10.0;
}
