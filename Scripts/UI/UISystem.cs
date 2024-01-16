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
    public Array<DataBlock> _PlanetsData = null;
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
            // deselect other planets
            for (int idx = 0; idx < Planets.Count; idx++)
            {
                if (Planets[idx].Selected)
                {
                    Planets[idx].Deselect();
                }
            }

            PlanetLeft.Visible = false;
            PlanetRight.Visible = false;

            Visible = false;

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


        if (selectedIdx < (_PlanetsData.Count + 1) / 2)
        {
            Array<DataBlock> planetProperties = PlanetSelected._Data.GetSubs();
            for (int idx = 0; idx < PlaneRightRows.Count; idx++) PlaneRightRows[idx].Visible = false;
            for (int idx = 0; idx < PlaneRightRowsProperies.Count; idx++)
            {
                if (idx < planetProperties.Count)
                {
                    PlaneRightRowsProperies[idx].Text = planetProperties[idx].ToUIString();
                    PlaneRightRowsProperies[idx].Visible = true;

                    if (PlaneRightRows[idx / 9].Visible != true)
                    {
                        PlaneRightRows[idx / 9].Visible = true;
                    }
                }
                else
                {
                    PlaneRightRowsProperies[idx].Visible = false;
                }
            }

            PlanetLeft.Visible = false;
            PlanetRight.Visible = true;
        }
        else
        {
            Array<DataBlock> planetProperties = PlanetSelected._Data.GetSubs();
            for (int idx = 0; idx < PlaneLeftRows.Count; idx++) PlaneLeftRows[idx].Visible = false;
            for (int idx = 0; idx < PlaneLeftRowsProperies.Count; idx++)
            {
                if (idx < planetProperties.Count)
                {
                    PlaneLeftRowsProperies[idx].Text = planetProperties[idx].ToUIString();
                    PlaneLeftRowsProperies[idx].Visible = true;

                    if (PlaneLeftRows[idx / 9].Visible != true)
                    {
                        PlaneLeftRows[idx / 9].Visible = true;
                    }
                }
                else
                {
                    PlaneLeftRowsProperies[idx].Visible = false;
                }
            }

            PlanetLeft.Visible = true;
            PlanetRight.Visible = false;
        }

        // deselect other planets
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (Planets[idx].Selected && planetUI != Planets[idx])
            {
                Planets[idx].Deselect();
            }
        }
    }
}