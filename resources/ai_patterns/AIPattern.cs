using Godot;
using System;

/// <summary>
/// The abstract base class for all data-driven AI behavior patterns.
/// </summary>
/// <remarks>
/// Any custom enemy behavior (e.g., SineWave, Chase) must inherit from this Resource.
/// Because it is a Resource, it is created and configured directly in the Godot FileSystem/Inspector.
/// </remarks>
public abstract partial class AIPattern : Resource
{
    public abstract void Execute(CharacterBody2D entity, double delta);
}
