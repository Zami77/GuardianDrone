using Godot;
using System;

public class BasicEnemy : KinematicBody2D, IEnemy
{
	const int BASIC_ENEMY_MOVE_SPEED = 155;
	const int BASIC_ENEMY_DAMAGE = 5;
	const float BASIC_ENEMY_DAMAGE_RATE = 0.25f;
	
	int enemyHealth = 50;
	KinematicBody2D guarded;
	AnimationPlayer effectsPlayer;
	float attackTime;

	public override void _Ready()
	{
		guarded = GetNode<KinematicBody2D>("../GuardedPath2D/GuardedPathFollow2D/Guarded");
		effectsPlayer = GetNode<AnimationPlayer>("EffectsPlayer");
	}

	public void TakeDamage(int dmg)
	{
		enemyHealth = Math.Max(0, enemyHealth - dmg);
		effectsPlayer.Play(AnimationType.TakeDamage);
	}

	public bool IsDead() => enemyHealth == 0;

	public void Attack(IFriendly ifriendly)
	{
		ifriendly.TakeDamage(BASIC_ENEMY_DAMAGE);
		attackTime = Helper.GetTime();
	}

	private void handleMovement(float delta)
	{
		var velocity = Position.DirectionTo(guarded.GlobalPosition) * BASIC_ENEMY_MOVE_SPEED * delta;

		var collision = MoveAndCollide(velocity);

		if (collision?.Collider is IFriendly ifriendly && Helper.IsCooledDown(attackTime, BASIC_ENEMY_DAMAGE_RATE))
		{
			if (!ifriendly.IsDead())
			{
				Attack(ifriendly);
			}
		}
	}

	public override void _Process(float delta)
	{
		if (IsDead())
		{
			QueueFree();
		}
		
		handleMovement(delta);
	}
}
