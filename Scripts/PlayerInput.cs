using Godot;
using Godot.Collections;

// Generated
public partial class PlayerInput : Node
{
    [ExportCategory("Runtime")]
    [Export]
    public Game Game = null;
    [Export]
    public SectorData SelectedSector = null;
    [Export]
    public StarData SelectedStar = null;
    [Export]
    public PlanetData SelectedPlanet = null;
    [Export]
    public Array<FleetData> SelectedFleets = new Array<FleetData>();

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
            //OnSelect += PlayerInput.SelectLocation;
        }
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent)
        {
            if (!mouseButtonEvent.IsPressed())
            {
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    //DeselectAll();
                    DeselectOneStep();
                }
            }
        }

        if (inputEvent is InputEventKey keyButtonEvent)
        {
            if (!keyButtonEvent.IsPressed())
            {
                // on key button release
                if (keyButtonEvent.Keycode == Key.S)
                {
                    Game.Map.SaveMap();
                }
                if (keyButtonEvent.Keycode == Key.D)
                {
                    Game.Map.LoadMap();
                    Game.Map.Data.GenerateGameFromData(Game.Def);
                }
            }
        }
    }

    public void SelectSector(SectorData newSelectedSector, bool refreshUI = true)
    {
        DeselectFleetAll();

        if (SelectedSector == newSelectedSector)
        {
            // on reselect
            GD.Print("PlayerInput - reselecteed " + SelectedSector.SectorName);

            if (refreshUI) Game.GalaxyUI.Refresh();

            return;
        }

        if (SelectedSector != null)
        {
            DeselectSector();
        }

        SelectedSector = newSelectedSector;

        for (int idx = 0; idx < SelectedSector.Systems.Count; idx++)
        {
            SystemData systemFromSector = SelectedSector.Systems[idx];
            if (systemFromSector.Star != SelectedStar)
            {
                systemFromSector.Star._Node.GFX.Select(false, true, false, false);
            }
        }

        if (refreshUI) Game.GalaxyUI.Refresh();

        // on select
        if (SelectedStar != null) GD.Print("PlayerInput - selected " + SelectedSector.SectorName);
    }

    public void SelectStar(StarData newSelectedStar, bool refreshUI = true)
    {
        DeselectFleetAll();

        if (SelectedStar == newSelectedStar)
        {
            // on reselect
            GD.Print("PlayerInput - reselecteed " + SelectedStar.StarName);

            if (refreshUI) Game.GalaxyUI.Refresh();

            return;
        }

        if (SelectedSector != null && SelectedSector != newSelectedStar?.System?._Sector)
        {
            DeselectSector();
        }
        if (SelectedStar != null)
        {
            if (SelectedPlanet != null)
            {
                DeselectPlanet();
            }
            DeselectStar();
        }
        SelectedStar = newSelectedStar;

        SystemData system = SelectedStar.System;
        if (system != null)
        {
            SelectSector(system._Sector, false);
            SelectedStar._Node.GFX.Select(true, true, false, false);
        }
        else
        {
            SelectedStar._Node.GFX.Select(true, false, false, false);
        }

        if (refreshUI) Game.GalaxyUI.Refresh();

        // on select
        if (SelectedStar != null) GD.Print("PlayerInput - selected " + SelectedStar.StarName);
    }
    public void SelectPlanet(PlanetData newSelectedPlanet, bool buildingsTab, bool populationTab)
    {
        DeselectFleetAll();

        if (SelectedPlanet == newSelectedPlanet)
        {
            // on reselect
            GD.Print("PlayerInput - reselecteed " + SelectedPlanet.PlanetName);

            Game.GalaxyUI.Refresh(buildingsTab, populationTab);

            return;
        }

        if (SelectedPlanet != null)
        {
            DeselectPlanet();
        }
        SelectedPlanet = newSelectedPlanet;

        SelectStar(SelectedPlanet._Star, false);

        Game.GalaxyUI.Refresh(buildingsTab, populationTab);

        // on select
        if (SelectedPlanet != null) GD.Print("PlayerInput - selected " + SelectedPlanet.PlanetName);
    }

    public void SelectFleet(StarData atStar, bool friendlyToStar)
    {
        DeselectAll(false);

        for (int idx = 0; idx < atStar.Fleets_PerTurn.Count; idx++)
        {
            if (atStar.System == null || atStar.System._Sector._Player == Game.HumanPlayer)
            {
                if (friendlyToStar)
                {
                    if (atStar.Fleets_PerTurn[idx]._Player == Game.HumanPlayer)
                    {
                        SelectedFleets.Add(atStar.Fleets_PerTurn[idx]);
                    }
                }
                else
                {
                    if (atStar.Fleets_PerTurn[idx]._Player != Game.HumanPlayer)
                    {
                        SelectedFleets.Add(atStar.Fleets_PerTurn[idx]);
                    }
                }
            }
            else
            {
                if (friendlyToStar)
                {
                    if (atStar.Fleets_PerTurn[idx]._Player != Game.HumanPlayer)
                    {
                        SelectedFleets.Add(atStar.Fleets_PerTurn[idx]);
                    }
                }
                else
                {
                    if (atStar.Fleets_PerTurn[idx]._Player == Game.HumanPlayer)
                    {
                        SelectedFleets.Add(atStar.Fleets_PerTurn[idx]);
                    }
                }
            }
        }

        if (SelectedFleets.Count > 0)
        {
            SelectedFleets[0].AtStar_PerTurn._Node.GFX.Select(false, false, friendlyToStar, !friendlyToStar, SelectedFleets.Count == 1);
        }

        Game.GalaxyUI.Refresh();
    }

    public void DeselectOneStep()
    {
        if (Game.GalaxyUI.ColonyBuildings.IsUpgradeWindowOpen())
        {
            Game.GalaxyUI.ColonyBuildings.CloseUpgradeWindow();
            return;
        }
        else if (Game.GalaxyUI.ColonyBuildings.IsUpgradingWindowOpen())
        {
            Game.GalaxyUI.ColonyBuildings.CloseUpgradingWindow();
            return;
        }
        else if (SelectedFleets.Count > 0)
        {
            DeselectFleetAll();
        }
        if (SelectedPlanet != null)
        {
            DeselectPlanet();
        }
        else if (SelectedStar != null)
        {
            DeselectStar();
        }
        else
        {
            DeselectSector();
        }

        Game.GalaxyUI.Refresh();
    }

    public void DeselectAll(bool refresh = true)
    {
        Game.GalaxyUI.ColonyBuildings.CloseUpgradeWindow();
        Game.GalaxyUI.ColonyBuildings.CloseUpgradingWindow();

        DeselectSector();
        DeselectStar();
        DeselectPlanet();
        DeselectFleetAll();

        if (refresh)
            Game.GalaxyUI.Refresh();
    }
    public void DeselectAllButSector()
    {
        DeselectStar();
        DeselectPlanet(); 
        
        Game.GalaxyUI.Refresh();
    }

    public void DeselectSector()
    {
        // on deselect
        if (SelectedSector != null) GD.Print("PlayerInput - deselected " + SelectedSector.SectorName);

        if (SelectedSector != null)
        {
            for (int idx = 0; idx < SelectedSector.Systems.Count; idx++)
            {
                SystemData systemFromSector = SelectedSector.Systems[idx];
                if (systemFromSector.Star != SelectedStar)
                {
                    systemFromSector.Star._Node.GFX.Deselect();
                }
            }

            SelectedSector = null;
        }
    }

    public void DeselectStar()
    {
        // on deselect
        if (SelectedStar != null) GD.Print("PlayerInput - deselected " + SelectedStar.StarName);

        if (SelectedStar != null)
        {
            if (SelectedSector != null)
            {
                SelectedStar._Node.GFX.Select(false, true, false, false);
            }
            else
            {
                SelectedStar._Node.GFX.Deselect();
            }
            SelectedStar = null;
        }
    }

    public void DeselectPlanet()
    {
        // on deselect
        if (SelectedPlanet != null) GD.Print("PlayerInput - deselected " + SelectedPlanet.PlanetName);

        if (SelectedPlanet != null)
        {
            SelectedPlanet = null;
        }
    }

    public void DeselectFleetAll()
    {
        if (SelectedFleets.Count > 0)
        {
            SelectedFleets[0].AtStar_PerTurn._Node.GFX.Deselect();
        }

        SelectedFleets.Clear();
    }

    public void DeselectFleet(FleetData fleet)
    {
        if (SelectedFleets.Count == 1)
        {
            SelectedFleets[0].AtStar_PerTurn._Node.GFX.Deselect();
        }
        SelectedFleets.Remove(fleet); 

        Game.GalaxyUI.RefreshPawnsUI();
    }
}
