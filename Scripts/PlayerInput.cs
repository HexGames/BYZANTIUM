using Godot;
using System;

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
                systemFromSector.Star._Node.GFX.Select(false, true);
            }
        }

        if (refreshUI) Game.GalaxyUI.Refresh();

        // on select
        if (SelectedStar != null) GD.Print("PlayerInput - selected " + SelectedSector.SectorName);
    }

    public void SelectStar(StarData newSelectedStar, bool refreshUI = true)
    {
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
            SelectedStar._Node.GFX.Select(true, true);
        }
        else
        {
            SelectedStar._Node.GFX.Select(true, false);
        }

        if (refreshUI) Game.GalaxyUI.Refresh();

        // on select
        if (SelectedStar != null) GD.Print("PlayerInput - selected " + SelectedStar.StarName);
    }
    public void SelectPlanet(PlanetData newSelectedPlanet)
    {
        if (SelectedPlanet == newSelectedPlanet)
        {
            // on reselect
            GD.Print("PlayerInput - reselecteed " + SelectedPlanet.PlanetName);

            Game.GalaxyUI.Refresh();

            return;
        }

        if (SelectedPlanet != null)
        {
            DeselectPlanet();
        }
        SelectedPlanet = newSelectedPlanet;

        SelectStar(SelectedPlanet._Star, false);

        Game.GalaxyUI.Refresh();

        // on select
        if (SelectedPlanet != null) GD.Print("PlayerInput - selected " + SelectedPlanet.PlanetName);
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
        else if (SelectedPlanet != null)
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

    public void DeselectAll()
    {
        DeselectSector();
        DeselectStar();
        DeselectPlanet();

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
                SelectedStar._Node.GFX.Select(false, true);
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
}
