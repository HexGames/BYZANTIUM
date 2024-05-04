using Godot;
using System;
using System.Collections.Generic;

// Generated
[Tool]
public partial class StarNode : Node
{
    //[Signal]
    //public delegate void OnSelectEventHandler(LocationNode selectedLocation);

    [ExportCategory("Runtime")]
    [Export]
    public PlayerInput PlayerInput = null;

    [ExportCategory("Generated")]
    //[Export]
    //public LocationDef Def = null;
    [Export]
    public StarData Data = null;
    [Export]
    public StarGFX GFX = null;


    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;

        PlayerInput = GetNode<PlayerInput>("/root/Main/PlayerInput");
    }

    public void Select()
    {
        PlayerInput.SelectStar( Data );
    }
}
