using Godot;

//[Tool]
public partial class UIItemListItem : Control
{
    // is beeing duplicated
    private Panel BG = null;
    private Label Property = null;
    private Label Value = null;

    [Export]
    public DataBlock _Data = null;

    Game Game;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;
        Game = GetNode<Game>("/root/Main/Game");

        BG = GetNode<Panel>("BG");
        Property = GetNode<Label>("BG/Name");
        Value = GetNode<Label>("BG/Value");
    }

    public void Refresh(DataBlock data)
    {
        Property.Text = data.ToUIName();
        Value.Text = data.ToUIValue();

        string tooltip = data.ToToolTipString_Old();
        BG.TooltipText = tooltip;
    }
}