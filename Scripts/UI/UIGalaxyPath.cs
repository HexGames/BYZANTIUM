using Godot;
using Godot.Collections;

public partial class UIGalaxyPath : Control
{
    // is beeing duplicated
    PanelContainer PathBg = null;
    RichTextLabel PathValue = null;
    private static string PathValue_Original = "";

    [ExportCategory("Runtime")]
    [Export]
    public GFXPathsItem _PathGFX = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");
        PathBg = GetNode<PanelContainer>("PanelContainer");
        PathValue = GetNode<RichTextLabel>("PanelContainer/Fleets");
        if (PathValue_Original.Length == 0) PathValue_Original = PathValue.Text;
    }

    public void Refresh()
    {
        PathValue.Text = PathValue_Original.Replace("$v", _PathGFX._Fleets.Count.ToString());
    }

    public override void _Process(double delta)
    {
        if (_PathGFX != null)
        {
            Vector2 pos2D = Game.Camera.UnprojectPosition(_PathGFX.Position + new Vector3(0.0f, 0.0f, 0.0f));
            Position = pos2D;
        }
    }
}