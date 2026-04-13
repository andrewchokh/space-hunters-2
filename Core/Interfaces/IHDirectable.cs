using Godot;
using System;

public enum HDirection 
{
    Left,
    Right 
}

public interface IHDirectable
{
    public HDirection HorizontalDirection { get; }
}
