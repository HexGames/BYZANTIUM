using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UISystem : Control
{
    [Export]
    public UISystemPanel Planet = null;
    [Export]
    public UISystemPanel Terraforming = null;
    [Export]
    public UISystemPanel Exploration = null;
    [Export]
    public UISystemPanel Colony = null;
    [Export]
    public UISystemPanel Support = null;
    [Export]
    public UISystemPanel Buildings = null;

    [Export]
    public Array<UISystemPlanet> Planets = new Array<UISystemPlanet>();

    [Export]
    public Array<DataBlock> _PlanetsData = null;
    [Export]
    public UISystemPlanet PlanetSelected = null;


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

            Planet.Visible = false;
            Terraforming.Visible = false;
            Exploration.Visible = false;
            Colony.Visible = false;
            Support.Visible = false;
            Buildings.Visible = false;
        }
    }

    public void Refresh( LocationData system )
    {
        if (system == null)
        {
            // deselect other planets
            for (int idx = 0; idx < Planets.Count; idx++)
            {
                if (Planets[idx].Selected)
                {
                    Planets[idx].Deselect();
                }
            }

            Visible = false;
            Game.Camera.UILockSystem = false;

            return;
        }

        _PlanetsData = system.System.GetSubs("Planet");

        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (idx < _PlanetsData.Count)
            {
                Planets[idx].Refresh(system, _PlanetsData[idx]);
                Planets[idx].Visible = true;
            }
            else
            {
                Planets[idx].Visible = false;
            }
        }
        //Game.ActionColonyUI.Visible = false;
        Visible = true;
        Game.Camera.UILockSystem = true;
    }

    public void Select(UISystemPlanet planetUI)
    {
        PlanetSelected = planetUI;

        RefreshInfoPanels(planetUI);

        // deselect other planets
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (Planets[idx].Selected && planetUI != Planets[idx])
            {
                Planets[idx].Deselect();
            }
        }

        //if (Data.GetPlayer(Game.Map.Data, PlanetSelected._Data)?.Human == true)
        //{
        //    Game.ActionColonyUI.Refresh(PlanetSelected);
        //    Game.ActionColonyUI.Visible = true;
        //}
        //else
        //{
        //    Game.ActionColonyUI.Visible = false;
        //}
    }

    private void RefreshInfoPanels(UISystemPlanet planetUI)
    {
        // make the proper panel visible
        Array<DataBlock> planetProperties = planetUI._Data.GetSubs();
        PlayerData playerData = null;
        DataBlock colony = Data.GetPlayerColony(Game.Map.Data, planetUI._Data, out playerData);

        Planet.Visible = true;
        Terraforming.Visible = false;
        Exploration.Visible = false;

        RefreshInfoPanels_Default(Planet, planetProperties);

        if (colony != null)
        {
            Colony.Visible = true;
            Support.Visible = true;
            Buildings.Visible = true;

            Array<DataBlock> colonySubs = colony.GetSubs();
            RefreshInfoPanels_Default(Colony, colonySubs);
        }
        else
        {
            Colony.Visible = false;
            Support.Visible = false;
            Buildings.Visible = false;
        }

        //Array<DataBlock> Properties = new Array<DataBlock>();
        //
        //for (int idx = 0; idx < planetProperties.Count; idx++)
        //{
        //    planetProperties
        //}
        //
        //if (selectedIdx < (_PlanetsData.Count + 1) / 2)
        //{
        //    for (int idx = 0; idx < PlaneRightRows.Count; idx++) PlaneRightRows[idx].Visible = false;
        //    for (int idx = 0; idx < PlaneRightRowsProperies.Count; idx++) PlaneRightRowsProperies[idx].Visible = false;
        //    
        //    if (playerData != null)
        //    {
        //        string text = "Owned by " + playerData.PlayerName;
        //        Color color = Game.UILib.GetPlayerColor(playerData.PlayerName);
        //
        //        StyleBoxFlat styleBox = new StyleBoxFlat();
        //        styleBox.BgColor = color;
        //
        //        PlaneRightRowsProperies[0].Text = text;
        //        PlaneRightRowsProperies[0].Visible = true;
        //        PlaneRightRowsProperies[0].AddThemeStyleboxOverride("normal", styleBox);
        //        PlaneRightRows[0].Visible = true;
        //    }
        //
        //    for (int idx = 0; idx < planetProperties.Count; idx++)
        //    {
        //        int row = planetProperties[idx].ToUIRow();
        //
        //        if (row >= 0 && row < 7)
        //        {
        //            for (int propertyIdx = row * 9; propertyIdx < row * 9 + 9; propertyIdx++)
        //            {
        //                if (PlaneRightRowsProperies[propertyIdx].Visible == false)
        //                {
        //                    string text = planetProperties[idx].ToUIString();
        //                    Color color = planetProperties[idx].ToUIColor();
        //
        //                    StyleBoxFlat styleBox = new StyleBoxFlat();
        //                    styleBox.BgColor = color;
        //
        //                    PlaneRightRowsProperies[propertyIdx].Text = text;
        //                    PlaneRightRowsProperies[propertyIdx].Visible = true;
        //                    PlaneRightRowsProperies[propertyIdx].AddThemeStyleboxOverride("normal", styleBox);
        //                    PlaneRightRows[row].Visible = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //
        //    PlanetLeft.Visible = false;
        //    PlanetRight.Visible = true;
        //}
    }

    public class PropertyInfo
    {
        public DataBlock _Data;
        public string Text;
        public Color BGColor;
        public int Row;
    }
    private void RefreshInfoPanels_Default(UISystemPanel panel, Array<DataBlock> properties)
    {
        List<PropertyInfo> propertiesInfos = new List<PropertyInfo>();
        for (int idx = 0; idx < properties.Count; idx++)
        {
            PropertyInfo info = new PropertyInfo();
            info._Data = properties[idx];
            info.Text = properties[idx].ToUIString();
            info.BGColor = properties[idx].ToUIColor();
            info.Row = properties[idx].ToUIRow();
            propertiesInfos.Add(info);
        }

        propertiesInfos.Sort((x, y) => x.Row.CompareTo(y.Row));

        panel.Refresh(propertiesInfos);
    }
}