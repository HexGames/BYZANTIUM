using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UISystem : Control
{
    [ExportCategory("Links")]
    [Export]
    public UIPawnList SystemBar = null;
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

    //[Export]
    //public Array<UISystemPlanet> Planets = new Array<UISystemPlanet>();

    [Export]
    public UIColonyActionBuild ActionBuild = null;

    [ExportCategory("Runtime")]
    [Export]
    public SystemData _SystemData = null;
    [Export]
    public ColonyData _ColonyData = null;
    [Export]
    public UIPawnListPlanet PlanetSelected = null;

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
            SystemBar.Visible = false;

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

        SystemBar.Refresh(system);

        //Game.ActionColonyUI.Visible = false;
        Visible = true;
        Game.Camera.UILockSystem = true;
    }

    public void Select(UIPawnListPlanet planetUI)
    {
        PlanetSelected = planetUI;

        RefreshInfoPanels(planetUI);

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

    private void RefreshInfoPanels(UIPawnListPlanet planetUI)
    {
        // make the proper panel visible
        Array<DataBlock> planetProperties = planetUI._PlanetData.GetSubs();
        ColonyData colony = _SystemData.GetColony(planetUI._PlanetData);

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
                RefreshInfoPanels_Buildings(BuildingsPanel, _ColonyData.Buildings.GetSubs());

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
    }

    public class PropertyInfo
    {
        public DataBlock _Data;
        public string Text;
        public Color BGColor;
        public int Row;
        public string Tooltip = "";
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

    private void RefreshInfoPanels_Buildings(UISystemPanel panel, Array<DataBlock> properties)
    {
        List<PropertyInfo> propertiesInfos = new List<PropertyInfo>();
        for (int idx = 0; idx < properties.Count; idx++)
        {
            PropertyInfo info = new PropertyInfo();
            info._Data = properties[idx];
            info.Text = properties[idx].ToUIString();
            info.BGColor = properties[idx].ToUIColor();
            info.Row = properties[idx].ToUIRow();
            DataBlock benefit = Game.Def.GetBuilding(properties[idx].Name).GetSub("Benefit");
            if (benefit != null) info.Tooltip = benefit.ToToolTipString();
            propertiesInfos.Add(info);
        }

        propertiesInfos.Sort((x, y) => x.Row.CompareTo(y.Row));

        panel.Refresh(propertiesInfos);
    }
}