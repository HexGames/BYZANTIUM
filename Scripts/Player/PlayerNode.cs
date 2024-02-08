using Godot;
using System;

// Generated
[Tool]
public partial class PlayerNode : Node
{
    [ExportCategory("Generated")]
    [Export]
    public PlayerData Data = null;
    [Export]
    public Color GFX = new Color(1, 0, 1, 1);

    //[ExportCategory("Runtime")]
    //[Export]
    //public Game Game = null;


    public override void _Ready()
    {
        //if (!Engine.IsEditorHint())
        //{
        //    Game = GetNode<Game>("/root/Main/Game");
        //}
    }

    public void Select()
    {
    }
}
