using Godot;
using Godot.Collections;

public partial class UIBuildings : Control
{
    [ExportCategory("Links")]
    [Export]
    public Control BuildingPresent = null;
    [Export]
    public Array<UIBuildingsItem> Prefix = null;
    [Export]
    public Control PrefixSeparator = null;
    [Export]
    public Array<UIBuildingsItem> Main = null;
    [Export]
    public Control ResourcesSeparator = null;
    [Export]
    public Array<UIBuildingsItem> Resources = null;
    [Export]
    public Control Colonize = null;
    [Export]
    public Button ColonizButton = null;
    [Export]
    public UITooltipTrigger ColonizeButton_Tooltip = null;
    private string ColonizeButton_Tooltip_Original = null;
    [Export]
    private RichTextLabel ColonizeCost = null;
    private string ColonizeCost_Original = "";
    [Export]
    private RichTextLabel ColonizeTime = null;
    private string ColonizeTime_Original = "";
    [Export]
    public Control Colonizing = null;
    [Export]
    public Panel ColonizingProgressBG = null;
    [Export]
    public ProgressBar ColonizingProgressCurrent = null;
    [Export]
    public ProgressBar ColonizingProgressNextTurn = null;
    [Export]
    public RichTextLabel ColonizingTime = null;
    private string ColonizingTime_Original = "";
    [Export]
    public Control BuildingNone = null;

    [Export]
    public Control Title = null;
    [Export]
    public Control UpgradeLocation = null;
    [Export]
    public Array<UIBuildingsItem> Upgrades = null;
    [Export]
    public Control UpgradingLocation = null;
    [Export]
    public Button UpgradingPlus = null;
    [Export]
    public Button UpgradingMinus = null;
    [Export]
    public RichTextLabel UpgradingQueue = null;
    private string UpgradingQueue_Original = "";
    [Export]
    public RichTextLabel UpgradingCost = null;
    private string UpgradingCost_Original = "";
    [Export]
    public RichTextLabel UpgradingTime = null;
    private string UpgradingTime_Original = "";

    [ExportCategory("Runtime")]
    [Export]
    public UIBuildingsItem BuildingSelected = null;
    [Export]
    public SectorData _Sector = null;
    [Export]
    public PlanetData _Planet = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        ColonizeCost_Original = ColonizeCost.Text;
        ColonizeTime_Original = ColonizeTime.Text;
        ColonizingTime_Original = ColonizingTime.Text;
        UpgradingQueue_Original = UpgradingQueue.Text;
        UpgradingCost_Original = UpgradingCost.Text;
        UpgradingTime_Original = UpgradingTime.Text;

