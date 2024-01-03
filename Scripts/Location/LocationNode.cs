using Godot;
using System;

// Generated
public partial class LocationNode : Node
{
    //[Signal]
    //public delegate void OnSelectEventHandler(LocationNode selectedLocation);

    [ExportCategory("Runtime")]
    [Export]
    public PlayerInput PlayerInput = null;

    [ExportCategory("Generated")]
    [Export]
    public LocationDef Def = null;
    [Export]
    public LocationData Data = null;
    [Export]
    public LocationGFX GFX = null;


    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            PlayerInput = GetNode<PlayerInput>("/root/PlayerInput");
            //OnSelect += PlayerInput.SelectLocation;
        }
    }

    public void Select()
    {
        //GD.Print("You selected " + Def.LocationName);
        //EmitSignal(SignalName.OnSelect);
        PlayerInput.SelectLocation( this );
    }
}
