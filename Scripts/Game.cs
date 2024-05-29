using Godot;
using System;

public partial class Game : Node
{
    [ExportCategory("Links")]
    [Export]
    public AssetLibrary Assets;
    [Export]
    public DefLibrary Def;
    [Export]
    public MapGenerator MapGen;
    [Export]
    public MapCamera Camera;
    [Export]
	public PlayerInput Input;
    [Export]
    public UILibrary UILib;
    [Export]
    public UITooltipManager Tooltips;
    [Export]
    public UIGalaxy GalaxyUI;
    [Export]
    public UISystem SystemUI;
    [Export]
    public UIWindows WindowsUI;
    //[Export]
    //public UIActionColony ActionColonyUI;
    [Export]
    public MapNode Map;
    [Export]
    public TurnLoop TurnLoop;
    [Export]
    public GameArgs Args;

    [ExportCategory("Runtime")]
    [Export]
    public PlayerData HumanPlayer = null;

    private bool FirstFrame = true;

    public override void _Ready()
    {
        Args.ReadyArgs();
    }
    public override void _Process(double delta)
    {
        if (FirstFrame)
        {
            if (HumanPlayer != null)
            {
                Camera.TargetPosition = HumanPlayer.Sectors[0].Systems[0].Star._Node.GFX.GlobalPosition;
            }

            FirstFrame = false;
        }
    }
}
