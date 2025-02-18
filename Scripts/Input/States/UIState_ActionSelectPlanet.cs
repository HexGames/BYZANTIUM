using Godot.Collections;

public class UIState_ActionSelectPlanet : UIStateBase
{
    public override void Init() { }

    // ---------------------------------------------------------------------------------------- ENTER / EXIT
    public override void Enter()
    {
        Game.self.UIGalaxy.DistrictsInfo.RefreshStar(UI.StarSelected);
        Game.self.UIGalaxy.DistrictsInfo.RefreshButtons(UI.PossibleActions);
        Game.self.UIGalaxy.ActionsEconomy.Refresh_Colonize();

        Game.self.Camera.SetZoomAndScroll(false);
    }
    public override void Exit()
    {
        Game.self.UIGalaxy.DistrictsInfo.Visible = false;
        Game.self.UIGalaxy.ActionsEconomy.Visible = false;

        Game.self.Camera.SetZoomAndScroll(true);
    }

    public override void Hover(StarData star) { }
    public override void Hover(PlanetData planet) { }
    public override void Hover(Array<FleetData> fleets) { }
    public override void Dehover() { }

    public override void ZoomIn() { }
    public override void ZoomOut() { }

    public override void Update(double frameTime) { }
}
