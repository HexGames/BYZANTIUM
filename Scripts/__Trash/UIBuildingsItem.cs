using Godot;
/*
public partial class UIBuildingsItem : Control
{
    // is beeing duplicated
    private TextureRect Icon = null;
    private RichTextLabel IconText = null;
    private static string IconText_Original = "";
    private Control LevelBg = null;
    private RichTextLabel Level = null;
    private static string Level_Original = "";
    private Control InConstruction = null;
    private RichTextLabel InConstructionTurns = null;
    private static string InConstructionTurns_Original = "";
    private RichTextLabel ConstructCost = null;
    private static string ConstructCost_Original = "";
    private RichTextLabel ConstructTurns = null;
    private static string ConstructTurns_Original = "";
    private Panel Selected = null;
    private UITooltipTrigger Tooltip = null;

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _BuildingDef;

    Game Game;

    public override void _Ready()
    {
        return;

        Game = GetNode<Game>("/root/Main/Game");

        Icon = GetNode<TextureRect>("Button/Icon");

        IconText = GetNode<RichTextLabel>("Button/IconText");
        if (IconText_Original.Length == 0) IconText_Original = IconText.Text;

        LevelBg = GetNode<Control>("Button/Level");

        Level = GetNode<RichTextLabel>("Button/Level/Level");
        if (Level_Original.Length == 0) Level_Original = Level.Text;

        InConstruction = GetNode<Control>("Button/InConstruction");

        InConstructionTurns = GetNode<RichTextLabel>("Button/InConstruction/Turns");
        if (InConstructionTurns_Original.Length == 0) InConstructionTurns_Original = InConstructionTurns.Text;

        if (HasNode("Cost"))
        {
            ConstructCost = GetNode<RichTextLabel>("Cost");
            if (ConstructCost_Original.Length == 0) ConstructCost_Original = ConstructCost.Text;
            ConstructTurns = GetNode<RichTextLabel>("Time");
            if (ConstructTurns_Original.Length == 0) ConstructTurns_Original = ConstructTurns.Text;
        }

        Selected = GetNode<Panel>("Button/Selected");

        Tooltip = GetNode<UITooltipTrigger>("Button/ToolTip");
    }

    // second parameter for buildings to be constructed, third for in construction buildings
    //public void Refresh(DataBlock buildingDef, SectorData constructInSector, BuildingQueueWrapper.Info inConstructionInfo)
    //{
    //    _BuildingDef = buildingDef;
    //
    //    Icon.Visible = false;
    //
    //    IconText.Text = IconText_Original.Replace("$$", _BuildingDef.GetSub("Icon").ValueS);
    //
    //    string level = "";
    //    DataBlock LevelData = _BuildingDef.GetSub("Level", false);
    //    if (LevelData != null) level = LevelData.ValueS;
    //
    //    if (level != "")
    //    {
    //        Level.Text = Level_Original.Replace("$I", level);
    //        LevelBg.Visible = true;
    //    }
    //    else
    //    {
    //        LevelBg.Visible = false;
    //    }
    //
    //    if (inConstructionInfo != null)
    //    {
    //        InConstructionTurns.Text = InConstructionTurns_Original.Replace("$turns", inConstructionInfo.Turns.ToString());
    //        InConstruction.Visible = true;
    //    }
    //    else
    //    {
    //        InConstruction.Visible = false;
    //    }
    //
    //    if (constructInSector != null)
    //    {
    //        int cost = _BuildingDef.GetSub("Cost").GetSub("Production").ValueI;
    //        if (ConstructCost != null)
    //        {
    //            ConstructCost.Text = ConstructCost_Original.Replace("$value", (cost / 10).ToString());
    //            ConstructCost.Visible = true;
    //        }
    //        if (ConstructTurns != null)
    //        {
    //            ConstructTurns.Text = ConstructTurns_Original.Replace("$time", PlayerHelper.GetBuildTime(Game, constructInSector, cost).ToString());
    //            ConstructTurns.Visible = true;
    //        }
    //    }
    //    else
    //    {
    //        if (ConstructCost != null) ConstructCost.Visible = false;
    //        if (ConstructTurns != null) ConstructTurns.Visible = false;
    //    }
    //
    //    Tooltip.Title = _BuildingDef.ValueS;
    //    Tooltip.Row_1 = "";
    //    Tooltip.Row_1_Right = "";
    //    Tooltip.Row_2 = "";
    //    Tooltip.Row_2_Right = "";
    //
    //    DataBlock benefit = _BuildingDef.GetSub("Benefit", false);
    //    if (benefit != null)
    //    {
    //        string row = "";
    //        for (int idx = 0; idx < benefit.Subs.Count; idx++)
    //        {
    //            if (row.Length > 0)
    //            {
    //                Tooltip.Row_2 += "\n";
    //                Tooltip.Row_2_Right += "\n";
    //            }
    //            row = benefit.Subs[idx].ToUIDescription();
    //            Tooltip.Row_2 += row;
    //            //Tooltip.Row_2 += benefit.Subs[idx].ValueToString() + " " + benefit.Subs[idx].Name;
    //            //Tooltip.Row_2_Right += benefit.Subs[idx].ValueToString();
    //        }
    //    }
    //
    //    Selected.Visible = false;
    //}

    public void OnSelect()
    {
        //Game.GalaxyUI.ColonyBuildings.Select(this);

        Selected.Visible = true;
    }

    public void Deselect()
    {
        Selected.Visible = false;
    }
}*/