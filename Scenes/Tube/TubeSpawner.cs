using Godot;
using System;

public partial class TubeSpawner : Node3D
{
	[Export] private Vector2 Gap { get; set; } = new(2, -2);
	[Export] private Vector2I Count { get; set; } = new(10, 10);
	[ExportGroup("Dependencies")] 
	[Export] private PackedScene Tube { get; set; } = null!;

	public override void _Ready()
	{
		for (int y = 0; y < Count.Y; y++)
		{
			for (int x = 0; x < Count.X; x++)
			{
				if (y % 2 == 0 && x == Count.X - 1)
				{
					continue;
				}

				Node3D tube = Tube.Instantiate<Node3D>();
				Vector3 spawnPosition = new(x * Gap.X, y * Gap.Y, 0);
				if (y % 2 == 0)
				{
					spawnPosition.X += (Gap.X / 2);
				}

				tube.Position = spawnPosition;
				AddChild(tube);
			}
		}
	}
}
