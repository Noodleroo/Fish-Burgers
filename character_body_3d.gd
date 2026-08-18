extends CharacterBody3D
@export var mouse_sensitivity: float = 0.002

@onready var camera: Camera3D = $PlayerCamera
@onready var interaction_ray : RayCast3D = $PlayerCamera/RayCast3D
@onready var crosshair : Label = %Crosshair

const SPEED = 5.0
const JUMP_VELOCITY = 4.5

func _ready() -> void:
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
	#Hides mouse 

	
func _unhandled_input(event: InputEvent) -> void:
	if event is InputEventMouseMotion:
		rotate_y(-event.relative.x * mouse_sensitivity)
		camera.rotate_x(-event.relative.y * mouse_sensitivity)
		camera.rotation.x = clamp(camera.rotation.x, deg_to_rad(-85), deg_to_rad(85))
	if (event is InputEventMouseButton and event.button_index == MOUSE_BUTTON_LEFT and event.pressed):
		print("Mouse clicked, Raycast collison status: ", interaction_ray.is_colliding())
		if interaction_ray.is_colliding():
			var hit_object = interaction_ray.get_collider()
			print("Raycast hit this node: ", hit_object)
			if hit_object.has_method("interact"):
				hit_object.interact()
			else:
				print("The node we hit does NOT have an interact method")

func _physics_process(delta: float) -> void:
	# Add the gravity.
	if not is_on_floor():
		velocity += get_gravity() * delta
	# Handle jump.
	#if Input.is_action_just_pressed("ui_accept") and is_on_floor():
	#	velocity.y = JUMP_VELOCITY

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var input_dir := Input.get_vector("ui_left", "ui_right", "ui_up", "ui_down")
	var direction := (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	if direction:
		velocity.x = direction.x * SPEED
		velocity.z = direction.z * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		velocity.z = move_toward(velocity.z, 0, SPEED)

	move_and_slide()
	
	
	if interaction_ray.is_colliding() and interaction_ray.get_collider().has_method("interact"):
		crosshair.text = "◯"
		crosshair.add_theme_color_override("font_color", Color.WHITE)
	else:
		crosshair.text = "⬤"
		crosshair.add_theme_color_override("font_color", Color.WHITE)
	
	
	

		
	
	
		
