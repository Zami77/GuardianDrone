using Godot;
using System;

public class BasicEnemy : Area2D
{
    const int BASIC_ENEMY_MOVE_SPEED = 140;
    Area2D guarded;

    public override void _Ready()
    {
        guarded = GetNode<Area2D>("../GuardedPath2D/GuardedPathFollow2D/Guarded");
    }

    public override void _Process(float delta)
    {
        var velocity = Position.DirectionTo(guarded.GlobalPosition) * BASIC_ENEMY_MOVE_SPEED * delta;
        GlobalPosition += velocity;
    }
}
