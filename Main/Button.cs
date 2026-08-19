using Godot;
using System;

public partial class Button : StaticBody3D
{
	public void Interact()
    {
        GD.Print("you clicked me!");
    }
}
