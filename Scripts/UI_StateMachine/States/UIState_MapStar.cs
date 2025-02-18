using Godot.Collections;

public class UIState_MapStar : UIStateBase
{
    public override void Init() { }

    // ---------------------------------------------------------------------------------------- ENTER / EXIT
    public override void Enter() 
    {
        StarData star = UI.StarSelected;

        star._Node.GFX.GFXSelect();
        if (UI.ZoomedIn) star._Node.GFX.ShowPlanets3DGUI();

        Game.self.UIGalaxy.SystemInfo.Refresh(star);
        Game.self.UIGalaxy.DistrictsInfo.RefreshStar(star);
        if (star.System != null && star.System._Player == Game.self.HumanPlayer) Game.self.UIGalaxy.ActionsEconomy.RefreshSystem(star.System);
        else Game.self.UIGalaxy.ActionsEconomy.RefreshText(star);
    }
    public override void Exit()
    {
        StarData star = UI.StarSelected;

        star._Node.GFX.GFXDeselect();
        if (UI.ZoomedIn) star._Node.GFX.HidePlanets3DGUI();

        Game.self.UIGalaxy.SystemInfo.Visible = false;
        Game.self.UIGalaxy.DistrictsInfo.Visible = false;
        Game.self.UIGalaxy.ActionsEconomy.Visible = false;
    }

    // ---------------------------------------------------------------------------------------- HOVERS
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
        if (UI.FleetsHovered.Count > 0) UI.ClearHoverToken(UI.FleetsHovered);

        Game.self.UIGalaxy.SystemInfo.Refresh(star);
        Game.self.UIGalaxy.DistrictsInfo.RefreshStar(star);
        Game.self.UIGalaxy.FleetsSelected.Visible = false;
        Game.self.UIGalaxy.ActionsEconomy.RefreshText(star);
    }

    public override void Hover(PlanetData planet) 
    {
        if (UI.StarHovered != null && UI.StarHovered != planet._Star)
        {
            UI.ClearHoverToken(UI.StarHovered);
            UI.SetHoverToken(planet._Star);
        }
        else if (UI.PlanetHovered != null && UI.PlanetHovered._Star != planet._Star)
        {
            UI.ClearHoverToken(UI.PlanetHovered._Star);
            UI.SetHoverToken(planet._Star);
        }
        else
        {
            UI.SetHoverToken(planet._Star);
        }
        UI.ClearHoverToken(UI.PlanetHovered);
        UI.SetHoverToken(planet);
        if (UI.FleetsHovered.Count > 0) UI.ClearHoverToken(UI.FleetsHovered);

        Game.self.UIGalaxy.SystemInfo.Refresh(planet._Star);
        Game.self.UIGalaxy.DistrictsInfo.RefreshPlanet(planet);
        Game.self.UIGalaxy.FleetsSelected.Visible = false;
        Game.self.UIGalaxy.ActionsEconomy.RefreshText(planet._Star);
    }
    public override void Hover(Array<FleetData> fleets)
    {
        if (UI.StarHovered != null) UI.ClearHoverToken(UI.StarHovered);
        if (UI.PlanetHovered != null)
        {
            UI.ClearHoverToken(UI.PlanetHovered._Star);
            UI.ClearHoverToken(UI.PlanetHovered);
        }
        if (UI.FleetsHovered.Count > 0) UI.ClearHoverToken(UI.FleetsHovered);
        Game.self.SelectorsUI3D.FleetHover(fleets);

        Game.self.UIGalaxy.SystemInfo.Visible = false;
        Game.self.UIGalaxy.DistrictsInfo.Visible = false;
        if (fleets.Contains(UI.FleetSelected)) Game.self.UIGalaxy.FleetsSelected.RefreshFleet(UI.FleetSelected);
        else Game.self.UIGalaxy.FleetsSelected.RefreshFleets(fleets);
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
        if (UI.FleetsHovered.Count > 0) UI.ClearHoverToken(UI.FleetsHovered);

        Game.self.UIGalaxy.SystemInfo.Refresh(UI.StarSelected);
        Game.self.UIGalaxy.DistrictsInfo.RefreshStar(UI.StarSelected);
        Game.self.UIGalaxy.FleetsSelected.Visible = false;
        if (UI.StarSelected.System != null && UI.StarSelected.System._Player == Game.self.HumanPlayer) Game.self.UIGalaxy.ActionsEconomy.RefreshSystem(UI.StarSelected.System);
        else Game.self.UIGalaxy.ActionsEconomy.RefreshText(UI.StarSelected);
    }

    // ---------------------------------------------------------------------------------------- ZOOMS
    public override void ZoomIn() 
    {
        UI.StarSelected._Node.GFX.ShowPlanets3DGUI();

        if (UI.StarHovered != null) UI.StarHovered._Node.GFX.ShowPlanets3DGUI();
        if (UI.PlanetHovered != null) UI.PlanetHovered._Star._Node.GFX.ShowPlanets3DGUI();
    }
    public override void ZoomOut() 
    {
        UI.StarSelected._Node.GFX.HidePlanets3DGUI();
        if (UI.PlanetHovered != null)
        {
            UI.Hover(UI.PlanetHovered._Star);
        }
        if (UI.StarHovered != null) UI.StarHovered._Node.GFX.HidePlanets3DGUI();
    }

    // ---------------------------------------------------------------------------------------- UPDATE
    public override void Update(double frameTime) { }
}
