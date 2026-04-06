using Godot;
using System;

[GlobalClass]
public partial class AbilityPattern : Resource
{
    [Export]
    public double Duration = 3.0;
    [Export]
    public double Cooldown = 10.0;

}
