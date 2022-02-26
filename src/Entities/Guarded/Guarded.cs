using Godot;
using System;

public class Guarded : KinematicBody2D, IGuarded
{
	const int GUARDED_MAX_HEALTH = 50;
	int guardedHealth = GUARDED_MAX_HEALTH;
	AnimationPlayer effectsPlayer;
	public override void _Ready()
	{
		effectsPlayer = GetNode<AnimationPlayer>("EffectsPlayer");
	}
	public void TakeDamage(int dmg)
	{
		guardedHealth = Math.Max(guardedHealth - dmg, 0);
		effectsPlayer.Play("TakeDamage");
	}

	public bool IsDead() => guardedHealth == 0;

	public override void _Process(float delta)
	{
		if (IsDead())
		{
			// TODO: Investigate better way to die than hide
			Hide();
		}
	}
}
