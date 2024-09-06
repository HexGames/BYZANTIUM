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
    public GFXPaths Paths;
    [Export]
    public GFXIncomings Incomings;
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
    [Export]
    public UI3DSelectors SelectorsUI3D;
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

    static Game _self;
    public static Game self
    {
        get
        {
            if (_self != null)
            {
                return _self;
            }
            else
            {
                _self = ((SceneTree)Engine.GetMainLoop()).Root.GetNode<Game>("/root/Main/Game");
                /*if (_self == null)
                {
                    _self = ((SceneTree)Engine.GetMainLoop()).Root.GetNode<Game>("root/Main/Game");
                }
                if (_self == null)
                {
                    _self = EditorInterface.Singleton.GetEditedSceneRoot().GetNode<Game>("/root/Main/Game");
                }
                if (_self == null)
                {
                    _self = EditorInterface.Singleton.GetEditedSceneRoot().GetNode<Game>("root/Main/Game");
                }
                if (_self == null)
                {
                    _self = ((SceneTree)Engine.GetMainLoop()).Root.GetNode<Game>("Main/Game");
                }
                if (_self == null)
                {
                    _self = EditorInterface.Singleton.GetEditedSceneRoot().GetNode<Game>("Main/Game");
                }*/
                return _self;
            }
        }
        set
        {
            _self = value;
        }
    }

    public override void _Ready()
    {
        Args.ReadyArgs();

        // Human player
        for (int idx = 0; idx < Map.Data.Players.Count; idx++)
        {
            if (Map.Data.Players[idx].Human)
            {
                HumanPlayer = Map.Data.Players[idx];
                break;
            }
        }
    }

    public override void _Process(double delta)
    {
        if (FirstFrame)
        {
            if (HumanPlayer != null)
            {
                Camera.TargetPosition = HumanPlayer.Systems[0].Star._Node.GFX.GlobalPosition;
            }

            FirstFrame = false;
        }
    }
}
