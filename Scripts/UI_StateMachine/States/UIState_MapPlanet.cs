using Godot.Collections;

public class UIState_MapPlanet : UIStateBase
{
    public override void Init() { }

    // ---------------------------------------------------------------------------------------- ENTER / EXIT
    public override void Enter() 
    {
        PlanetData planet = UI.PlanetSelected;

        Game.self.SelectorsUI3D.PlanetSelect(planet);
        planet._Star._Node.GFX.GFXSelect();
        if (UI.ZoomedIn) planet._Star._Node.GFX.ShowPlanets3DGUI();

        Game.self.UIGalaxy.SystemInfo.Refresh(planet._Star);
        Game.self.UIGalaxy.DistrictsInfo.RefreshPlanet(planet);
        if (planet._Star.System != null && planet._Star.System._Player == Game.self.HumanPlayer) Game.self.UIGalaxy.ActionsEconomy.RefreshPlanet(planet);
        else Game.self.UIGalaxy.ActionsEconomy.RefreshText(planet._Star);
    }
    public override void Exit()
    {
        PlanetData planet = UI.PlanetSelected;

        Game.self.SelectorsUI3D.PlanetDeselect();
        planet._Star._Node.GFX.GFXDeselect();
        if (UI.ZoomedIn) planet._Star._Node.GFX.HidePlanets3DGUI();

        Game.self.UIGalaxy.SystemInfo.Visible = false;
        Game.self.UIGalaxy.DistrictsInfo.Visible = false;
        Game.self.UIGalaxy.ActionsEconomy.Visible = false;
    }

    // ---------------------------------------------------------------------------------------- HOVERS
    public override void Hover(StarData star)
    {
        if (star != UI.PlanetSelected._Star)
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

            Game.self.UIGalaxy.SystemInfo.Refresh(star);
            Game.self.UIGalaxy.DistrictsInfo.RefreshStar(star);
            Game.self.UIGalaxy.FleetsSelected.Visible = false;
            Game.self.UIGalaxy.ActionsEconomy.RefreshText(star);
        }
        else
        {
            UI.ClearHoverToken(UI.PlanetHovered);
        }
        if (UI.FleetsHovered.Count > 0) UI.ClearHoverToken(UI.FleetsHovered);

        //Game.self.UIGalaxy.DEBUGText.Text = "Map Panet" + "\n" + "Hover Star";
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

        //Game.self.UIGalaxy.DEBUGText.Text = "Map Panet" + "\n" + "Hover Planet";
    }
    public override void Hover(Array<FleetData> fleets)
    {
        if (UI.StarHovered != null && UI.StarHovered != UI.PlanetSelected._Star) UI.ClearHoverToken(UI.StarHovered);
        if (UI.PlanetHovered != null)
        {
            if (UI.PlanetHovered._Star != UI.PlanetSelected._Star) UI.ClearHoverToken(UI.PlanetHovered._Star);
            UI.ClearHoverToken(UI.PlanetHovered);
        }
        if (UI.FleetsHovered.Count > 0) UI.ClearHoverToken(UI.FleetsHovered);
        Game.self.SelectorsUI3D.FleetHover(fleets);

        Game.self.UIGalaxy.SystemInfo.Visible = false;
        Game.self.UIGalaxy.DistrictsInfo.Visible = false;
        Game.self.UIGalaxy.FleetsSelected.RefreshFleets(fleets);
        Game.self.UIGalaxy.ActionsEconomy.Visible = false;

        //Game.self.UIGalaxy.DEBUGText.Text = "Map Panet" + "\n" + "Hover Fleets";
    }
    public override void Dehover()
    {
        if (UI.StarHovered != null && UI.StarHovered != UI.PlanetSelected._Star) UI.ClearHoverToken(UI.StarHovered);
        if (UI.PlanetHovered != null)
        {
            if (UI.PlanetHovered._Star != UI.PlanetSelected._Star) UI.ClearHoverToken(UI.PlanetHovered._Star);
            UI.ClearHoverToken(UI.PlanetHovered);
        }
        if (UI.FleetsHovered.Count > 0) UI.ClearHoverToken(UI.FleetsHovered);

        Game.self.UIGalaxy.SystemInfo.Refresh(UI.PlanetSelected._Star);
        Game.self.UIGalaxy.DistrictsInfo.RefreshPlanet(UI.PlanetSelected);
        Game.self.UIGalaxy.FleetsSelected.Visible = false;
        if (UI.PlanetSelected._Star.System != null && UI.PlanetSelected._Star.System._Player == Game.self.HumanPlayer) Game.self.UIGalaxy.ActionsEconomy.RefreshPlanet(UI.PlanetSelected);
        else Game.self.UIGalaxy.ActionsEconomy.RefreshText(UI.PlanetSelected._Star);

        //Game.self.UIGalaxy.DEBUGText.Text = "Map Panet" + "\n" + "Dehover";
    }

    // ---------------------------------------------------------------------------------------- ZOOMS
    public override void ZoomIn()
    {
        UI.PlanetSelected._Star._Node.GFX.ShowPlanets3DGUI();

        if (UI.StarHovered != null) UI.StarHovered._Node.GFX.ShowPlanets3DGUI();
        if (UI.PlanetHovered != null) UI.PlanetHovered._Star._Node.GFX.ShowPlanets3DGUI();
    }

    public override void ZoomOut() 
    {
        UI.PlanetSelected._Star._Node.GFX.HidePlanets3DGUI();
        if (UI.PlanetHovered != null)
        {
            UI.Hover(UI.PlanetHovered._Star);
        }
        if (UI.StarHovered != null) UI.StarHovered._Node.GFX.HidePlanets3DGUI();
    }

    // ---------------------------------------------------------------------------------------- UPDATE
    public override void Update(double frameTime) { }
}
