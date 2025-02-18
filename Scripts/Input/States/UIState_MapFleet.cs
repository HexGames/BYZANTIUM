using Godot.Collections;

public class UIState_MapFleet : UIStateBase
{
    public override void Init() { }

    // ---------------------------------------------------------------------------------------- ENTER / EXIT
    public override void Enter() 
    {
        FleetData fleet = UI.FleetSelected;

        Game.self.SelectorsUI3D.FleetSelect(fleet);

        Game.self.UIGalaxy.FleetsSelected.RefreshFleet(fleet);

        for (int idx = 0; idx < fleet.AvailableMoves_PerTurn.Count; idx++)
        {
            fleet.AvailableMoves_PerTurn[idx]._Node.GFX.SetAsMoveTarget();
        }
    }
    public override void Exit() 
    {
        FleetData fleet = UI.FleetSelected;

        Game.self.SelectorsUI3D.FleetDeselect();

        Game.self.UIGalaxy.FleetsSelected.Visible = false;

        for (int idx = 0; idx < fleet.AvailableMoves_PerTurn.Count; idx++)
        {
            fleet.AvailableMoves_PerTurn[idx]._Node.GFX.ClearTarget();
        }
    }

    public override void Hover(StarData star) 
    {
        if (UI.StarHovered != null) UI.ClearHoverToken(UI.StarHovered);
        if (UI.PlanetHovered != null)
        {
            UI.ClearHoverToken(UI.PlanetHovered);
            if (UI.PlanetHovered._Star != star)
            {
                UI.ClearHoverToken(UI.PlanetHovered._Star);
                UI.SetHoverToken(star);
            }
        }
        else
        {
            UI.SetHoverToken(star);
        }
        if (UI.FleetsHovered.Count > 0 && UI.FleetsHovered.Contains(UI.FleetSelected) == false) UI.ClearHoverToken(UI.FleetsHovered);

        if (ActionMove.HasAvailableMove(Game.self, UI.FleetSelected, star))
        {
            star._Node.GFX.SetAsMoveTarget();
        }
        else
        {
            star._Node.GFX.ClearTarget();
        }
    }
    public override void Hover(PlanetData planet) 
    {
        Hover(planet._Star);
    }

    public override void Hover(Array<FleetData> fleets) 
    {
        if (UI.StarHovered != null) UI.ClearHoverToken(UI.StarHovered);
        if (UI.PlanetHovered != null)
        {
            UI.ClearHoverToken(UI.PlanetHovered._Star);
            UI.ClearHoverToken(UI.PlanetHovered);
        }
        if (UI.FleetsHovered.Count > 0 && UI.FleetsHovered.Contains(UI.FleetSelected) == false) UI.ClearHoverToken(UI.FleetsHovered);
        UI.SetHoverToken(fleets);

        Game.self.UIGalaxy.SystemInfo.Visible = false;
        Game.self.UIGalaxy.DistrictsInfo.Visible = false;
        Game.self.UIGalaxy.FleetsSelected.RefreshFleets(fleets);
        Game.self.UIGalaxy.ActionsEconomy.Visible = false;
    }

    public override void Dehover()
    {
        if (UI.StarHovered != null) UI.ClearHoverToken(UI.StarHovered);
        if (UI.PlanetHovered != null)
        {
            UI.ClearHoverToken(UI.PlanetHovered._Star);
            UI.ClearHoverToken(UI.PlanetHovered);
        }
        if (UI.FleetsHovered.Count > 0 && UI.FleetsHovered.Contains(UI.FleetSelected) == false) UI.ClearHoverToken(UI.FleetsHovered);

        FleetData fleet = UI.FleetSelected;

        Game.self.UIGalaxy.SystemInfo.Visible = false;
        Game.self.UIGalaxy.DistrictsInfo.Visible = false;
        Game.self.UIGalaxy.FleetsSelected.RefreshFleet(fleet);
        Game.self.UIGalaxy.ActionsEconomy.Visible = false;
    }

    public override void ZoomIn() { }
    public override void ZoomOut() { }

    public override void Update(double frameTime) { }
}
