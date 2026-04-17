using Godot;
using System;

/// <summary>
/// A basic blueprint for any piece of logic that needs to be attached to a game object.
/// </summary>
public interface IComponent
{
    /// <summary>
    /// The main 2D game object (character, item, etc.) that this component is currently working on.
    /// </summary>
    public Node2D Actor { get; }
}
