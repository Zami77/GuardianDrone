using Godot;
using System;

public class Guarded : KinematicBody2D, IGuarded
{
	const int GUARDED_MAX_HEALTH = 50;
	int guardedHealth = GUARDED_MAX_HEALTH;
	public override void _Ready()
	{
		
	}
	public void TakeDamage(int dmg)
	{
		guardedHealth = Math.Max(guardedHealth - dmg, 0);
		GD.Print("Player hit");
		// TODO: Play blink animation
	}

	public bool IsDead() => guardedHealth == 0;

	public override void _Process(float delta)
	{
		if (IsDead())
		{
			Hide();
		}
	}
}
