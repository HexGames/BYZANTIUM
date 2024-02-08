using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIPawnList : Control
{
    [ExportCategory("Links")]
    [Export]
    public HScrollBar ScrollBar = null;

    [Export]
    public Control ParentNode = null;

    [Export]
    public Array<UIPawnListGroupHeader> GroupHeaders = new Array<UIPawnListGroupHeader>();

    [Export]
    public Array<UIPawnListPlanet> Planets = new Array<UIPawnListPlanet>();

    //[Export]
    //public Array<UIPawnListFleet> Fleets = new Array<UIPawnListFleet>();


    [ExportCategory("Runtime")]
    [Export]
    public bool GroupByType = false;
    [Export]
    public PlayerData _PlayerData = null;
    [Export]
    public SystemData _SystemData = null;
    [Export]
    public UIPawnListPlanet PlanetSelected = null;
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

    public void Refresh(SystemData systemData)
    {
        _SystemData = systemData;

        // hide all
        for (int groupIdx = 0; groupIdx < GroupHeaders.Count; groupIdx++)
        {
            GroupHeaders[groupIdx].Visible = false;
        }
        for (int planetIdx = 0; planetIdx < Planets.Count; planetIdx++)
        {
            Planets[planetIdx].Visible = false;
        }

        // refresh planets
        for (int planetIdx = 0; planetIdx < systemData.Planets.Count; planetIdx++)
        {
            RefreshPlanets(planetIdx, systemData, systemData.Planets[planetIdx]);
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
            GD.Print("list " + planetIdx + " " + Planets[planetIdx]._PlanetData.ValueS);
        }

        Visible = true;
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
            int refreshedGroupIdx = 0;
            for (int colonyIdx = 0; colonyIdx < _PlayerData.Colonies.Count; colonyIdx++)
            {
                bool foundSystem = false;
                for (int groupIdx = 0; groupIdx < GroupHeaders.Count; groupIdx++)
                {
                    if (_PlayerData.Colonies[colonyIdx].System == GroupHeaders[groupIdx]._SystemData)
                    {
                        foundSystem = true;
                        break;
                    }
                }
                if (foundSystem == false)
                {
                    RefreshGroup(refreshedGroupIdx, _PlayerData.Colonies[colonyIdx].System);
                    refreshedGroupIdx++;
                }

                RefreshPlanets(colonyIdx, _PlayerData.Colonies[colonyIdx].System, _PlayerData.Colonies[colonyIdx].Planet);
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
                while (planetIdx < Planets.Count && GroupHeaders[groupIdx]._SystemData.Planets.Contains(Planets[planetIdx]._PlanetData))
                {
                    ParentNode.AddChild(Planets[planetIdx]);
                    planetIdx++;
                }
            }
        }

        Visible = true;
    }

    public void RefreshGroup(int idx, SystemData systemData)
    {
        while (idx >= GroupHeaders.Count) 
        {
            UIPawnListGroupHeader newGroup = GroupHeaders[0].Duplicate(7) as UIPawnListGroupHeader;
            GroupHeaders[0].GetParent().AddChild(newGroup);
            GroupHeaders.Add(newGroup);
        }

        GroupHeaders[idx].Refresh(systemData);
    }

    public void RefreshPlanets(int idx, SystemData systemData, DataBlock planetData)
    {
        while (idx >= Planets.Count)
        {
            UIPawnListPlanet newPlanet = Planets[0].Duplicate(7) as UIPawnListPlanet;
            Planets[0].GetParent().AddChild(newPlanet);
            Planets.Add(newPlanet);
        }

        DataBlock parentPlanet = null;
        if (planetData.GetSub("Moon") != null)
        {
            for (int systemPlanetIdx = 0; systemPlanetIdx < systemData.Planets.Count; systemPlanetIdx++)
            {
                if (systemData.Planets[systemPlanetIdx].GetSub("Moon") == null)
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

    public void Select(UIPawnListPlanet planet)
    {
        if (_SystemData != null)
        {
            Game.SystemUI.Select(planet);
            for (int idx = 0; idx < Planets.Count; idx++)
            {
                if (Planets[idx] != planet)
                {
                    Planets[idx].Deselect();
                }
            }
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
}