using Godot;
using System;

public partial class Game : Node
{
    [Export]
    public MapCamera Camera;
    [Export]
	public PlayerInput PlayerInput;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerInput = GetNode<PlayerInput>("/root/PlayerInput");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//}
}
