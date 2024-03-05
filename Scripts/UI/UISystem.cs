using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

//[Tool]
public partial class UISystem : Control
{
    [ExportCategory("Links")]
    [Export]
    public UISystemBarList SystemBar = null;
    [Export]
    public UIBudget Budget = null;
    [Export]
    public UIConProgress ConProgressBuildings = null;
    [Export]
    public UIConProgress ConProgressColony = null;
    [Export]
    public UIConProgress ConProgressShipyard = null;
    [Export]
    public Control ConProgressBuildings_None = null;
    [Export]
    public Control ConProgressColony_None = null;
    [Export]
    public Control ConProgressShipyard_None = null;
    [Export]
    public UIEconomyInfo EconomyInfo = null;
    [Export]
    public UIItemList PlanetInfo = null;
    [Export]
    public UIItemList BuildingInfo = null;
    [Export]
    public UIItemList TradeInfo = null;

    [ExportCategory("Runtime")]
    [Export]
    public PlanetData PlanetSelected = null;
    [Export]
    public SystemData SystemSelected = null;
    [Export]
    public SectorData SectorSelected = null;
    [Export]
    public StarData _StarData = null;

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
            //OnSelect += PlayerInput.SelectLocation;
            Visible = false;
        }
    }

    public void Refresh( StarData star )
    {
        if (star == null)
        {
            // deselect other planets
            SystemBar.Visible = false;

            Budget.Visible = false;
            ConProgressBuildings.Visible = false;
            ConProgressColony.Visible = false;
            ConProgressShipyard.Visible = false;
            ConProgressBuildings_None.Visible = false;
            ConProgressColony_None.Visible = false;
            ConProgressShipyard_None.Visible = false;
            PlanetInfo.Visible = false;
            BuildingInfo.Visible = false;
            TradeInfo.Visible = false;

            _StarData = null;
            PlanetSelected = null;
            SystemSelected = null;
            SectorSelected = null;

            Visible = false;
            Game.Camera.UILockSystem = false;

            return;
        }

        _StarData = star;

        SystemBar.Refresh(star);

        if (_StarData.System != null)
        {
            //int totalProduction = star.System._Sector.ResourcesPerTurn.Get("Prod").Value_1 - star.System._Sector.ResourcesPerTurn.Get("Prod").Value_2;
            //Budget.RefreshBudget(star.System._Sector, totalProduction, false, false);
            Budget.Visible = false;

            ConProgressBuildings.Visible = false;
            ConProgressColony.Visible = false;
            ConProgressShipyard.Visible = false;
            ConProgressBuildings_None.Visible = false;
            ConProgressColony_None.Visible = false;
            ConProgressShipyard_None.Visible = false;

            //int campaignProgress = star.System._Sector.ResourcesPerTurn.Get("Energy").Value_1 - star.System._Sector.ResourcesPerTurn.Get("Energy").Value_2;
            //SectorCampaignProgress.RefreshCampaignProgress(star.System._Sector.ActionCampaign, campaignProgress);
            //SectorCampaignProgress.Visible = true;

            //int constructionProgress = star.System._Sector.BudgetPerTurn.GetProduction("Construction_1", totalProduction);
            //SectorConstructionProgress.RefreshConstructionProgress(star.System._Sector.ActionBuild, constructionProgress);
            //SectorConstructionProgress.Visible = true;

            PlanetInfo.Visible = false;
            BuildingInfo.Visible = false;
            TradeInfo.Visible = false; // not used yet

            // autoselect biggest colony
            if (_StarData.System._Sector._Player == Game.HumanPlayer)
            {
                SystemBar.ForceSelect(_StarData.System.Colonies[0].Planet);
            }
        }
        else
        {
            Budget.Visible = false;
            ConProgressBuildings.Visible = false;
            ConProgressColony.Visible = false;
            ConProgressShipyard.Visible = false;
            ConProgressBuildings_None.Visible = false;
            ConProgressColony_None.Visible = false;
            ConProgressShipyard_None.Visible = false;

            PlanetInfo.Visible = false;
            BuildingInfo.Visible = false;
            TradeInfo.Visible = false;
        }

        Visible = true;
        Game.Camera.UILockSystem = true;
    }

    public void Hover(PlanetData planet)
    {
        ShowPlanetInfo(planet);
    }

    public void HoverSystem()
    {
        if (_StarData.System != null)
        {
            ShowSystemInfo(_StarData.System);
        }
    }

    public void HoverSector()
    {
        if (_StarData.System != null)
        {
            ShowSectorInfo(_StarData.System._Sector);
        }
    }

    public void Dehover()
    {
        if (PlanetSelected != null)
        {
            ShowPlanetInfo(PlanetSelected);
        }
        else if (SystemSelected != null)
        {
            ShowSystemInfo(SystemSelected);
        }
        else if (SectorSelected != null)
        {
            ShowSectorInfo(SectorSelected);
        }
        else
        {
            HideInfo();
        }
    }

    public void Select(PlanetData planet)
    {
        PlanetSelected = planet;
        SystemSelected = null;
        SectorSelected = null;

        ShowPlanetInfo(PlanetSelected);
    }

    public void SelectSystem()
    {
        PlanetSelected = null;
        SystemSelected = _StarData.System;
        SectorSelected = null;

        ShowSystemInfo(SystemSelected);
    }

    public void SelectSector()
    {
        PlanetSelected = null;
        SystemSelected = null;
        SectorSelected = _StarData.System._Sector;

        ShowSectorInfo(SectorSelected);
    }

    public void ShowSectorInfo(SectorData sector)
    {
        PlanetInfo.Visible = false;

        BuildingInfo.Visible = false;

        EconomyInfo.Refresh(sector);
        EconomyInfo.Visible = true;
    }

    public void ShowSystemInfo(SystemData system)
    {
        PlanetInfo.Visible = false;

        BuildingInfo.Visible = false;

        EconomyInfo.Refresh(system);
        EconomyInfo.Visible = true;
    }

    public void ShowPlanetInfo(PlanetData planet)
    {
        PlanetInfo.Refresh(planet.Data);
        PlanetInfo.Visible = true;

        if (planet.Colony != null)
        {
            ColonyData colony = planet.Colony;

            BuildingInfo.Refresh(colony.Buildings, "Buildings   " 
                + colony.Resources.GetSub("BuildingSlots*Used").ValueI.ToString() + "/" + colony.Resources.GetSub("BuildingSlots").ValueI.ToString()
                + "[img=24x24]Assets/UI/Symbols/Building.png[/img]");
            BuildingInfo.Visible = true;

            EconomyInfo.Refresh(colony);
            EconomyInfo.Visible = true;

            int productionTotal = colony.ResourcesPerTurn.Get("Prod").Value_2;
            int constructors = colony.ResourcesPerTurn.Get("Constructor").Value_1;
            int shipyard = colony.ResourcesPerTurn.Get("Shipyard").Value_1;

            int pops = colony.ResourcesPerTurn.Get("Pops").Value_2;
            if (pops > 0)
            {
                ConProgressBuildings.Refresh(colony.ColonyName, colony.ActionConBuildings, productionTotal - 500 * constructors - 500 * shipyard);
                ConProgressBuildings.Visible = true;
                ConProgressBuildings_None.Visible = false;
            }
            else
            {
                ConProgressBuildings.Visible = false;
                ConProgressBuildings_None.Visible = true;
            }

            if (constructors > 0)
            {
                ConProgressColony.Refresh(colony.ColonyName, colony.ActionConColony, 500 * constructors, constructors);
                ConProgressColony.Visible = true;
                ConProgressColony_None.Visible = false;
            }
            else
            {
                ConProgressColony.Visible = false;
                ConProgressColony_None.Visible = true;
            }

            if (shipyard > 0)
            {
                ConProgressShipyard.Refresh(colony.ColonyName, colony.ActionConShipyard, 500 * shipyard, shipyard);
                ConProgressShipyard.Visible = true;
                ConProgressShipyard_None.Visible = false;
            }
            else
            {
                ConProgressShipyard.Visible = false;
                ConProgressShipyard_None.Visible = true;
            }
        }
        else
        {
            BuildingInfo.Visible = false;
            EconomyInfo.Visible = false;

            ConProgressBuildings.Visible = false;
            ConProgressColony.Visible = false;
            ConProgressShipyard.Visible = false;
            ConProgressBuildings_None.Visible = false;
            ConProgressColony_None.Visible = false;
            ConProgressShipyard_None.Visible = false;
        }
    }

    public void HideInfo()
    {
        PlanetInfo.Visible = false;
        BuildingInfo.Visible = false;
        EconomyInfo.Visible = false;
    }
}