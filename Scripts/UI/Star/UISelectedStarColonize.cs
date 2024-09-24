using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISelectedStarColonize : Control
{
    [ExportCategory("Links")]
    [Export]
    public Control ColonizeBtn = null;
    [Export]
    public Control ColonizePlanetBg = null;
    [Export]
    public RichTextLabel ColonizePlanetText = null;
    private static string ColonizePlanetText_Original = "";
    [Export]
    public Control Disabled_Colonize = null;
    [Export]
    public Control Disabled_NoColonyShip = null;
    [Export]
    public Control Disabled_SelectAPlanet = null;

    [ExportCategory("Runtime")]
    [Export]
    public StarData _Star = null;
    [Export]
    public Array<FleetData> _FriendlyFleets = null;

    public override void _Ready()
    {
        if (ColonizePlanetText_Original.Length == 0)  ColonizePlanetText_Original = ColonizePlanetText.Text;
    }

    public void Refresh(StarData star)
    {
        _Star = star;

        bool hasColonyShip = ActionColonize.GetColonyShip(Game.self, Game.self.HumanPlayer, _Star) != null;
        PlanetData selectedPlanet = Game.self.Input.SelectedPlanet;

        if (selectedPlanet != null && hasColonyShip)
        {
            ColonizeBtn.Visible = true;
            ColonizePlanetBg.Visible = true;
            ColonizePlanetText.Text = ColonizePlanetText_Original.Replace("$planet", selectedPlanet.PlanetName);

            Disabled_Colonize.Visible = false;
            Disabled_NoColonyShip.Visible = false;
            Disabled_SelectAPlanet.Visible = false;
        }
        else if (hasColonyShip)
        {
            ColonizeBtn.Visible = false;
            ColonizePlanetBg.Visible = false;

            Disabled_Colonize.Visible = true;
            Disabled_NoColonyShip.Visible = false;
            Disabled_SelectAPlanet.Visible = true;
        }
        else
        {
            ColonizeBtn.Visible = false;
            ColonizePlanetBg.Visible = false;

            Disabled_Colonize.Visible = true;
            Disabled_NoColonyShip.Visible = true;
            Disabled_SelectAPlanet.Visible = false;
        }
    }

    public void OnColonize()
    {
        ActionColonize.Colonize(Game.self, Game.self.HumanPlayer, _Star, Game.self.Input.SelectedPlanet);
    }
}