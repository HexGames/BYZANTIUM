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

    public void Select(bool sector = false)
    {
        if (sector && Data.System?._Sector != null)
        {
            PlayerInput.SelectSector(Data.System._Sector);
        }
        else
        {
            PlayerInput.SelectStar(Data);
        }        
    }

    public void SelectFleet(bool friendlyToStar)
    {
        PlayerInput.SelectFleet(Data, friendlyToStar);

        //if ()
        //{
        //    PlayerInput.SelectSector(Data.System._Sector);
        //}
        //else
        //{
        //    PlayerInput.SelectStar(Data);
        //}
    }
}
