using Godot;
using System;

public class Drone : KinematicBody2D, IDrone
{
	[Signal]
	delegate void DroneTakeDamage(int curHealth);
	[Signal]
	delegate void DroneInitHealth(int curHealth, int maxHealth);
	const int DRONE_SPEED = 300;
	const float FIRE_RATE = 0.25f;
	const int DRONE_HEALTH = 100;
	int curDroneHealth = DRONE_HEALTH;
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
		EmitSignal(nameof(DroneInitHealth), curDroneHealth, DRONE_HEALTH);
	}	

	private void shoot()
	{
		fireTime = Helper.GetTime();
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

		if (Input.IsActionPressed("fire") && Helper.IsCooledDown(fireTime, FIRE_RATE))
		{
			shoot();
		}
	}

	public void TakeDamage(int dmg)
	{
		curDroneHealth = Math.Max(curDroneHealth - dmg, 0);
		// TODO: Set up take damage animation for drone
		// effectsPlayer.Play(AnimationType.TakeDamage);
		this.EmitSignal(nameof(DroneTakeDamage), curDroneHealth);
	}

	public bool IsDead() => curDroneHealth == 0;

	public override void _Process(float delta)
	{
		handleInput(delta);
	}
}
