using Godot;
using System;

public partial class LinearProjectile : Projectile
{
    public override void Enter(Projectile projectile) 
    {
        return;
    }

    public override void Execute(Projectile projectile, double delta) 
    {
        GlobalPosition += new Vector2(Speed * (float) delta, 0);
    } 

    public override void Exit(Projectile projectile) 
    {
        return;
    }
}
