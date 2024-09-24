using Godot;
using Godot.Collections;

//[Tool]
public partial class UISelectedFleets : Control
{
    /*
    // was duplicated in the past...
    [Export]
    public Button Fleet_1;
    [Export]
    public Button Fleet_2;
    [Export]
    public Button Fleet_3;
    [Export]
    public Button Fleet_4;
    [Export]
    public Button Fleet_5;
    [Export]
    public Button MoreFleets;
    [Export]
    public RichTextLabel MoreFleetsText = null;
    private static string MoreFleetsText_Original = "";*/
    // is beeing duplicated
    [Export]
    public Array<UISelectedFleetsItem> Fleets = new Array<UISelectedFleetsItem>();

    [ExportCategory("Runtime")]
    [Export]
    public Array<FleetData> _Data = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");
    }

    public void Refresh(Array<FleetData> selectedFleets)
    {
        _Data = selectedFleets;

        // grow
        while (Fleets.Count < selectedFleets.Count)
        {
            UISelectedFleetsItem newItem = Fleets[0].Duplicate(7) as UISelectedFleetsItem;
            Fleets[0].GetParent().AddChild(newItem);
            Fleets.Add(newItem);
        }

        for (int idx = 0; idx < Fleets.Count; idx++)
        {
            if (idx < selectedFleets.Count)
            {
                Fleets[idx].Refresh(selectedFleets[idx]);
                Fleets[idx].Visible = true;
            }
            else
            {
                Fleets[idx].Visible = false;
            }
        }
    }
}