using Godot;
using System;

public class Guarded : KinematicBody2D, IGuarded
{
	[Signal]
	delegate void PlayerTakeDamage(int curHealth);
	[Signal]
	delegate void PlayerInitHealth(int curHealth, int maxHealth);
	[Export]
	public bool IsInvincible = false;
	const int GUARDED_MAX_HEALTH = 50;
	int guardedHealth = GUARDED_MAX_HEALTH;
	AnimationPlayer effectsPlayer;
	public override void _Ready()
	{
		guardedHealth = IsInvincible ? int.MaxValue : guardedHealth;
		effectsPlayer = GetNode<AnimationPlayer>("EffectsPlayer");
		EmitSignal(nameof(PlayerInitHealth), guardedHealth, GUARDED_MAX_HEALTH);
	}
	public void TakeDamage(int dmg)
	{
		guardedHealth = Math.Max(guardedHealth - dmg, 0);
		effectsPlayer.Play(AnimationType.TakeDamage);
		this.EmitSignal(nameof(PlayerTakeDamage), guardedHealth);
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
