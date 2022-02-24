using Godot;
using System;

public class BasicEnemy : KinematicBody2D, IDamageable
{
	const int BASIC_ENEMY_MOVE_SPEED = 140;
	int enemyHealth = 20;
	KinematicBody2D guarded;

	public override void _Ready()
	{
		guarded = GetNode<KinematicBody2D>("../GuardedPath2D/GuardedPathFollow2D/Guarded");
	}

    public void TakeDamage(int dmg)
    {
        enemyHealth -= dmg;
		GD.Print($"Super health:{enemyHealth}");
    }

	private bool isDead() => enemyHealth <= 0;
    public override void _Process(float delta)
	{
		if (isDead())
		{
			QueueFree();
		}
		
		var velocity = Position.DirectionTo(guarded.GlobalPosition) * BASIC_ENEMY_MOVE_SPEED * delta;
		GlobalPosition += velocity;
	}
}
