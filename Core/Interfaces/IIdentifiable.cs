using Godot;
using System;

/// <summary>
/// An interface for objects that need a unique label to help the game tell them apart.
/// </summary>
public interface IIdentifiable
{
    /// <summary>
    /// A unique string of text used to identify this specific object in the database.
    /// </summary>
    public string ID { get; }
}