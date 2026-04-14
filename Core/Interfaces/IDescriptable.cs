using Godot;
using System;

public interface IDescriptable
{
    public string DisplayName { get; }
    public string Description { get; }
}
