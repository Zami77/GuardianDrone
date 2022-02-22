using Godot;
using System;

public class GuardedPathFollow2D : PathFollow2D
{
	const int GUARDED_MOVE_SPEED = 150;
	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
	{
		Offset += GUARDED_MOVE_SPEED * delta;
	}
}
