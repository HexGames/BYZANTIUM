using Godot;
using Godot.Collections;

[Tool]
public partial class UISystem : Control
{
    [Export]
    public Array<UISystemPlanet> Planets = new Array<UISystemPlanet>();
    [Export]
    public Control PlanetLeft = null;
    [Export]
    public Control PlanetRight = null;
    [Export]
    public Array<BoxContainer> PlaneLeftRows = new Array<BoxContainer>();
    [Export]
    public Array<BoxContainer> PlaneRightRows = new Array<BoxContainer>();
    [Export]
    public Array<Label> PlaneLeftRowsProperies = new Array<Label>();
    [Export]
    public Array<Label> PlaneRightRowsProperies = new Array<Label>();

    [Export]
    public UISystemPlanet PlanetSelected = null;

    [Export]
    public bool AutoLink
    {
        get => false;
        set
        {
            if (value)
            {
                AutoLinkFunc();
            }
        }
    }

    DefLibrary DefLib;

    public void AutoLinkFunc()
    {
        Planets.Clear();
        PlanetLeft = null;
        PlanetRight = null;
        PlaneLeftRows.Clear();
        PlaneRightRows.Clear();
        PlaneLeftRowsProperies.Clear();
        PlaneRightRowsProperies.Clear();

        for (int n = 0; n < 13; n++)
        {
            Planets.Add(GetTree().EditedSceneRoot.GetNode<UISystemPlanet>("VBoxContainer/HBoxContainer/Planet_" + n.ToString()));
        }

        PlanetLeft = GetTree().EditedSceneRoot.GetNode<Control>("VBoxContainer/HBoxContainer/Planet_1/PanelLeft");
        PlanetRight = GetTree().EditedSceneRoot.GetNode<Control>("VBoxContainer/HBoxContainer/Planet_1/PanelRight");
        for (int n1 = 1; n1 < 8; n1++)
        {
            PlaneLeftRows.Add(GetTree().EditedSceneRoot.GetNode<BoxContainer>("VBoxContainer/HBoxContainer/Planet_1/PanelLeft/VBoxContainer/HBoxContainer" + n1.ToString()));
            PlaneRightRows.Add(GetTree().EditedSceneRoot.GetNode<BoxContainer>("VBoxContainer/HBoxContainer/Planet_1/PanelRight/VBoxContainer/HBoxContainer" + n1.ToString()));
            for (int n2 = 1; n2 < 10; n2++)
            {
                PlaneLeftRowsProperies.Add(GetTree().EditedSceneRoot.GetNode<Label>("VBoxContainer/HBoxContainer/Planet_1/PanelLeft/VBoxContainer/HBoxContainer" + n1.ToString() + "/Label" + n2.ToString()));
                PlaneRightRowsProperies.Add(GetTree().EditedSceneRoot.GetNode<Label>("VBoxContainer/HBoxContainer/Planet_1/PanelRight/VBoxContainer/HBoxContainer" + n1.ToString() + "/Label" + n2.ToString()));
            }
        }
    }
    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            DefLib = GetNode<DefLibrary>("/root/Main/DefLibrary");
            //OnSelect += PlayerInput.SelectLocation;
            Visible = false;
        }
    }

    public void Refresh( LocationData system )
    {
        if (system == null)
        {
            Visible = false;
            return;
        }

        Array<DataBlock> planetsData = system.System.GetSubs(DefLib.GetDBType("Planet"));

        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (idx < planetsData.Count)
            {
                Planets[idx].Refresh(system, planetsData[idx]);
                Planets[idx].Visible = true;
            }
            else
            {
                Planets[idx].Visible = false;
            }
        }
        Visible = true;
    }

    public void Select(UISystemPlanet planetUI)
    {
        PlanetSelected = planetUI;

        // change the panels parent
        if (PlanetSelected != PlanetLeft.GetParent())
        {
            PlanetLeft.Reparent(PlanetSelected, false);
        }

        if (PlanetSelected != PlanetRight.GetParent())
        {
            PlanetRight.Reparent(PlanetSelected, false);
        }

        // make the proper panel visible
        int selectedIdx = 0;
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (planetUI == Planets[idx])
            {
                selectedIdx = idx;
                break;
            }
        }

        if (selectedIdx < Planets.Count / 2)
        {
            PlanetLeft.Visible = false;
            PlanetRight.Visible = true;
        }
        else
        {
            PlanetLeft.Visible = true;
            PlanetRight.Visible = false;
        }

        // deselectt other planets
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (Planets[idx].Selected && planetUI != Planets[idx])
            {
                Planets[idx].Deselect();
            }
        }
    }
}