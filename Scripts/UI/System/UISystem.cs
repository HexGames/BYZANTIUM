using Godot;

//[Tool]
public partial class UISystem : Control
{
    [ExportCategory("Links")]
    [Export]
    public UISystemBarList SystemBar = null;
    [Export]
    public UIBudget Budget = null;
    [Export]
    public UIConstruction SectorConstruction = null;
    [Export]
    public UIShipbuilding SectorShipbuilding = null;
    [Export]
    public UIEconomyInfo EconomyInfo = null;
    [Export]
    public UIFocusList FocusInfo = null;
    [Export]
    public UIItemList PlanetInfo = null;
    [Export]
    public UIItemList BuildingInfo = null;

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
        PlanetSelected = null;
        SystemSelected = null;
        SectorSelected = null;

        if (star == null)
        {
            // deselect other planets
            SystemBar.Visible = false;

            Budget.Visible = false;
            SectorConstruction.Visible = false;
            SectorShipbuilding.Visible = false;
            PlanetInfo.Visible = false;
            BuildingInfo.Visible = false;

            _StarData = null;

            Visible = false;
            Game.Camera.UILockSystem = false;

            return;
        }

        _StarData = star;

        SystemBar.Refresh(star);

        if (_StarData.System != null)
        {
            Budget.Visible = false;

            SectorConstruction.Refresh(_StarData.System._Sector);
            SectorConstruction.Visible = true;
            SectorShipbuilding.Visible = false;

            PlanetInfo.Visible = false;
            BuildingInfo.Visible = false;

            // autoselect biggest colony
            if (_StarData.System._Sector._Player == Game.HumanPlayer)
            {
                SystemBar.ForceSelect(_StarData.System.Colonies[0].Planet);
            }
        }
        else
        {
            Budget.Visible = false;
            SectorConstruction.Visible = false;
            SectorShipbuilding.Visible = false;

            PlanetInfo.Visible = false;
            BuildingInfo.Visible = false;
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

        FocusInfo.Visible = false;

        EconomyInfo.Refresh(sector);
        EconomyInfo.Visible = true;
    }

    public void ShowSystemInfo(SystemData system)
    {
        PlanetInfo.Visible = false;

        BuildingInfo.Visible = false;

        FocusInfo.Visible = false;

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

            BuildingInfo.Refresh(colony.Buildings, "Buildings");
            BuildingInfo.Visible = true;

            EconomyInfo.Refresh(colony);
            EconomyInfo.Visible = true;

            FocusInfo.Refresh(colony);
            FocusInfo.Visible = true;

            //JobList.Refresh(colony);
            //JobList.Visible = true;

            int production = colony.Resources_PerTurn.Get("Production").Value_2;
            int shipbuilding = colony.Resources_PerTurn.Get("Shipbuilding").Value_1;

            SectorShipbuilding.Refresh(colony.ColonyName, colony.ActionShipbuilding, shipbuilding);
            SectorShipbuilding.Visible = true;
        }
        else
        {
            BuildingInfo.Visible = false;
            EconomyInfo.Visible = false;
            FocusInfo.Visible = false;
            SectorShipbuilding.Visible = false;
        }
    }

    public void HideInfo()
    {
        PlanetInfo.Visible = false;
        BuildingInfo.Visible = false;
        EconomyInfo.Visible = false;
        FocusInfo.Visible = false;
    }
}