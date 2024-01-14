using Godot;
using Godot.Collections;

[Tool]
public partial class SystemUI : Control
{
    [Export]
    public Array<ColorRect> Planets = new Array<ColorRect>();
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
            Planets.Add(GetTree().EditedSceneRoot.GetNode<ColorRect>("VBoxContainer/HBoxContainer/Planet_" + n.ToString()));
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
}