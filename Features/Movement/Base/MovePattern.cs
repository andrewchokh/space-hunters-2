using Godot;
using System;

[GlobalClass]
public abstract partial class MovePattern : Resource
{
    [Export]
    public float MoveSpeed;

    public abstract void Execute(CharacterBody2D actor, double delta);
}
