using Godot;
using System;

/// <summary>
/// Automatically destroys a designated entity when this notifier exits the visible screen area.
/// </summary>
/// <remarks>
/// A self-contained cleanup tool. Attach this to moving scenes (like enemies or projectiles)
/// to automatically prevent memory leaks when they leave the battlefield.
/// </remarks>
public partial class DisposableComponent : VisibleOnScreenNotifier2D, IComponent
{
    public Node2D Actor => GetParent() as Node2D;

    /// <summary>
    /// subscribes to the screen exited event.
    /// </summary>
    public override void _Ready()
    {
        ScreenExited += Actor.QueueFree;
    }
}
