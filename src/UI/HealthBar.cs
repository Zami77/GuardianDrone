using Godot;
using System;

public class HealthBar : Control
{
	TextureProgress healthBarOver;
	TextureProgress healthBarUnder;
	Tween updateTween;
	[Export]
	Color healtyColor = Color.ColorN("Green");
	[Export]
	Color cautionColor = Color.ColorN("Yellow");
	[Export]
	Color dangerColor = Color.ColorN("Red");
	[Export(PropertyHint.Range, "float, 0, 1, 0.05")]
	float cautionZone = 0.5f;
	[Export(PropertyHint.Range, "float, 0, 1, 0.05")]
	float dangerZone = 0.2f;


	public override void _Ready()
	{
		healthBarOver = GetNode<TextureProgress>("./HealthBarOver");
		healthBarUnder = GetNode<TextureProgress>("./HealthBarUnder");
		healthBarUnder.TintProgress = dangerColor;
		updateTween = GetNode<Tween>("UpdateTween");
	}

	public void OnHealthUpdated(int curHealth)
	{
		healthBarOver.Value = curHealth;
		updateTween.InterpolateProperty(healthBarUnder, "value", healthBarUnder.Value, curHealth, 0.5f);
		updateTween.Start();
		assignColor(curHealth);
	}

	private void assignColor(int curHealth)
	{
		if (curHealth < healthBarOver.MaxValue * dangerZone)
		{
			healthBarOver.TintProgress = dangerColor;
		}
		else if (curHealth < healthBarOver.MaxValue * cautionZone)
		{
			healthBarOver.TintProgress = cautionColor;
		}
		else
		{
			healthBarOver.TintProgress = healtyColor;
		}
	}

	public void OnMaxHealthUpdated(int maxHealth)
	{
		healthBarOver.MaxValue = maxHealth;
		healthBarUnder.MaxValue = maxHealth;
	}

	/* Signal Checks */

	private void _on_Guarded_PlayerTakeDamage(int curHealth)
	{
		OnHealthUpdated(curHealth);
	}

	private void _on_Guarded_PlayerInitHealth(int curHealth, int maxHealth)
	{
		OnMaxHealthUpdated(maxHealth);
		OnHealthUpdated(curHealth);
	}
}
