using Godot;
using System;

public class Drone : KinematicBody2D
{
	int drone_speed = 300;
	Vector2 inputVector = Vector2.Zero;
	bool facingLeft = false;
	Sprite droneSprite;
	
	public override void _Ready()
	{
		droneSprite = GetNode<Sprite>("Sprite");
	}
	
	private void handleInput(float delta)
	{
		inputVector.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		inputVector.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

		GlobalPosition += inputVector.Normalized() * drone_speed * delta;

		if (Input.IsActionJustPressed("switch_dir")) 
		{
			facingLeft = !facingLeft;
			droneSprite.FlipH = facingLeft;
		}
	}

	public override void _Process(float delta)
	{
		handleInput(delta);
	}
}
