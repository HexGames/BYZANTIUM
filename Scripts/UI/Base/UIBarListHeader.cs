using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIBarListHeader : Control
{
    // is beeing duplicated
    private Label GroupName = null;

    [ExportCategory("Runtime")]
    [Export]
    public bool ShowPinned = false;
    [Export]
    public bool ShowHidden = false;

    [Export]
    public int GroupOrder = 0;

    [Export]
    public SectorData _SectorData = null;
    [Export]
    public SystemData _SystemData = null;


    //[Export]
    //public bool AutoLink
    //{
    //    get => false;
    //    set
    //    {
    //        if (value)
    //        {
    //            AutoLinkFunc();
    //        }
    //    }
    //}

    Game Game;

    /*public void AutoLinkFunc()
    {
    }*/

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
            GroupName = GetNode<Label>("GroupNamePanel/GroupName");
            Visible = false;
        }
    }

    public void Refresh(SectorData sector)
    {
        _SectorData = sector;
        Name = _SectorData.SectorName + "_UI";

        GroupName.Text = _SectorData.SectorName;

        Visible = true;
    }

    public void Refresh(SystemData system)
    {
        _SystemData = system;
        Name = _SystemData.SystemName + "_UI";

        GroupName.Text = _SystemData.SystemName;

        Visible = true;
    }
}