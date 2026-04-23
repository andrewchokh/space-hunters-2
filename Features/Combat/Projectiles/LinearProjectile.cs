using Godot;
using System;

public partial class LinearProjectile : Projectile
{
    public override void Execute(Projectile projectile, double delta) =>        
        GlobalPosition += new Vector2(Speed * (float) delta, 0);
}
