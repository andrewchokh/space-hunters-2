using Godot;
using System;

/// <summary>
/// A base template for creating different types of combat moves or behaviors.
/// Because it is a Resource, you can save different patterns as files and swap them easily.
/// </summary>
public abstract partial class AttackPattern : Resource
{
    /// <summary>
    /// The specific logic for how this attack works. 
    /// Every new attack you create must fill this in with its own unique actions.
    /// </summary>
    /// <param name="weapon">The weapon component that is currently performing this attack.</param>
    public abstract void Execute(WeaponComponent weapon);
}