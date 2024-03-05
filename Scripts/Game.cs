using Godot;
using System;

public partial class Game : Node
{
    [ExportCategory("Links")]
    [Export]
    public DefLibrary Def;
    [Export]
    public MapCamera Camera;
    [Export]
	public PlayerInput Input;
    [Export]
    public UILibrary UILib;
    [Export]
    public UIGalaxy GalaxyUI;
    [Export]
    public UISystem SystemUI;
    //[Export]
    //public UIActionColony ActionColonyUI;
    [Export]
    public MapNode Map;
    [Export]
    public TurnLoop TurnLoop;

    [ExportCategory("Runtime")]
    [Export]
    public PlayerData HumanPlayer = null;

    private bool FirstFrame = true;

    public override void _Process(double delta)
    {
        if (FirstFrame)
        {
            for (int idx = 0; idx < Map.Data.Players.Count; idx++)
            {
                if (Map.Data.Players[idx].Human)
                {
                    HumanPlayer = Map.Data.Players[idx];
                    Camera.TargetPosition = HumanPlayer.Sectors[0].Systems[0].Star._Node.GFX.GlobalPosition;
                    break;
                }
            }

            FirstFrame = false;
        }
    }
}
