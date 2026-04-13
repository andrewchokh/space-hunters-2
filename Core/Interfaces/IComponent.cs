using Godot;
using System;

public interface IComponent
{
    /// <summary>
    /// The parent node this component controls. 
    /// </summary>
    public Node2D Actor { get; }
}
