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
        float speed = SpaceshipData.ProjectileSpeed;

        if (HorizontalDirection == HDirection.Left)
            speed *= -1;

        GlobalPosition += new Vector2(speed * (float) delta, 0);
    } 

    public override void Exit(Projectile projectile) 
    {
        return;
    }
}
