using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UISystem : Control
{
    [Export]
    public UISystemPanel PlanetPanel = null;
    [Export]
    public UISystemPanel TerraformingPanel = null;
    [Export]
    public UISystemPanel ExplorationPanel = null;
    [Export]
    public UISystemPanel ColonyPanel = null;
    [Export]
    public UISystemPanel SupportPanel = null;
    [Export]
    public UISystemPanel BuildingsPanel = null;

    [Export]
    public Array<UISystemPlanet> Planets = new Array<UISystemPlanet>();

    [Export]
    public UIColonyActionBuild ActionBuild = null;

    [Export]
    public SystemData _SystemData = null;
    [Export]
    public ColonyData _ColonyData = null;
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

            PlanetPanel.Visible = false;
            TerraformingPanel.Visible = false;
            ExplorationPanel.Visible = false;
            ColonyPanel.Visible = false;
            SupportPanel.Visible = false;
            BuildingsPanel.Visible = false;
        }
    }

    public void Refresh( SystemData system )
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

            PlanetPanel.Visible = false;
            TerraformingPanel.Visible = false;
            ExplorationPanel.Visible = false;
            ColonyPanel.Visible = false;
            SupportPanel.Visible = false;
            BuildingsPanel.Visible = false;

            _SystemData = null;
            _ColonyData = null;
            PlanetSelected = null;

            Visible = false;
            Game.Camera.UILockSystem = false;

            return;
        }

        _SystemData = system;

        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (idx < _SystemData.Planets.Count)
            {
                Planets[idx].Refresh(system, _SystemData.Planets[idx]);
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
        ColonyData colony = _SystemData.GetColony(planetUI._Data);

        PlanetPanel.Visible = true;
        TerraformingPanel.Visible = false;
        ExplorationPanel.Visible = false;

        RefreshInfoPanels_Default(PlanetPanel, planetProperties);

        if (colony != null)
        {
            if (_ColonyData != colony)
            {
                _ColonyData = colony;
                _ColonyData._Node.Select();

                ColonyPanel.Visible = true;
                SupportPanel.Visible = true;
                BuildingsPanel.Visible = true;

                Array<DataBlock> colonySubs = new Array<DataBlock>();
                colonySubs.AddRange(_ColonyData.Resources.GetSubs());
                colonySubs.AddRange(_ColonyData.Bonuses.GetSubs());

                RefreshInfoPanels_Default(ColonyPanel, colonySubs);

                ActionBuild.Refresh(_ColonyData);
            }
        }
        else
        {
            _ColonyData = null;

            ColonyPanel.Visible = false;
            SupportPanel.Visible = false;
            BuildingsPanel.Visible = false;
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