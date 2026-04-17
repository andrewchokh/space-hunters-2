using Godot;
using System;

/// <summary>
/// An interface for objects that have a specific facing or movement direction.
/// </summary>
public interface IDirectable
{
    /// <summary>
    /// A 2D coordinate representing which way the object is pointing or moving.
    /// </summary>
    public Vector2 Direction { get; }
}