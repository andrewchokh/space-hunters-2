using Godot;
using System;

/// <summary>
/// A simple ability used for testing purposes to confirm that the ability system is running.
/// </summary>
[GlobalClass]
public partial class DebugAbilityData : AbilityData
{
    /// <summary>
    /// Prints a message to the output console every frame while the ability is active.
    /// </summary>
    /// <param name="actor">The character currently using this debug ability.</param>
    /// <param name="delta">The time elapsed since the last frame.</param>
    public override void Execute(CharacterBody2D actor, double delta) {
        GD.Print("DEBUG Ability processing!");
    }
}