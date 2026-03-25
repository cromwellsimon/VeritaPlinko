using Godot;
using System;

public partial class Ball : Node3D
{
	[Export] private Vector2 Boundary { get; set; } = new(-10, 10);
	
	[ExportGroup("Dependencies")]
	[Export] private RigidBody3D RigidBody { get; set; } = null!;
	
	private bool IsDropped { get; set; }
	private Vector3 StartingPosition { get; set; }

	public override void _Ready()
	{
		StartingPosition = RigidBody.GlobalPosition;
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}

	public override void _Process(double delta)
	{
		if (!IsDropped)
		{
			Move();
			if (Input.IsMouseButtonPressed(MouseButton.Left))
			{
				Drop();
			}
		}
	}

	private void Drop()
	{
		RigidBody.LinearVelocity = new Vector3(0, 0, 0);
		RigidBody.SetFreezeEnabled(false);
		IsDropped = true;
	}

	private void Move()
	{
		RigidBody.SetFreezeEnabled(true);
		
		Camera3D camera = GetViewport().GetCamera3D();
		Vector2 mousePos = GetViewport().GetMousePosition();

		Vector3 rayOrigin = camera.ProjectRayOrigin(mousePos);
		Vector3 rayDir = camera.ProjectRayNormal(mousePos);

		Plane plane = new Plane(Vector3.Forward, 0);

		Vector3? hitPoint = plane.IntersectsRay(rayOrigin, rayDir);

		if (hitPoint.HasValue)
		{
			RigidBody.SetGlobalPosition(new(
				Math.Clamp(hitPoint.Value.X, Boundary.X, Boundary.Y),
				StartingPosition.Y,
				StartingPosition.Z
			));
		}
	}

	public void Reset() => IsDropped = false;
}
