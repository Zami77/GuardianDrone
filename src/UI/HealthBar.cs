using Godot;
using System;

public class HealthBar : Control
{
	TextureProgress healthBarOver;
	TextureProgress healthBarUnder;
	Tween updateTween;
	[Export]
	Color healtyColor = Color.ColorN("green");
	[Export]
	Color cautionColor = Color.ColorN("yellow");
	[Export]
	Color dangerColor = Color.ColorN("red");
	[Export(PropertyHint.Range, "float, 0, 1, 0.05")]
	float cautionZone = 0.5f;
	[Export(PropertyHint.Range, "float, 0, 1, 0.05")]
	float dangerZone = 0.2f;


	public override void _Ready()
	{
		healthBarOver = GetNode<TextureProgress>("HealthBarOver");
		healthBarUnder = GetNode<TextureProgress>("HealthBarUnder");
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
}
