using Godot;
using System;
using R3;

public partial class Goal : Node3D
{
	[Export] public int Value { get; set; } = 100;
	[Export] public Color Color { get; set; } = Colors.LawnGreen;
	
	[ExportGroup("Dependencies")]
	[Export] private Area3D CollisionArea { get; set; } = null!;
	[Export] private MeshInstance3D Mesh { get; set; } = null!;

	[Signal] public delegate void GoalEnteredEventHandler(int value);
	
	public override void _Ready()
	{
		Material material = (Material)Mesh.GetSurfaceOverrideMaterial(0).Duplicate();
		material.Set("albedo_color", Color);
		material.Set("emission", Color);
		Mesh.SetSurfaceOverrideMaterial(0, material);
		CollisionArea.BodyEntered += (other) =>
		{
			if (other.GetParent() is Ball)
			{
				EmitSignalGoalEntered(Value);
			}
		};
	}
}
