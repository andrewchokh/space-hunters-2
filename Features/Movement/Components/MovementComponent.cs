using Godot;
using System;

/// <summary>
/// Component responsible for discrete object movement between fixed horizontal lanes.
/// </summary>
/// <remarks>
/// This system decouples input and target calculation from the visual representation.
/// It relies on <see cref="MapManager"/> to translate row indices into Y-coordinates
/// and uses <see cref="Mathf.Lerp"/> to provide smooth transitions between lanes.
/// </remarks>
[GlobalClass]
public abstract partial class MovementComponent : Node2D, IComponent
{
    [Export]
    public MovePattern Pattern;

    public Node2D Actor => GetParent() as Node2D;
}