        ColonizeButton_Tooltip_Original = ColonizeButton_Tooltip.Row_1;
    }

    public void Refresh(PlanetData planet)
    {
        _Planet = planet;
        //_Sector = planet._Star.System?._Sector;

        // terraform
        for ( int idx = 0; idx < Prefix.Count; idx++)
        {
            Prefix[idx].Visible = false;
        }
        PrefixSeparator.Visible = false;

        // Colony
        bool hasColonySection = false;
        for (int idx = 0; idx < Main.Count; idx++)
        {
            Main[idx].Visible = false;
        }

        ColonizeButton_Tooltip.Disabled = true;

        //Array<DataBlock> buildings;
        //if (_Planet.Colony != null)
        //{
        //    buildings = _Planet.Colony.Buildings.GetSubs("Building");
        //}
        //else
        //{
        //    buildings = _Planet.Data.GetSubs("Building");
        //}
        //
        //int mainBuildingsIdx = 0;
        //for (int idx = 0; idx < buildings.Count; idx++)
        //{
        //    DataBlock building = buildings[idx];
        //    //DataBlock upgrade = building.GetSub("NewBuilding", false);
        //    BuildingQueueWrapper.Info inConstructionInfo = null;
        //    if (_Sector != null)
        //    {
        //        if (building.GetSub("InConstruction", false) != null)
        //        {
        //            if (_Planet.Colony != null)
        //            {
        //                continue;
        //                //inConstructionInfo = _Sector.BuildQueue_PerTurn_ActionChange.Get(building.ValueS, _Planet);
        //            }
        //            else
        //            {
        //                inConstructionInfo = _Sector.BuildQueue_PerTurn_ActionChange.Get(building.ValueS, _Planet);
        //            }
        //        }
        //        else
        //        {
        //            inConstructionInfo = _Sector.BuildQueue_PerTurn_ActionChange.GetUpgrade(building.ValueS, _Planet);
        //        }
        //    }
        //
        //    DataBlock buildingDef;
        //    if (inConstructionInfo != null)
        //    {
        //        buildingDef = inConstructionInfo.BuildingDef;
        //    }
        //    else
        //    {
        //        buildingDef = Game.Def.GetBuilding(buildings[idx].ValueS);
        //    }
        //
        //    RefreshMainBuilding(mainBuildingsIdx, buildingDef, inConstructionInfo);
        //    mainBuildingsIdx++;
        //}
        //
        //hasColonySection = mainBuildingsIdx > 0;
        //Colonizing.Visible = false;
        //Colonize.Visible = false;

        // res
        bool hasResSection = false;
        for (int idx = 0; idx < Resources.Count; idx++)
        {
            Resources[idx].Visible = false;
        }
        ResourcesSeparator.Visible = hasColonySection && hasResSection;

        BuildingPresent.Visible = hasColonySection || hasResSection;
        BuildingNone.Visible = !BuildingPresent.Visible;

        Title.Visible = true;
        UpgradeLocation.Visible = false;
        UpgradingLocation.Visible = false;
    }

    public void RefreshMainBuilding(int idx, DataBlock buildingDef, BuildingQueueWrapper.Info inConstructionInfo = null)
    {
        while (idx >= Main.Count)
        {
            UIBuildingsItem newBuilding = Main[0].Duplicate(7) as UIBuildingsItem;
            Main[0].GetParent().AddChild(newBuilding);
            Main.Add(newBuilding);
        }

        Main[idx].Refresh(buildingDef, null, inConstructionInfo);
        Main[idx].Visible = true;
    }

    public void Select(UIBuildingsItem building)
    {
        BuildingSelected = null;

        for (int idx = 0; idx < Main.Count; idx++)
        {
            if (Main[idx] != building)
            {
                Main[idx].Deselect();
            }
        }

        for (int idx = 0; idx < Main.Count; idx++)
        {
            if (Main[idx] == building)
            {
                BuildingSelected = building;

                BuildingQueueWrapper.Info inConstructionInfo = null;
                //for (int sectorIdx = 0; sectorIdx < Game.HumanPlayer.Sectors.Count; sectorIdx++)
                //{
                //    var info = Game.HumanPlayer.Sectors[sectorIdx].BuildQueue_PerTurn_ActionChange.Get(building._BuildingDef.ValueS, _Planet);
                //    if (info != null)
                //    {
                //        inConstructionInfo = info;
                //        break;
                //    }
                //}

                if (inConstructionInfo != null)
                {
                    OpenUpgradingWindow(inConstructionInfo, building.GlobalPosition + building.Size / 2);
                }
                else
                {
                    Array<DataBlock> upgradeLinks = building._BuildingDef.GetSubs("Upgrade");
                    Array<DataBlock> upgrades = new Array<DataBlock>();
                    for (int upgradeIdx = 0; upgradeIdx < upgradeLinks.Count; upgradeIdx++)
                    {
                        if (ActionBuild.GetAvailableBuilding(Game, _Sector, upgradeLinks[upgradeIdx].ValueS, _Planet) != null)
                        {
                            upgrades.Add(Game.Def.GetBuilding(upgradeLinks[upgradeIdx].ValueS));
                        }
                    }
                    OpenUpgradeWindow(upgrades, building.GlobalPosition + building.Size / 2);
                    return;
                }
            }
        }
        for (int idx = 0; idx < Upgrades.Count; idx++)
        {
            if (Upgrades[idx] == building)
            {
                DefBuildingWrapper action = ActionBuild.GetAvailableBuilding(Game, _Sector, building._BuildingDef.ValueS, _Planet);
                if (action != null)
                {
                    ActionBuild.AddToQueue(Game, action);
                    //Game.GalaxyUI.SectorConstruction.Refresh(_Sector);
                    //CloseUpgradeWindow();
                    Refresh(_Planet);
                }
                else
                {
                    GD.PrintErr("Build Action not found: " + building._BuildingDef.ValueS + " - " + _Planet.PlanetName);
                }
            }
        }
    }

    private void OpenUpgradeWindow(Array<DataBlock> upgrades, Vector2 pos)
    {
        if (_Sector != null && upgrades.Count > 0)
        {
            Title.Visible = false;
            UpgradeLocation.Visible = true;
            UpgradingLocation.Visible = false;

            UpgradeLocation.SetGlobalPosition(pos);

            for (int idx = 0; idx < Upgrades.Count; idx++)
            {
                Upgrades[idx].Visible = false;
            }

            while (Upgrades.Count < upgrades.Count)
            {
                UIBuildingsItem newItem = Upgrades[0].Duplicate(7) as UIBuildingsItem;
                Upgrades[0].GetParent().AddChild(newItem);
                Upgrades.Add(newItem);
            }

            for (int idx = 0; idx < Upgrades.Count; idx++)
            {
                if (idx < upgrades.Count)
                {
                    Upgrades[idx].Refresh(upgrades[idx], _Sector, null);
                    Upgrades[idx].Visible = true;
                }
                else
                {
                    Upgrades[idx].Visible = false;
                }
            }
        }
        else
        {
            Title.Visible = true;
            UpgradeLocation.Visible = false;
            UpgradingLocation.Visible = false;
        }
    }

    public void CloseUpgradeWindow()
    {
        Title.Visible = true;
        UpgradeLocation.Visible = false;
        UpgradingLocation.Visible = false;
    }

    public bool IsUpgradeWindowOpen()
    {
        return UpgradeLocation.Visible;
    }

    public void OpenUpgradingWindow(BuildingQueueWrapper.Info info, Vector2 pos)
    {
        UpgradingPlus.Visible = info.Position > 0;
        UpgradingMinus.Visible = info.Position < info.Sector.BuildQueue_PerTurn_ActionChange.Buildings.Count - 1;
        if (info.Position == 0)
        {
            UpgradingQueue.Text = UpgradingQueue_Original.Replace("$text", "In Construction");
            UpgradingCost.Text = UpgradingCost_Original.Replace("$value", info.Progress.ToString() + "/" + info.ProgressMax.ToString());
            UpgradingTime.Text = UpgradingTime_Original.Replace("time", info.Turns.ToString());
        }
        else
        {
            UpgradingQueue.Text = UpgradingQueue_Original.Replace("$text", Helper.GetOrdinal(info.Position) + " in queue");
            UpgradingCost.Text = UpgradingCost_Original.Replace("$value", info.ProgressMax.ToString());
            UpgradingTime.Text = UpgradingTime_Original.Replace("time", info.Turns.ToString());
        }

        Title.Visible = false;
        UpgradeLocation.Visible = false;
        UpgradingLocation.Visible = true;

        UpgradingLocation.SetGlobalPosition(pos);
    }

    public void CloseUpgradingWindow()
    {
        Title.Visible = true;
        UpgradeLocation.Visible = false;
        UpgradingLocation.Visible = false;
    }

    public bool IsUpgradingWindowOpen()
    {
        return UpgradingLocation.Visible;
    }
}