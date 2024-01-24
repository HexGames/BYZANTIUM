using Godot;
using Godot.Collections;

//[Tool]
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
            Game = GetNode<Game>("/root/Main/Game");
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
        Game.ActionColonyUI.Visible = false;
        Visible = true;
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

        if (Data.GetPlayer(Game.Map.Data, PlanetSelected._Data)?.Human == true)
        {
            Game.ActionColonyUI.Refresh(PlanetSelected);
            Game.ActionColonyUI.Visible = true;
        }
        else
        {
            Game.ActionColonyUI.Visible = false;
        }
    }

    private void RefreshInfoPanels(UISystemPlanet planetUI)
    {
        // change the panels parent
        if (planetUI != PlanetLeft.GetParent())
        {
            PlanetLeft.Reparent(planetUI, false);
        }

        if (planetUI != PlanetRight.GetParent())
        {
            PlanetRight.Reparent(planetUI, false);
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

        Array<DataBlock> planetProperties = planetUI._Data.GetSubs();
        PlayerData playerData = null;
        DataBlock colony = Data.GetPlayerColony(Game.Map.Data, planetUI._Data, out playerData);
        if (colony != null)
        {
            Array<DataBlock> colonySubs = colony.GetSubs();
            for (int idx = 0; idx < colonySubs.Count; idx++)
            {
                planetProperties.Add(colonySubs[idx]);
                planetProperties.AddRange(colonySubs[idx].Subs);
            }
        }

        if (selectedIdx < (_PlanetsData.Count + 1) / 2)
        {
            for (int idx = 0; idx < PlaneRightRows.Count; idx++) PlaneRightRows[idx].Visible = false;
            for (int idx = 0; idx < PlaneRightRowsProperies.Count; idx++) PlaneRightRowsProperies[idx].Visible = false;
            
            if (playerData != null)
            {
                string text = "Owned by " + playerData.PlayerName;
                Color color = Game.UILib.GetPlayerColor(playerData.PlayerName);

                StyleBoxFlat styleBox = new StyleBoxFlat();
                styleBox.BgColor = color;

                PlaneRightRowsProperies[0].Text = text;
                PlaneRightRowsProperies[0].Visible = true;
                PlaneRightRowsProperies[0].AddThemeStyleboxOverride("normal", styleBox);
                PlaneRightRows[0].Visible = true;
            }

            for (int idx = 0; idx < planetProperties.Count; idx++)
            {
                int row = planetProperties[idx].ToUIRow();

                if (row >= 0 && row < 7)
                {
                    for (int propertyIdx = row * 9; propertyIdx < row * 9 + 9; propertyIdx++)
                    {
                        if (PlaneRightRowsProperies[propertyIdx].Visible == false)
                        {
                            string text = planetProperties[idx].ToUIString();
                            Color color = planetProperties[idx].ToUIColor();

                            StyleBoxFlat styleBox = new StyleBoxFlat();
                            styleBox.BgColor = color;

                            PlaneRightRowsProperies[propertyIdx].Text = text;
                            PlaneRightRowsProperies[propertyIdx].Visible = true;
                            PlaneRightRowsProperies[propertyIdx].AddThemeStyleboxOverride("normal", styleBox);
                            PlaneRightRows[row].Visible = true;
                            break;
                        }
                    }
                }
            }

            PlanetLeft.Visible = false;
            PlanetRight.Visible = true;
        }
        else
        {
            for (int idx = 0; idx < PlaneLeftRows.Count; idx++) PlaneLeftRows[idx].Visible = false;
            for (int idx = 0; idx < PlaneLeftRowsProperies.Count; idx++) PlaneLeftRowsProperies[idx].Visible = false;

            if (playerData != null)
            {
                string text = "Owned by " + playerData.PlayerName;
                Color color = Game.UILib.GetPlayerColor(playerData.PlayerName);

                StyleBoxFlat styleBox = new StyleBoxFlat();
                styleBox.BgColor = color;

                PlaneLeftRowsProperies[0].Text = text;
                PlaneLeftRowsProperies[0].Visible = true;
                PlaneLeftRowsProperies[0].AddThemeStyleboxOverride("normal", styleBox);
                PlaneLeftRows[0].Visible = true;
            }

            for (int idx = 0; idx < planetProperties.Count; idx++)
            {
                int row = planetProperties[idx].ToUIRow();
                string text = planetProperties[idx].ToUIString();

                if (row >= 0 && row < 7)
                {
                    for (int propertyIdx = row * 9; propertyIdx < row * 9 + 9; propertyIdx++)
                    {
                        if (PlaneLeftRowsProperies[propertyIdx].Visible == false)
                        {
                            PlaneLeftRowsProperies[propertyIdx].Text = text;
                            PlaneLeftRowsProperies[propertyIdx].Visible = true;
                            PlaneLeftRows[row].Visible = true;
                            break;
                        }
                    }
                }
            }

            PlanetLeft.Visible = true;
            PlanetRight.Visible = false;
        }
    }
}