using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIBuildLocation : Control
{
    // is beeing duplicated
    private RichTextLabel Location;
    private static string Location_Original = "";
    private Panel Selected;

    //[ExportCategory("Runtime")]
    //[Export]
    public ActionTargetInfo _ActionTarget = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        Location = GetNode<RichTextLabel>("Container/Container/VBoxContainer/Location");
        if (Location_Original.Length == 0) Location_Original = Location.Text;

        Selected = GetNode<Panel>("Container/Panel");
    }

    public void Refresh(ActionTargetInfo _actionTarget)
    {
        _ActionTarget = _actionTarget;

        Location.Text = Location_Original.Replace("$location", "at " + _ActionTarget._Planet.PlanetName + " in " + _ActionTarget._Planet._Star.StarName + " System");
    }
    public void OnSelect()
    {

    }
}