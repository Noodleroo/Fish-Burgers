using Godot;
using System;

public partial class RainbowBox3D : MeshInstance3D
{
	public float CycleSpeed { get; set; } = 0.5f;
	
	private StandardMaterial3D _material;
	private float _hue = 0.0f;
	public override void _Ready()
	{
		_material = new StandardMaterial3D();

		MaterialOverride = _material;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_hue += CycleSpeed * (float)delta;

		if (_hue > 1.0f)
		{
			_hue -= 1.0f;
		}

		Color rainbowColor = Color.FromHsv(_hue, 1.0f, 1.0f);

		_material.AlbedoColor = rainbowColor;
	}
}
