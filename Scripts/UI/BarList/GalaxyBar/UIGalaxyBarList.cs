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
    public Array<UIGalaxyBarListGroupHeader> GroupHeaders = new Array<UIGalaxyBarListGroupHeader>();

    [Export]
    public Array<UIBarListPlanet> Planets = new Array<UIBarListPlanet>();

    //[Export]
    //public Array<UIPawnListFleet> Fleets = new Array<UIPawnListFleet>();


    [ExportCategory("Runtime")]
    [Export]
    public bool GroupByType = false;
    [Export]
    public PlayerData _PlayerData = null;
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

        // hide all
        for (int groupIdx = 0; groupIdx < GroupHeaders.Count; groupIdx++)
        {
            GroupHeaders[groupIdx].Visible = false;
        }
        for (int planetIdx = 0; planetIdx < Planets.Count; planetIdx++)
        {
            Planets[planetIdx].Visible = false;
        }

        if (GroupByType == false)
        {
            // refresh groups and planets
            //int refreshedGroupIdx = 0;
            //for (int colonyIdx = 0; colonyIdx < _PlayerData.Colonies.Count; colonyIdx++)
            //{
            //    bool foundSystem = false;
            //    for (int groupIdx = 0; groupIdx < GroupHeaders.Count; groupIdx++)
            //    {
            //        if (_PlayerData.Colonies[colonyIdx].Star == GroupHeaders[groupIdx]._SystemData)
            //        {
            //            foundSystem = true;
            //            break;
            //        }
            //    }
            //    if (foundSystem == false)
            //    {
            //        RefreshGroup(refreshedGroupIdx, _PlayerData.Colonies[colonyIdx].Star);
            //        refreshedGroupIdx++;
            //    }
            //
            //    RefreshPlanets(colonyIdx, _PlayerData.Colonies[colonyIdx].Star, _PlayerData.Colonies[colonyIdx].Planet);
            //}
            int refreshedPlanetIdx = 0;
            for (int sectorIdx = 0; sectorIdx < _PlayerData.Sectors.Count; sectorIdx++)
            {
                RefreshGroup(sectorIdx, _PlayerData.Sectors[sectorIdx]);
                for (int systemIdx = 0; systemIdx < _PlayerData.Sectors[sectorIdx].Systems.Count; systemIdx++)
                {
                    SystemData system = _PlayerData.Sectors[sectorIdx].Systems[systemIdx];
                    for (int colonyIdx = 0; colonyIdx < system.Colonies.Count; colonyIdx++)
                    {
                        ColonyData colony = system.Colonies[colonyIdx];
                        RefreshPlanets(refreshedPlanetIdx, system.Star, colony.Planet);
                        refreshedPlanetIdx++;
                    }
                }
            }

            // unparent
            for (int idx = 0; idx < GroupHeaders.Count; idx++)
            {
                ParentNode.RemoveChild(GroupHeaders[idx]);
            }
            for (int idx = 0; idx < Planets.Count; idx++)
            {
                ParentNode.RemoveChild(Planets[idx]);
            }

            // reorder

            // parent
            int planetIdx = 0;
            for (int groupIdx = 0; groupIdx < GroupHeaders.Count; groupIdx++)
            {
                ParentNode.AddChild(GroupHeaders[groupIdx]);
                while (planetIdx < Planets.Count && GroupHeaders[groupIdx]._SectorData.GetPlanet(Planets[planetIdx]._PlanetData.PlanetName) != null)
                {
                    ParentNode.AddChild(Planets[planetIdx]);
                    planetIdx++;
                }
            }
        }

        Visible = true;
    }

    public void RefreshGroup(int idx, SectorData sectorData)
    {
        while (idx >= GroupHeaders.Count) 
        {
            UIGalaxyBarListGroupHeader newGroup = GroupHeaders[0].Duplicate(7) as UIGalaxyBarListGroupHeader;
            GroupHeaders[0].GetParent().AddChild(newGroup);
            GroupHeaders.Add(newGroup);
        }

        GroupHeaders[idx].Refresh(sectorData);
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

    public void Select(UIBarListPlanet planet)
    {
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
}