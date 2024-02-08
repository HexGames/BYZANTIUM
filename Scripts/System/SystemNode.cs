using Godot;
using System;
using System.Collections.Generic;

// Generated
[Tool]
public partial class SystemNode : Node
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
    public SystemData Data = null;
    [Export]
    public SystemGFX GFX = null;


    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;

        PlayerInput = GetNode<PlayerInput>("/root/Main/PlayerInput");
        //OnSelect += PlayerInput.SelectLocation;
    }

    public void Select()
    {
        //GD.Print("You selected " + Def.LocationName);
        //EmitSignal(SignalName.OnSelect);
        PlayerInput.SelectLocation( this );
    }
}
