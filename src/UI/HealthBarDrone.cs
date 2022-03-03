using Godot;
using System;

public class HealthBarDrone : HealthBar
{
	/* Signal Checks */

	private void _on_Drone_DroneInitHealth(int curHealth, int maxHealth)
	{
		base.OnMaxHealthUpdated(maxHealth);
		base.OnHealthUpdated(curHealth);
	}


	private void _on_Drone_DroneTakeDamage(int curHealth)
	{
		base.OnHealthUpdated(curHealth);
	}
}

