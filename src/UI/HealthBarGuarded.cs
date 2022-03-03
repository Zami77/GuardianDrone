using Godot;
using System;

public class HealthBarGuarded : HealthBar
{
	
	/* Signal Checks */

	private void _on_Guarded_PlayerTakeDamage(int curHealth)
	{
		base.OnHealthUpdated(curHealth);
	}

	private void _on_Guarded_PlayerInitHealth(int curHealth, int maxHealth)
	{
		base.OnMaxHealthUpdated(maxHealth);
		base.OnHealthUpdated(curHealth);
	}
}
