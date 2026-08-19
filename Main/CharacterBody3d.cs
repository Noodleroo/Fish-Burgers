using Godot;

public partial class CharacterBody3d : CharacterBody3D
{
    [Export]
    public float MouseSensitivity { get; set; } = 0.002f;

    private Camera3D _camera;
    private RayCast3D _interactionRay;
    private Label _crosshair;

    public const float Speed = 5.0f;
    public const float JumpVelocity = 4.5f;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;

        _camera = GetNode<Camera3D>("PlayerCamera");
        _interactionRay = GetNode<RayCast3D>("PlayerCamera/RayCast3D");
        _crosshair = GetNode<Label>("%Crosshair");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            RotateY(-mouseMotion.Relative.X * MouseSensitivity);
            _camera.RotateX(-mouseMotion.Relative.Y * MouseSensitivity);

            Vector3 rotation = _camera.Rotation;
            rotation.X = Mathf.Clamp(rotation.X, Mathf.DegToRad(-85f), Mathf.DegToRad(85f));
            _camera.Rotation = rotation;
        }

        if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
        {
            GD.Print("Mouse clicked, Raycast collison status: ", _interactionRay.IsColliding());

            if (_interactionRay.IsColliding())
            {
                GodotObject hitObject = _interactionRay.GetCollider();
                GD.Print("Raycast hit this node: ", hitObject);

                if (hitObject.HasMethod("Interact"))
                {
                    hitObject.Call("Interact");
                }
                else
                {
                    GD.Print("The node we hit does NOT have an interact method");
                }
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }

        // Handle jump.
        // if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        // {
        //     velocity.Y = JumpVelocity;
        // }

        // Get the input direction and handle the movement/deceleration.
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();

        if (_interactionRay.IsColliding() && _interactionRay.GetCollider().HasMethod("Interact"))
        {
            _crosshair.Text = "◯";
            _crosshair.AddThemeColorOverride("font_color", Colors.White);
        }
        else
        {
            _crosshair.Text = "⬤";
            _crosshair.AddThemeColorOverride("font_color", Colors.White);
        }
    }
}