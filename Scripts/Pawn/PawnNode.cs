using Godot;
using System;

// Generated
[Tool]
public partial class PawnNode : Node
{
    //[Signal]
    //public delegate void OnSelectEventHandler(LocationNode selectedLocation);

    [ExportCategory("Runtime")]
    [Export]
    public PlayerInput PlayerInput = null;

    [ExportCategory("Generated")]
    [Export]
    public PawnDef Def = null;
    [Export]
    public PawnData Data = null;
    [Export]
    public PawnGFX GFX = null;


    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            PlayerInput = GetNode<PlayerInput>("/root/Main/PlayerInput");
            //OnSelect += PlayerInput.SelectLocation;
        }
    }

    public void Select()
    {
        //GD.Print("You selected " + Def.LocationName);
        //EmitSignal(SignalName.OnSelect);
        //PlayerInput.SelectLocation( this );
    }
}
