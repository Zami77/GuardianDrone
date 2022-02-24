using Godot;
using System;

public class Drone : KinematicBody2D
{
	const int DRONE_SPEED = 300;
	const float FIRE_RATE = 0.25f;
	Vector2 inputVector = Vector2.Zero;
	bool facingLeft = false;
	Sprite droneSprite;
	PackedScene basicLaser;
	Node2D firePoint;
	float fireTime;
	
	
	public override void _Ready()
	{
		droneSprite = GetNode<Sprite>("Sprite");
		firePoint = GetNode<Node2D>("FirePoint");
		basicLaser = (PackedScene)ResourceLoader.Load("res://src/Entities/Drone/BasicLaser.tscn");
	}

	// Gets time in seconds
	private float getTime() => OS.GetTicksMsec() / 1000.0f;

	private void shoot()
	{
		if (getTime() - fireTime < FIRE_RATE)
		{
			return;
		}

		fireTime = getTime();
		BasicLaser laser = basicLaser.Instance<BasicLaser>();
		GetTree().Root.AddChild(laser);
		laser.Setup(facingLeft);
		laser.GlobalPosition = firePoint.GlobalPosition;
	}
	
	private void handleInput(float delta)
	{
		inputVector.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		inputVector.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

		GlobalPosition += inputVector.Normalized() * DRONE_SPEED * delta;

		if (Input.IsActionJustPressed("switch_dir")) 
		{
			facingLeft = !facingLeft;
			droneSprite.FlipH = facingLeft;
		}

		if (Input.IsActionPressed("fire"))
		{
			shoot();
		}
	}

	public override void _Process(float delta)
	{
		handleInput(delta);
	}
}
