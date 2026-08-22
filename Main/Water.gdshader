shader_type spatial;

uniform vec3 water_color : source_color = vec3(0.0, 0.4, 0.8);
uniform float roughness : hint_range(0.0, 0.1) = 0.1;

void fragment() {
	ALBEDO = water_color;
	ALPHA = 0.7;
	ROUGHNESS = roughness;
	METALLIC = 0.1;
}
