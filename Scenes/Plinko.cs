using Godot;
using System;
using System.Diagnostics;
using System.Linq;
using VeritaPlinko.Scripts;

public partial class Plinko : Node3D
{
	[ExportGroup("Dependencies")]
	[Export] private Ball Ball { get; set; } = null!;
	[Export] private Node3D Goals { get; set; } = null!;
	[Export] private UI UI { get; set; } = null!;
	
	public override void _Ready()
	{
		foreach (Goal goal in Goals.GetChildrenRecursively().Where((node) => node is Goal).Cast<Goal>())
		{
			goal.GoalEntered += (value) =>
			{
				UI.Score.Value += value;
				Ball.Reset();
			};
		}
	}
}
