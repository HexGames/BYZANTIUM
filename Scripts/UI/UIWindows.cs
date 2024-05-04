using Godot;
using Godot.Collections;

public partial class UIWindows : Control
{
    [ExportCategory("Links")]
    [Export]
    //public UIBuild BuildWindow = null;
   
    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");
    }

    //public void Build(SectorData sector)
    //{
    //    BuildWindow.Refresh(sector);
    //    BuildWindow.Visible = true;
    //}

    public void Build(PlanetData planet)
    {
        //BuildWindow.Refresh(planet);
        //BuildWindow.Visible = true;
    }

    public void HideAll()
    {
        //BuildWindow.Visible = false;
    }
}