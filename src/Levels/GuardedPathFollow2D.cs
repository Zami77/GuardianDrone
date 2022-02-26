using Godot;
using System;

public class GuardedPathFollow2D : PathFollow2D
{
	const int GUARDED_MOVE_SPEED = 150;
	IGuarded guarded;
	public override void _Ready()
	{
		guarded = GetNode<IGuarded>("./Guarded");
	}

	public override void _Process(float delta)
	{
		if (!guarded.IsDead())
		{
			Offset += GUARDED_MOVE_SPEED * delta;
		}
	}
}
