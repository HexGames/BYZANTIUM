using Godot.Collections;

public class UIState_ActionSelectDistrict : UIStateBase
{
    public override void Init() { }

    public override void Enter() { }
    public override void Exit() { }

    public override void Hover(StarData star) { }
    public override void Hover(PlanetData planet) { }
    public override void Hover(Array<FleetData> fleets) { }
    public override void Dehover() { }

    public override void ZoomIn() { }
    public override void ZoomOut() { }

    public override void Update(double frameTime) { }
}
