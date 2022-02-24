using Godot;
using System;

public class BasicLaser : KinematicBody2D
{
	const int LASER_SPEED = 400;
	int laserDamage = 10;
	bool facingLeft = false;
	Sprite bulletSprite;
	VisibilityNotifier2D visibilityNotifier;

	public override void _Ready()
	{
		bulletSprite = GetNode<Sprite>("Sprite");
		visibilityNotifier = GetNode<VisibilityNotifier2D>("VisibilityNotifier2D");
	}
	public void Setup(bool _facingLeft)
	{
		facingLeft = _facingLeft;
		bulletSprite.FlipH = facingLeft;
	}
	public override void _Process(float delta)
	{
		if (!visibilityNotifier.IsOnScreen())
		{
			QueueFree();
		}	

		var bulletDir = facingLeft ? Vector2.Left : Vector2.Right;
		var collision = MoveAndCollide(bulletDir * delta * LASER_SPEED);

		if (collision?.Collider is IDamageable iDamageable)
		{
			iDamageable.TakeDamage(laserDamage);
			QueueFree();
		}
	}
}
