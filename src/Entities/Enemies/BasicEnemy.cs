using Godot;
using System;

public class BasicEnemy : KinematicBody2D, IDamageable
{
	const int BASIC_ENEMY_MOVE_SPEED = 140;
	const string TAKE_DAMAGE_ANIM = "TakeDamage";
	int enemyHealth = 50;
	KinematicBody2D guarded;
	AnimationPlayer animationPlayer;

	public override void _Ready()
	{
		guarded = GetNode<KinematicBody2D>("../GuardedPath2D/GuardedPathFollow2D/Guarded");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

    public void TakeDamage(int dmg)
    {
    	enemyHealth = Math.Max(0, enemyHealth - dmg);
		animationPlayer.Play(TAKE_DAMAGE_ANIM);
    }

	private bool isDead() => enemyHealth == 0;

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
