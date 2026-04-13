using Godot;
using System;

public interface IDescriptible
{
    public string DisplayName { get; }
    public string Description { get; }
}
