shader_type spatial;

uniform vec3 basic_color : source_color = vec3(1.273, 1.312, 1.645);

void fragment() {
	ALBEDO = basic_color;
	
	METALLIC = 0.5;
	
	ROUGHNESS = 0.2;
}
