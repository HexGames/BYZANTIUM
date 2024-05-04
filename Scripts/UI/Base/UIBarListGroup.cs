using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

//[Tool]
public partial class UIBarListGroup : Control
{
    // is beeing duplicated
    private UIGalaxyBarList GalaxyList = null;
    private RichTextLabel PlanetName = null;
    private string PlanetName_Original = null;
    private TextureRect Sector = null;
    private TextureRect System = null;
    private Control Sub_1 = null;
    private RichTextLabel Sub_1_Name = null;
    private string Sub_1_Name_Original = null;
    private Control Sub_2 = null;
    private RichTextLabel Sub_2_Name = null;
    private string Sub_2_Name_Original = null;
    private Control Sub_3 = null;
    private RichTextLabel Sub_3_Name = null;
    private string Sub_3_Name_Original = null;
    private Panel Selected = null;

    //[ExportCategory("Runtime")]
    //[Export]
    //public bool IsSelected = false;

    [Export]
    public bool Parent = false;
    [Export]
    public SectorData _SectorData = null;
    [Export]
    public SystemData _SystemData = null;


    Game Game;

    private bool LockBuildButton = false;
    private bool HasPossibleBuildings = false;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
            GalaxyList = GetNode<Control>("../../") as UIGalaxyBarList;
            PlanetName = GetNode<RichTextLabel>("Mask/NameBackground/Name");
            PlanetName_Original = PlanetName.Text;
            Sector = GetNode<TextureRect>("Mask/Sector");
            System = GetNode<TextureRect>("Mask/System");
            Sub_1 = GetNode<Control>("Mask/ListBackground_1");
            Sub_1_Name = GetNode<RichTextLabel>("Mask/ListBackground_1/Text");
            Sub_1_Name_Original = Sub_1_Name.Text;
            Sub_2 = GetNode<Control>("Mask/ListBackground_2");
            Sub_2_Name = GetNode<RichTextLabel>("Mask/ListBackground_2/Text");
            Sub_2_Name_Original = Sub_2_Name.Text;
            Sub_3 = GetNode<Control>("Mask/ListBackground_3");
            Sub_3_Name = GetNode<RichTextLabel>("Mask/ListBackground_3/Text");
            Sub_3_Name_Original = Sub_3_Name.Text;
            Selected = GetNode<Panel>("Selected");

            Visible = false;
        }
    }

    public void Refresh(SectorData sectorData, bool parent = false)
    {
        _SectorData = sectorData;
        _SystemData = null;
        Parent = parent;
        Selected.Visible = false;

        Name = _SectorData.SectorName + "_UI";

        PlanetName.Text = PlanetName_Original.Replace("$name", _SectorData.SectorName);

        Sector.Visible = true;
        System.Visible = false;

        List<SystemData> orderedSystems = new List<SystemData>();
        orderedSystems.AddRange(_SectorData.Systems);

        //orderedSystems.Sort((x, y) => { return x.; });

        Sub_1.Visible = false;
        Sub_2.Visible = false;
        Sub_3.Visible = false;
        if (Parent == false)
        {
            for (int idx = 0; idx < orderedSystems.Count; idx++)
            {
                if (idx == 0)
                {
                    Sub_1.Visible = true;
                    Sub_1_Name.Text = Sub_1_Name_Original.Replace("$name", orderedSystems[idx].SystemName);
                }
                else if (idx == 1)
                {
                    Sub_2.Visible = true;
                    Sub_2_Name.Text = Sub_2_Name_Original.Replace("$name", orderedSystems[idx].SystemName);
                }
                else if (idx == 2)
                {
                    Sub_3.Visible = true;
                    if (orderedSystems.Count == 3)
                    {
                        Sub_3_Name.Text = Sub_3_Name_Original.Replace("$name", orderedSystems[idx].SystemName);
                    }
                    else
                    {
                        Sub_3_Name.Text = Sub_3_Name_Original.Replace("$name", "+" + (orderedSystems.Count - 2).ToString());
                    }
                }
            }
        }

        Visible = true;
    }

    public void Refresh(SystemData systemData, bool parent = false)
    {
        _SectorData = null; // sectorData;
        _SystemData = systemData;
        Parent = parent;
        Selected.Visible = false;

        Name = _SystemData.SystemName + "_UI";

        PlanetName.Text = PlanetName_Original.Replace("$name", _SystemData.SystemName);

        Sector.Visible = false;
        System.Visible = true;

        List<ColonyData> orderedColonies = new List<ColonyData>();
        orderedColonies.AddRange(_SystemData.Colonies);

        //orderedSystems.Sort((x, y) => { return x.; });

        Sub_1.Visible = false;
        Sub_2.Visible = false;
        Sub_3.Visible = false;
        if (Parent == false)
        {
            for (int idx = 0; idx < orderedColonies.Count; idx++)
            {
                if (idx == 0)
                {
                    Sub_1.Visible = true;
                    Sub_1_Name.Text = Sub_1_Name_Original.Replace("$name", orderedColonies[idx].ColonyName);
                }
                else if (idx == 1)
                {
                    Sub_2.Visible = true;
                    Sub_2_Name.Text = Sub_2_Name_Original.Replace("$name", orderedColonies[idx].ColonyName);
                }
                else if (idx == 2)
                {
                    Sub_3.Visible = true;
                    if (orderedColonies.Count == 3)
                    {
                        Sub_3_Name.Text = Sub_3_Name_Original.Replace("$name", orderedColonies[idx].ColonyName);
                    }
                    else
                    {
                        Sub_3_Name.Text = Sub_3_Name_Original.Replace("$name", "+" + (orderedColonies.Count - 2).ToString());
                    }
                }
            }
        }

        Visible = true;
    }

    public void OnHover()
    {
        if (GalaxyList != null)
        {
            //GalaxyList.Hover(this);
        }
    }

    public void OnDehover()
    {
        if (GalaxyList != null)
        {
            //GalaxyList.Unhover();
        }
    }

    public void OnSelect()
    {
        //if (IsSelected) return;
        //IsSelected = true;

        Selected.Visible = true;

        //if (GalaxyList != null)
        //{
        //    GalaxyList.Select(this, Parent);
        //}
        if (_SystemData != null)
        {
            if (Parent == false)
            {
                Game.Input.SelectStar(_SystemData.Star);
            }
            else
            {
                Game.Input.DeselectAllButSector();
            }
        }
        else if (_SectorData != null)
        {
            if (Parent == false)
            {
                Game.Input.SelectSector(_SectorData);
            }
            else
            {
                Game.Input.DeselectAll();
            }
        }
    }

    public void Deselect()
    {
        Selected.Visible = false;

        //IsSelected = false;
    }
}