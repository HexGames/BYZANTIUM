using Godot.Collections;

public class UIStateBase
{
    protected UIStateMachine UI;

    protected UIStateBase()
    {
        UI = Game.self.UI;
    }

    public virtual void Init() { }

    public virtual void Enter() { }
    public virtual void Exit() { }

    public virtual void Hover(StarData star) { }
    public virtual void Hover(PlanetData planet) { }
    public virtual void Hover(Array<FleetData> fleets) { }
    public virtual void Dehover() { }

    public virtual void ZoomIn() { }
    public virtual void ZoomOut() { }

    public virtual void Update(double frameTime) { }
}
