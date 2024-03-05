using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UISystemBarList : Control
{
    [ExportCategory("Links")]
    [Export]
    public HScrollBar ScrollBar = null;

    [Export]
    public Control ParentNode = null;

    [Export]
    public Array<UIBarListPlanet> Planets = new Array<UIBarListPlanet>();

    //[Export]
    //public Array<UIPawnListFleet> Fleets = new Array<UIPawnListFleet>();

    [Export]
    public Control FirendlySystem = null;
    [Export]
    public Control NoSystem = null;

    [Export]
    public Button SystemButton = null;

    [Export]
    public Control SectorSelected = null;
    [Export]
    public Control SystemSelected = null;


    [Export]
    private RichTextLabel NoSystemLabel = null;
    private string NoSystemLabel_Original = null; 
    [Export]
    private RichTextLabel SystemLabel = null;
    private string SystemLabel_Original = null;
    [Export]
    private RichTextLabel SectorLabel = null;
    private string SectorLabel_Original = null;
    [Export]
    private RichTextLabel SectorQuickInfoLabel = null;
    private string SectorQuickInfoLabel_Original = null;

    [ExportCategory("Runtime")]
    [Export]
    public bool GroupByType = false;
    [Export]
    public StarData _StarData = null;
    [Export]
    public UIBarListPlanet PlanetSelected = null;
    //[Export]
    //public UIPawnListFleet FleetSelected = null;

    Game Game;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");

            NoSystemLabel_Original = NoSystemLabel.Text;
            SystemLabel_Original = SystemLabel.Text;
            SectorLabel_Original = SectorLabel.Text;
            SectorQuickInfoLabel_Original = SectorQuickInfoLabel.Text;

            Visible = false;
        }
    }

    public void Refresh(StarData starData)
    {
        _StarData = starData;

        if (_StarData.System != null && _StarData.System._Sector._Player == Game.HumanPlayer)
        {
            FirendlySystem.Visible = true;
            NoSystem.Visible = false;

            SystemButton.Disabled = _StarData.System._Sector.Systems.Count == 1;

            SystemLabel.Text = SystemLabel_Original.Replace("$system", _StarData.StarName);
            SectorLabel.Text = SectorLabel_Original.Replace("$sector", _StarData.System._Sector.SectorName);

            SectorQuickInfoLabel.Text = SectorQuickInfoLabel_Original
                .Replace("$energy", _StarData.System._Sector.ResourcesPerTurn.GetUsedPerTotalString("Energy"))
                .Replace("$minerals", _StarData.System._Sector.ResourcesPerTurn.GetUsedPerTotalString("Minerals"));
        }
        else
        {
            FirendlySystem.Visible = false;
            NoSystem.Visible = true;

            NoSystemLabel.Text = NoSystemLabel_Original.Replace("$system", _StarData.StarName);
        }

        // hide all
        for (int planetIdx = 0; planetIdx < Planets.Count; planetIdx++)
        {
            Planets[planetIdx].Visible = false;
        }

        // refresh planets
        for (int planetIdx = 0; planetIdx < starData.Planets.Count; planetIdx++)
        {
            RefreshPlanets(planetIdx, starData, starData.Planets[planetIdx]);
        }

        // unparent
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            ParentNode.RemoveChild(Planets[idx]);
        }

        // reorder

        // parent
        for (int planetIdx = 0; planetIdx < Planets.Count; planetIdx++)
        {
            ParentNode.AddChild(Planets[planetIdx]);
            GD.Print("list " + planetIdx + " " + Planets[planetIdx]._PlanetData.PlanetName);
        }

        Visible = true;
    }

    public void RefreshPlanets(int idx, StarData systemData, PlanetData planetData)
    {
        while (idx >= Planets.Count)
        {
            UIBarListPlanet newPlanet = Planets[0].Duplicate(7) as UIBarListPlanet;
            Planets[0].GetParent().AddChild(newPlanet);
            Planets.Add(newPlanet);
        }

        PlanetData parentPlanet = null;
        if (planetData.Data.GetSub("Moon") != null)
        {
            for (int systemPlanetIdx = 0; systemPlanetIdx < systemData.Planets.Count; systemPlanetIdx++)
            {
                if (systemData.Planets[systemPlanetIdx].Data.GetSub("Moon") == null)
                {
                    parentPlanet = systemData.Planets[systemPlanetIdx];
                }
                if (systemData.Planets[systemPlanetIdx] == planetData)
                {
                    break;
                }
            }
        }

        Planets[idx].Refresh(planetData, parentPlanet);
    }

    public void Hover(UIBarListPlanet planet)
    {
        Game.SystemUI.Hover(planet._PlanetData);
    }

    public void Unhover()
    {
        Game.SystemUI.Dehover();
    }
    public void OnUnhover()
    {
        Unhover();
    }

    public void OnHoverSystem()
    {
        if (SystemButton.Disabled == false)
        {
            Game.SystemUI.HoverSystem();
        }
    }

    public void OnHoverSector()
    {
        Game.SystemUI.HoverSector();
    }

    public void ForceSelect(PlanetData planet)
    {
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (Planets[idx]._PlanetData == planet)
            {
                Planets[idx].OnSelect();
            }
        }
    }

    public void Select(UIBarListPlanet planet)
    {
        PlanetSelected = planet;
        Game.SystemUI.Select(PlanetSelected._PlanetData);
        SectorSelected.Visible = false;
        SystemSelected.Visible = false;
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (Planets[idx] != PlanetSelected)
            {
                Planets[idx].Deselect();
            }
        }
    }

    public void OnSelectSystem()
    {
        Game.SystemUI.SelectSector();

        SectorSelected.Visible = false;
        SystemSelected.Visible = true;
        PlanetSelected.Deselect();
    }

    public void OnSelectSector()
    {
        Game.SystemUI.SelectSystem();

        SectorSelected.Visible = true;
        SystemSelected.Visible = false; 
        PlanetSelected.Deselect();
    }
}