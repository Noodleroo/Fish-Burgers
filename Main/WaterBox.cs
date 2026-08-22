using Godot;
using System;
using System.Drawing;

[Tool]
public partial class WaterBox : Area3D
{
	[Export] public Vector3 BoxSize { get; set; } = new Vector3(10,10,10);
	[Export] public float Drag { get; set; } = 2.0f;

	private MeshInstance3D _mesh;
	private CollisionShape3D _collisionShape;

    public override void _Ready()
    {
        _mesh = GetNode<MeshInstance3D>("MeshInstance3D");
		_collisionShape = GetNode<CollisionShape3D>("CollisionShape3D");

		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;

		UpdateDimensions();
    }

	private void UpdateDimensions()
	{
		if (_mesh?.Mesh is BoxMesh boxMesh)
		{
			boxMesh.Size = BoxSize;
		}
		if(_collisionShape?.Shape is BoxShape3D boxShape)
		{
			boxShape.Size = BoxSize;
		}
	}

	private void OnBodyEntered(Node3D body)
	{
		if (body is RigidBody3D rigidBody)
		{
			rigidBody.LinearDamp += Drag;
		}
	}

	private void OnBodyExited(Node3D body)
	{
		if (body is RigidBody3D rigidBody)
		{
			rigidBody.LinearDamp = Mathf.Max(0, rigidBody.LinearDamp - Drag);
		}
	}

}
