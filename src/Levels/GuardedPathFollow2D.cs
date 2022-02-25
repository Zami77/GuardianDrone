using Godot;
using System;

public class GuardedPathFollow2D : PathFollow2D
{
	const int GUARDED_MOVE_SPEED = 150;
	KinematicBody2D guarded;
	public override void _Ready()
	{
		// TODO: get Guarded Node to see if dead to stop movement
		// guarded = GetNode<KinematicBody2D>("./Guarded");
	}

	public override void _Process(float delta)
	{
		Offset += GUARDED_MOVE_SPEED * delta;
	}
}
