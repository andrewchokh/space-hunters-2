using Godot;
using System;

/// <summary>
/// An interface for objects that need a name and a description to show to the player.
/// </summary>
public interface IDescriptable
{
    /// <summary>
    /// The name of the object as it should appear in the game's menus or UI.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// A short text explaining what the object is or what it does.
    /// </summary>
    public string Description { get; }
}