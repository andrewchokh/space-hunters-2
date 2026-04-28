using Godot;
using System;

/// <summary>
/// A base template for defining how an entity moves through the game world.
/// </summary>
[GlobalClass]
public abstract partial class MovePattern : Resource
{
    /// <summary>
    /// The specific math or logic that determines the entity's movement every frame.
    /// </summary>
    /// <param name="actor">The character body that will be moved.</param>
    /// <param name="delta">The time elapsed since the last physics frame.</param>
    public abstract void Execute(CharacterBody2D actor, double delta);
}