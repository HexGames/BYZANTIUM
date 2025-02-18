using Godot.Collections;

public class UIState_ActionSelectChoice : UIStateBase
{
    public override void Init() { }

    public override void Enter() 
    {
        if (UI.PlanetSelected != null)
        {
            Game.self.UIGalaxy.ActionsChoice.Refresh(UI.PossibleActions_forPlanet);
        }
        else if (UI.StarSelected != null)
        {
            Game.self.UIGalaxy.ActionsChoice.Refresh(UI.PossibleActions);
        }
        else
        {
            Game.self.UIGalaxy.ActionsChoice.Visible = false;
        }

        Game.self.Camera.SetZoomAndScroll(false);
    }
    public override void Exit()
    {
        Game.self.UIGalaxy.ActionsChoice.Visible = false;

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
