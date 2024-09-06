using Godot;
using Godot.Collections;
using System.Data;

public partial class UI3DPlanet : Control
{
    // is beeing duplicated
    TextureRect Arrow = null;
    PanelContainer Row_1_Bg = null;
    PanelContainer Row_2_Bg = null;
    PanelContainer Row_3_Bg = null;
    RichTextLabel Row_1 = null;
    RichTextLabel Row_2 = null;
    RichTextLabel Row_3 = null;

    [Export]
    public GFXStarOrbit GFX = null;

    public override void _Ready()
    {
        Arrow = GetNode<TextureRect>("VBoxContainer/TextureRect");
        Row_1_Bg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_1");
        Row_2_Bg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_2");
        Row_3_Bg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_3");
        Row_1 = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_1/System");
        Row_2 = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_2/Colony");
        Row_3 = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_3/Extra");
    }

    public void Refresh()
    {
        Row_1.Text = GFX._Planet.PlanetName;
        Row_1_Bg.Visible = true;

        if (GFX._Planet.Colony != null)
        {
            Arrow.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Planet.Colony._System._Player.PlayerID);

            Row_1_Bg.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Planet.Colony._System._Player.PlayerID);

            Row_2.Text = "9P 5C 7U";
            Row_2_Bg.Visible = true;
            Row_2_Bg.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Planet.Colony._System._Player.PlayerID);

            Row_3.Text = "3P 3BC";
            Row_3_Bg.Visible = true;
            Row_3_Bg.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Planet.Colony._System._Player.PlayerID);
        }
        else
        {
            Arrow.SelfModulate = new Color(0.75f, 0.75f, 0.75f, 0.75f);
            Row_1_Bg.SelfModulate = new Color(0.5f, 0.5f, 0.5f);

            Row_2_Bg.Visible = false;
            Row_3_Bg.Visible = false;
        }
    }
}