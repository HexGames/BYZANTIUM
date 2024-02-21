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
    public UIBudget Budget = null;

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

            Budget.Visible = false;
        }
    }

    public void Refresh( SystemData system )
    {
        if (system == null)
        {
            // deselect other planets
            SystemBar.Visible = false;

            Budget.Visible = false;

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

        Budget.RefreshBudget(colony.Budget, false, false);
        Budget.Visible = true;
    }

    /*public class PropertyInfo
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
            DataBlock buildingInfo = Game.Def.GetBuilding(properties[idx].Name); 
            if (buildingInfo == null)
            {
                GD.Print("BUILDING NOT FOUND! - " + properties[idx].Name);
                continue;
            }
            DataBlock benefit = buildingInfo.GetSub("Benefit");
            if (benefit != null) info.Tooltip = benefit.ToToolTipString();
            propertiesInfos.Add(info);
        }

        propertiesInfos.Sort((x, y) => x.Row.CompareTo(y.Row));

        panel.Refresh(propertiesInfos);
    }*/
}