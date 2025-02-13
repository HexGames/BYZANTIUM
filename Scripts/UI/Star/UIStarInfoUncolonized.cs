using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIStarInfoUncolonized : Control
{
    [ExportCategory("Links")]
    [Export]
    public Control UncolonizedBg = null;
    [Export]
    public Array<UIButton> ColonizePlanet = new Array<UIButton>();
    [Export]
    public Control Disabled_NoColonyShip = null;

    [Export]
    public Control Colonizing = null;
    [Export]
    public UIText ColonizingText;
    [Export]
    public TextureRect ColonizingColor;

    [ExportCategory("Runtime")]
    [Export]
    public StarData _Star = null;
    [Export]
    public Array<FleetData> _FriendlyFleets = null;

    public void Refresh(StarData star)
    {
        _Star = star;

        //FleetData colonyFleet = ActionColonize.GetColonyFleet(Game.self, Game.self.HumanPlayer, _Star);
        //List<PlanetData> habitables = _Star.GetHabitablePlanets();
        //
        //if (colonyFleet != null && colonyFleet.Data.HasSub("ActionColonize"))
        //{
        //    UncolonizedBg.Visible = false;
        //    Colonizing.Visible = true;
        //
        //    ColonizingText.SetTextWithReplace("$name", colonyFleet.Data.GetSubValueS("ActionColonize", "Planet"));
        //}
        //else
        //{
        //    UncolonizedBg.Visible = true;
        //    Colonizing.Visible = false;
        //
        //    for (int idx = 0; idx < ColonizePlanet.Count; idx++)
        //    {
        //        if (idx < habitables.Count)
        //        {
        //            ColonizePlanet[idx].Visible = true;
        //            ColonizePlanet[idx].BtnText.SetTextWithReplace("$name", habitables[idx].PlanetName, "$v", "???");
        //            ColonizePlanet[idx].Disabled = colonyFleet == null;
        //        }
        //        else
        //        {
        //            ColonizePlanet[idx].Visible = false;
        //        }
        //    }
        //    Disabled_NoColonyShip.Visible = colonyFleet == null;
        //}
    }

    //public void OnColonize_1()
    //{
    //    List<PlanetData> habitables = _Star.GetHabitablePlanets();
    //    ActionColonize.Colonize(Game.self, Game.self.HumanPlayer, _Star, habitables[0]);
    //}
    //public void OnColonize_2()
    //{
    //    List<PlanetData> habitables = _Star.GetHabitablePlanets();
    //    ActionColonize.Colonize(Game.self, Game.self.HumanPlayer, _Star, habitables[1]);
    //}
    //public void OnColonize_3()
    //{
    //    List<PlanetData> habitables = _Star.GetHabitablePlanets();
    //    ActionColonize.Colonize(Game.self, Game.self.HumanPlayer, _Star, habitables[2]);
    //}
    //public void OnCancel()
    //{
    //    ActionColonize.Cancel(Game.self, Game.self.HumanPlayer, _Star);
    //}
}