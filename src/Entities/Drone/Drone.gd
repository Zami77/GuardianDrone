extends KinematicBody2D

var drone_speed: int = 300
var input_vector: Vector2 = Vector2.ZERO
var facing_left: bool = false

onready var sprite: Sprite = get_node("Sprite")
# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

func handle_input(delta: float):
	input_vector.x = Input.get_action_strength("move_right") - Input.get_action_strength("move_left")
	input_vector.y = Input.get_action_strength("move_down") - Input.get_action_strength("move_up")
	
	global_position += input_vector.normalized() * drone_speed * delta
	
	if Input.is_action_just_pressed("switch_dir"):
		facing_left = !facing_left
		sprite.set_flip_h(facing_left)

func _process(delta):
	handle_input(delta)
