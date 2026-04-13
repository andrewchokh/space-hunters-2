using Godot;
using System;

/// <summary>
/// Automatically destroys a designated entity when this notifier exits the visible screen area.
/// </summary>
/// <remarks>
/// A self-contained cleanup tool. Attach this to moving scenes (like enemies or projectiles)
/// to automatically prevent memory leaks when they leave the battlefield.
/// </remarks>
public partial class DisposableComponent : VisibleOnScreenNotifier2D
{
    [Export]
    public Node2D Entity;
    /// <summary>
    /// subscribes to the screen exited event.
    /// </summary>
    public override void _Ready()
    {
        ScreenExited += Entity.QueueFree;
    }

    /// <summary>
    /// Verifies that all required dependencies and exported fields are correctly assigned.
    /// </summary>
    /// <returns>True if the component is safe to initialize; false if a critical assignment is missing.</returns>
    private bool Setup()
    {
        if (!this.IsAssigned(Entity, nameof(Entity))) return false;
        return true;
    }
}
