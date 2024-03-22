using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Transactions;

public partial class UIBuild : Control
{
    [ExportCategory("Links")]
    [Export]
    public Array<UIBuildBuilding> Buildings;
    [Export]
    public Array<UIBuildBuilding> Unavailable;
    [Export]
    public Array<UIBuildLocation> Locations;
    [Export]
    public Control BuildingSelected;
    [Export]
    public RichTextLabel BuildingName;
    private static string BuildingName_Original = "";
    [Export]
    public RichTextLabel Cost;
    private static string Cost_Original = "";
    [Export]
    public RichTextLabel Effects;
    private static string Effects_Original = "";
    [Export]
    public RichTextLabel Description;
    private static string Description_Original = "";
    [Export]
    public Control BuildingNone;

    [ExportCategory("Runtime")]
    [Export]
    public SectorData _Sector = null;
    [Export]
    public PlanetData _Planet = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        BuildingName_Original = BuildingName.Text;
        Cost_Original = Cost.Text;
        Effects_Original = Effects.Text;
        Description_Original = Description.Text;
    }

    private List<ActionTargetInfo> targets = new List<ActionTargetInfo>();
    public void Refresh(PlanetData _planet)
    {
        _Planet = _planet;

        targets.Clear();

        for (int idx = 0; idx < Game.TurnLoop.CurrentHumanPlayerData.Sectors.Count; idx++)
        {
            SectorData sector = Game.TurnLoop.CurrentHumanPlayerData.Sectors[idx];
            for (int buildIdx = 0; buildIdx < sector.AvailableBuildings_PerTurn.Count; buildIdx++)
            {
                ActionTargetInfo info = sector.AvailableBuildings_PerTurn[buildIdx];
                if (info._Planet == _Planet)
                {
                    targets.Add(info);
                }
            }
        }

        // grow
        while (Buildings.Count < targets.Count)
        {
            UIBuildBuilding newItem = Buildings[0].Duplicate(7) as UIBuildBuilding;
            Buildings[0].GetParent().AddChild(newItem);
            Buildings.Add(newItem);
        }

        for (int buildingsIdx = 0; buildingsIdx < Buildings.Count; buildingsIdx++)
        {
            if (buildingsIdx < targets.Count)
            {
                Buildings[buildingsIdx].Refresh(targets[buildingsIdx]);
                Buildings[buildingsIdx].Visible = true;
            }
            else
            {
                Buildings[buildingsIdx]._Building = null;
                Buildings[buildingsIdx].Visible = false;
                //Buildings[buildingsIdx].LocationActions.Clear();
            }
        }

        // --- 
        /*for (int unavailableIdx = 0; unavailableIdx < Game.Def.BuildingsInfo.Count; unavailableIdx++)
        {
            ActionTargetInfo buildingInfo = Game.Def.BuildingsInfo[unavailableIdx];
            DataBlock buildingDef = buildingInfo._Data;

            bool unavailable = true;
            for (int availableIdx = 0; availableIdx < Buildings.Count; availableIdx++)
            {
                if (buildingDef == Buildings[availableIdx]._BuildingDef)
                {
                    unavailable = false;
                    break;
                }
            }
            if (unavailable)
            {
                added = false;
                if (added == false)
                {
                    for (int buildingsIdx = 0; buildingsIdx < Unavailable.Count; buildingsIdx++)
                    {
                        if (Unavailable[buildingsIdx]._BuildingDef == null)
                        {
                            Unavailable[buildingsIdx].Refresh(buildingInfo);
                            Unavailable[buildingsIdx].Visible = true;
                            added = true;
                            break;
                        }
                    }
                }
                if (added == false)
                {
                    UIBuildBuilding newItem = Unavailable[0].Duplicate(7) as UIBuildBuilding;
                    Unavailable[0].GetParent().AddChild(newItem);
                    Unavailable.Add(newItem);
                    newItem.Refresh(buildingInfo);
                    newItem.Visible = true;
                    added = true;
                }
            }
        }*/

        //for (int unavailableIdx = 0; unavailableIdx < Unavailable.Count; unavailableIdx++)
        //{
        //    if (Unavailable[unavailableIdx]._Building == null)
        //    {
        //        Unavailable[unavailableIdx].Visible = false;
        //    }
        //}
    }

    public void Select(UIBuildBuilding building)
    {
        for (int idx = 0; idx < Buildings.Count; idx++)
        {
            if (Buildings[idx] != building)
            {
                Buildings[idx].Deselect();
            }
        }

        BuildingName.Text = BuildingName_Original.Replace("$name", building._Building.Name);

        Cost.Text = Cost_Original.Replace("$value", Helper.ResValueToString(building._Building.Cost.Get("Production").Value_1));

        Effects.Text = Effects_Original.Replace("$effects", building._Building.Benefit.GetAllString());

        Description.Text = Description_Original.Replace("$description", building._Building.Name + " is a building");

        BuildingSelected.Visible = true;
        BuildingNone.Visible = false;
}

    /*public void Refresh(SectorData _sector)
    {
        _Sector = _sector;

        for (int buildingsIdx = 0; buildingsIdx < Buildings.Count; buildingsIdx++)
        {
            Buildings[buildingsIdx]._BuildingDef = null;
            Buildings[buildingsIdx].Visible = false;
            Buildings[buildingsIdx].LocationActions.Clear();
        }

        bool added = false;
        for (int idx = 0; idx < _Sector.AvailableBuildings_PerTurn.Count; idx++)
        {
            added = false;
            for (int buildingsIdx = 0; buildingsIdx < Buildings.Count; buildingsIdx++)
            {
                if (Buildings[buildingsIdx]._BuildingDef == _Sector.AvailableBuildings_PerTurn[idx]._Data)
                {
                    Buildings[buildingsIdx].LocationActions.Add(_Sector.AvailableBuildings_PerTurn[idx]);
                    added = true;
                    break;
                }
            }
            if (added == false)
            {
                for (int buildingsIdx = 0; buildingsIdx < Buildings.Count; buildingsIdx++)
                {
                    if (Buildings[buildingsIdx]._BuildingDef == null)
                    {
                        Buildings[buildingsIdx].Refresh(_Sector.AvailableBuildings_PerTurn[idx]);
                        Buildings[buildingsIdx].Visible = true;
                        added = true;
                        break;
                    }
                }
            }
            if (added == false) 
            {
                UIBuildBuilding newItem = Buildings[0].Duplicate(7) as UIBuildBuilding;
                Buildings[0].GetParent().AddChild(newItem);
                Buildings.Add(newItem);
                newItem.Refresh(_Sector.AvailableBuildings_PerTurn[idx]);
                newItem.Visible = true;
                added = true;
            }
        }

        for (int buildingsIdx = 0; buildingsIdx < Buildings.Count; buildingsIdx++)
        {
            if (Buildings[buildingsIdx]._BuildingDef == null)
            {
                Buildings[buildingsIdx].Visible = false;
                break;
            }
        }

        // --- 
        for (int unavailableIdx = 0; unavailableIdx < Game.Def.BuildingsInfo.Count; unavailableIdx++)
        {
            ActionTargetInfo buildingInfo = Game.Def.BuildingsInfo[unavailableIdx];
            DataBlock buildingDef = buildingInfo._Data;

            bool unavailable = true;
            for (int availableIdx = 0; availableIdx < Buildings.Count; availableIdx++)
            {
                if (buildingDef == Buildings[availableIdx]._BuildingDef)
                {
                    unavailable = false;
                    break;
                }
            }
            if (unavailable)
            {
                added = false;
                if (added == false)
                {
                    for (int buildingsIdx = 0; buildingsIdx < Unavailable.Count; buildingsIdx++)
                    {
                        if (Unavailable[buildingsIdx]._BuildingDef == null)
                        {
                            Unavailable[buildingsIdx].Refresh(buildingInfo);
                            Unavailable[buildingsIdx].Visible = true;
                            added = true;
                            break;
                        }
                    }
                }
                if (added == false)
                {
                    UIBuildBuilding newItem = Unavailable[0].Duplicate(7) as UIBuildBuilding;
                    Unavailable[0].GetParent().AddChild(newItem);
                    Unavailable.Add(newItem);
                    newItem.Refresh(buildingInfo);
                    newItem.Visible = true;
                    added = true;
                }
            }
        }

        for (int unavailableIdx = 0; unavailableIdx < Unavailable.Count; unavailableIdx++)
        {
            if (Unavailable[unavailableIdx]._BuildingDef == null)
            {
                Unavailable[unavailableIdx].Visible = false;
            }
        }

        // ---
        for (int locationIdx = 0; locationIdx < Locations.Count; locationIdx++)
        {
            Locations[locationIdx].Visible = false;
        }
    }*/

    /*public void Select(UIBuildBuilding building)
    {
        for (int idx = 0; idx < Buildings.Count; idx++)
        {
            if (Buildings[idx] != building)
            {
                Buildings[idx].Deselect();
            }
        }

        for (int locationIdx = 0; locationIdx < Locations.Count; locationIdx++)
        {
            Locations[locationIdx]._ActionTarget = null;
            Locations[locationIdx].Visible = false;
        }
        
        bool added = false;
        for (int idx = 0; idx < _Sector.AvailableBuildings_PerTurn.Count; idx++)
        {
            added = false;
            if (building._BuildingDef == _Sector.AvailableBuildings_PerTurn[idx]._Data)
            {
                for (int locationIdx = 0; locationIdx < Locations.Count; locationIdx++)
                {
                    if (Locations[locationIdx]._ActionTarget == null)
                    {
                        Locations[locationIdx].Refresh(_Sector.AvailableBuildings_PerTurn[idx]);
                        Locations[locationIdx].Visible = true;
                        added = true;
                        break;
                    }
                }
                if (added == false)
                {
                    UIBuildLocation newItem = Locations[0].Duplicate(7) as UIBuildLocation;
                    Locations[0].GetParent().AddChild(newItem);
                    Locations.Add(newItem);
                    newItem.Refresh(_Sector.AvailableBuildings_PerTurn[idx]);
                    newItem.Visible = true;
                    added = true;
                }
            }
        }
    }*/

    public void OnCancel()
    {
        Game.WindowsUI.HideAll();
    }
}