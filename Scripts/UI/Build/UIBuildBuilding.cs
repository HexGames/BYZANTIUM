using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIBuildBuilding : Control
{
    // is beeing duplicated
    private RichTextLabel Building;
    private static string Building_Original = "";
    private RichTextLabel Benefit;
    private static string Benefit_Original = "";
    private RichTextLabel Turns;
    private static string Turns_Original = "";
    private Panel Selected;

    //[ExportCategory("Runtime")]
    public ActionTargetInfo _Building = null;
    //
    public List<ActionTargetInfo> LocationActions = new List<ActionTargetInfo>();

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        Building = GetNode<RichTextLabel>("Container/Container/VBoxContainer/Benefit/Title");
        if (Building_Original.Length == 0) Building_Original = Building.Text;

        Benefit = GetNode<RichTextLabel>("Container/Container/VBoxContainer/Benefit/");
        if (Benefit_Original.Length == 0) Benefit_Original =  Benefit.Text;

        Turns = GetNode<RichTextLabel>("Container/Container/VBoxContainer/Turns");
        if (Turns_Original.Length == 0) Turns_Original = Turns.Text;

        Selected = GetNode<Panel>("Container/Panel");
    }

    public void Refresh(ActionTargetInfo buildingTarget)
    {
        _Building = buildingTarget;
        LocationActions.Add(_Building);

        Building.Text = Building_Original.Replace("$name", _Building.Name);

        Benefit.Text = Benefit_Original.Replace("$value", _Building.Benefit.GetAllString());

        Turns.Text = Turns_Original.Replace("$turns", _Building.Turns.ToString());

        Selected.Visible = false;
    }

    //public void AddAditionalLocationAction(ActionTargetInfo buildingTarget)
    //{
    //    LocationActions.Add(buildingTarget);
    //}

    public void OnSelect()
    {
        Game.WindowsUI.BuildWindow.Select(this);
        Selected.Visible = true;
    }

    public void Deselect()
    {
        Selected.Visible = false;
    }
}