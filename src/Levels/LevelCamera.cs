using Godot;
using System;

public class LevelCamera : Camera2D
{
	PathFollow2D guarded;
	
	public override void _Ready()
	{
		guarded = GetNode<PathFollow2D>("../GuardedPath2D/GuardedPathFollow2D");
	}

	public override void _Process(float delta)
	{
		Position = new Vector2(guarded.GlobalPosition.x, Position.y);
	}
}
