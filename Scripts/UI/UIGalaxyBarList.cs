using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIGalaxyBarList : Control
{
    [ExportCategory("Links")]
    [Export]
    public HScrollBar ScrollBar = null;

    [Export]
    public Control ParentNode = null;

    [Export]
    public UIBarListGroup ParentSector = null;
    [Export]
    public UIBarListGroup ParentSystem = null;
    [Export]
    //public Array<UIBarListHeader> GroupHeaders = new Array<UIBarListHeader>();
    public UIBarListHeader GroupHeader = null;

    [Export]
    public Array<UIBarListGroup> Groups = new Array<UIBarListGroup>();

    [Export]
    public Array<UIBarListPlanet> Planets = new Array<UIBarListPlanet>();

    [ExportCategory("Tabs")]
    [Export]
    public Panel Tabs = null;
    [Export]
    public Control Tabs_BuildingsAvailable = null;
    [Export]
    public Control Tabs_PopulationAvailable = null;
    [Export]
    public Panel Tabs_BuildingsSelected = null;
    [Export]
    public Panel Tabs_PopulationSelected = null;
    //[Export]
    //public Array<UIPawnListFleet> Fleets = new Array<UIPawnListFleet>();


    [ExportCategory("Runtime")]
    [Export]
    public PlayerData _PlayerData = null;
    [Export]
    public SectorData _SectorData = null;
    [Export]
    public StarData _StarData = null;
    [Export]
    public SystemData _SystemData = null;
    [Export]
    public UIBarListPlanet PlanetSelected = null;
    //[Export]
    //public UIPawnListFleet FleetSelected = null;

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

    public void Refresh(PlayerData playerData)
    {
        _PlayerData = playerData;

        ParentSector.Visible = false;
        ParentSystem.Visible = false;

        // hide all
        Refresh_HideAll();

        for (int sectorIdx = 0; sectorIdx < _PlayerData.Sectors.Count; sectorIdx++)
        {
            RefreshGroup(sectorIdx, _PlayerData.Sectors[sectorIdx]);
        }

        Visible = true;
    }

    public void Refresh(SectorData sectorData)
    {
        _SectorData = sectorData;

        ParentSector.Refresh(sectorData, true);
        ParentSystem.Visible = false;

        // hide all
        Refresh_HideAll();

        GroupHeader.Refresh(sectorData);
        GroupHeader.Visible = true;

        for (int sectorIdx = 0; sectorIdx < _SectorData.Systems.Count; sectorIdx++)
        {
            RefreshGroup(sectorIdx, _SectorData.Systems[sectorIdx]);
        }

        Visible = true;
    }

    public void Refresh(SystemData systemData)
    {
        _SystemData = systemData;

        ParentSector.Refresh(systemData._Sector, true);
        ParentSystem.Refresh(systemData, true);

        // hide all
        Refresh_HideAll();

        GroupHeader.Refresh(systemData);
        GroupHeader.Visible = true;

        for (int planetIdx = 0; planetIdx < _SystemData.Star.Planets.Count; planetIdx++)
        {
            RefreshPlanet(planetIdx, _SystemData.Star, _SystemData.Star.Planets[planetIdx]);
        }

        // reorder

        // parent
        //int planetIdx = 0;
        //for (int groupIdx = 0; groupIdx < GroupHeaders.Count; groupIdx++)
        //{
        //    ParentNode.AddChild(GroupHeaders[groupIdx]);
        //    while (planetIdx < Planets.Count && GroupHeaders[groupIdx]._SectorData.GetPlanet(Planets[planetIdx]._PlanetData.PlanetName) != null)
        //    {
        //        ParentNode.AddChild(Planets[planetIdx]);
        //        planetIdx++;
        //    }
        //}

        Visible = true;
    }

    public void Refresh(StarData starData)
    {
        _StarData = starData;

        ParentSector.Visible = false;
        ParentSystem.Visible = false;

        // hide all
        Refresh_HideAll();

        GroupHeader.Visible = false;

        for (int planetIdx = 0; planetIdx < _StarData.Planets.Count; planetIdx++)
        {
            RefreshPlanet(planetIdx, _StarData, _StarData.Planets[planetIdx]);
        }

        // reorder

        // parent
        //int planetIdx = 0;
        //for (int groupIdx = 0; groupIdx < GroupHeaders.Count; groupIdx++)
        //{
        //    ParentNode.AddChild(GroupHeaders[groupIdx]);
        //    while (planetIdx < Planets.Count && GroupHeaders[groupIdx]._SectorData.GetPlanet(Planets[planetIdx]._PlanetData.PlanetName) != null)
        //    {
        //        ParentNode.AddChild(Planets[planetIdx]);
        //        planetIdx++;
        //    }
        //}

        Visible = true;
    }

    private void Refresh_HideAll()
    {
        //for (int headerIdx = 0; headerIdx < GroupHeaders.Count; headerIdx++)
        //{
        //    GroupHeaders[headerIdx].Visible = false;
        //}
        GroupHeader.Visible = false;
        for (int groupIdx = 0; groupIdx < Groups.Count; groupIdx++)
        {
            Groups[groupIdx].Visible = false;
        }
        for (int planetIdx = 0; planetIdx < Planets.Count; planetIdx++)
        {
            Planets[planetIdx].Visible = false;
        }
    }

    //public void RefreshHeader(SectorData sectorData)
    //{
    //    //while (idx >= GroupHeaders.Count) 
    //    //{
    //    //    UIBarListHeader newGroup = GroupHeaders[0].Duplicate(7) as UIBarListHeader;
    //    //    GroupHeaders[0].GetParent().AddChild(newGroup);
    //    //    GroupHeaders.Add(newGroup);
    //    //}
    //
    //    GroupHeaders[idx].Refresh(sectorData);
    //}

    public void RefreshGroup(int idx, SectorData sectorData)
    {
        while (idx >= Groups.Count)
        {
            UIBarListGroup newGroup = Groups[0].Duplicate(7) as UIBarListGroup;
            Groups[0].GetParent().AddChild(newGroup);
            Groups.Add(newGroup);
        }

        Groups[idx].Refresh(sectorData);
        Groups[idx].Visible = true;
        Tabs.Visible = false;
    }

    public void RefreshGroup(int idx, SystemData systemData)
    {
        while (idx >= Groups.Count)
        {
            UIBarListGroup newGroup = Groups[0].Duplicate(7) as UIBarListGroup;
            Groups[0].GetParent().AddChild(newGroup);
            Groups.Add(newGroup);
        }

        Groups[idx].Refresh(systemData);
        Groups[idx].Visible = true;
        Tabs.Visible = false;
    }

    public void RefreshPlanet(int idx, StarData systemData, PlanetData planetData)
    {
        while (idx >= Planets.Count)
        {
            UIBarListPlanet newPlanet = Planets[0].Duplicate(7) as UIBarListPlanet;
            Planets[0].GetParent().AddChild(newPlanet);
            Planets.Add(newPlanet);
        }

        PlanetData parentPlanet = null;
        if (planetData.Data.GetSub("Moon", false) != null)
        {
            for (int systemPlanetIdx = 0; systemPlanetIdx < systemData.Planets.Count; systemPlanetIdx++)
            {
                if (systemData.Planets[systemPlanetIdx].Data.GetSub("Moon", false) == null)
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
        Planets[idx].Visible = true;
    }

    //public void Hover(UIBarListPlanet planet)
    //{
    //}

    //public void Unhover(UIBarListPlanet planet)
    //{
    //}

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

    public void Hover(UIBarListPlanet planet)
    {
        //Game.GalaxyUI.HoverPlanet(planet._PlanetData);//TEMP02
    }

    public void Unhover()
    {
        //Game.GalaxyUI.UnhoverPlanet();//TEMP02
    }

    public void Select(PlanetData planet)
    {
        if (planet == null)
        {
            Tabs.Visible = false;
            PlanetSelected = null;
        }
        else
        {
            for (int idx = 0; idx < Planets.Count; idx++)
            {
                if (Planets[idx]._PlanetData == planet)
                {
                    PlanetSelected = Planets[idx];
                    Tabs.Visible = true;
                }
            }
        }

        //RefreshInfoPanels(planetUI);

        // deselect other planets
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (Planets[idx] != PlanetSelected)
            {
                Planets[idx].Deselect();
            }
        }
    }

    public void Select(UIBarListPlanet planet)
    {
        PlanetSelected = planet;
        
        //RefreshInfoPanels(planetUI);
        
        // deselect other planets
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (Planets[idx] != PlanetSelected)
            {
                Planets[idx].Deselect();
            }
        }
    }

    public void Select(UIBarListGroup group, bool ret)
    {
        if (group._SystemData != null)
        {
            if (ret) Refresh(group._SystemData._Sector);
            else Refresh(group._SystemData);
        }
        else
        {
            if (ret) Refresh(group._SectorData._Player);
            else Refresh(group._SectorData);
        }
        //PlanetSelected = planetUI;
        //
        //RefreshInfoPanels(planetUI);
        //
        //// deselect other planets
        //for (int idx = 0; idx < Planets.Count; idx++)
        //{
        //    if (Planets[idx].Selected && planetUI != Planets[idx])
        //    {
        //        Planets[idx].Deselect();
        //    }
        //}
    }

    public void Deselect()
    {
        if (PlanetSelected != null)
        {
            PlanetSelected.Deselect();
        }
        
        PlanetSelected = null;

        Tabs.Visible = false;
    }

    public void ShowTabsSelector(bool buildingsAvailable, bool popsAvailable)
    {
        Tabs.Visible = true;

        Tabs_BuildingsAvailable.Visible = buildingsAvailable;
        Tabs_PopulationAvailable.Visible = popsAvailable;
    }

    public void HideTabsSelector()
    {
        Tabs.Visible = false;
    }

    public void ShowBuildingsTab()
    {
        Tabs_BuildingsSelected.Visible = true;
        Tabs_PopulationSelected.Visible = false;
    }

    public void ShowPopulationTab()
    {
        Tabs_BuildingsSelected.Visible = false;
        Tabs_PopulationSelected.Visible = true;
    }

    public void HideAllTabs()
    {
        Tabs_BuildingsSelected.Visible = false;
        Tabs_PopulationSelected.Visible = false;
    }
}