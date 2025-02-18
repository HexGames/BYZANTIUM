using Godot;
using Godot.Collections;

public partial class UIGeneralItem : Control
{
    // is beeing duplicated
    private Array<Control> DepthDots = new Array<Control>();
    private RichTextLabel DataName = null;
    private static string DataName_Original = "";
    private RichTextLabel DataValue = null;
    private static string DataValue_Original = "";
    private Button Add = null;
    private Button Remove = null;
    private Button Expand = null;
    private Button Collapse = null;
    private Button Close = null;

    private Control SubItemsBg = null;
    private Control SubItems = null;

    private UITooltipTrigger Tooltip = null;

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _Data;

    [Export]
    public UIGeneralItem Parent = null;

    [Export]
    public Array<UIGeneralItem> SubItemsArray = new Array<UIGeneralItem>();

    [Export]
    public bool Expanded = false;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        DepthDots.Clear();
        for (int i = 1; i <= 7; i++)
            DepthDots.Add(GetNode<Control>("ValueContainer/Gap_" + i.ToString()));

        DataName = GetNode<RichTextLabel>("ValueContainer/Value/Margins/HBox/Name");
        if (DataName_Original.Length == 0) DataName_Original = DataName.Text;

        DataValue = GetNode<RichTextLabel>("ValueContainer/Value/Margins/HBox/Value");
        if (DataValue_Original.Length == 0) DataValue_Original = DataValue.Text;

        Add = GetNode<Button>("ValueContainer/Value/Margins/HBox/Add");
        Remove = GetNode<Button>("ValueContainer/Value/Margins/HBox/Remove");
        Expand = GetNode<Button>("ValueContainer/Value/Margins/HBox/Restore");
        Collapse = GetNode<Button>("ValueContainer/Value/Margins/HBox/Collapse");
        Close = GetNode<Button>("ValueContainer/Value/Margins/HBox/Close");

        SubItemsBg = GetNode<Control>("Depth");
        SubItems = GetNode<Control>("Depth/SubItems");

        Tooltip = GetNode<UITooltipTrigger>("ValueContainer/Value/Tooltip");
    }

    public void Refresh(DataBlock data, UIGeneralItem parent = null, int depth = 0)
    {
        _Data = data;

        Parent = parent;

        for (int idx = 0; idx < DepthDots.Count; idx++)
        {
            DepthDots[idx].Visible = idx < depth;
        }

        DataName.Text = DataName_Original.Replace("$name", data.Name);
        DataValue.Text = DataValue_Original.Replace("$value", data.ValueToString());

        Close.Visible = false;
        Add.Visible = false;
        Remove.Visible = false;
        Collapse.Visible = false;
        Expand.Visible = false;

        if (Parent == null)
        {
            Close.Visible = true;
            Expanded = true;
        }

        for (int idx = 0; idx < _Data.Subs.Count; idx++)
        {
            UIGeneralItem newItem = Game.UIGalaxy.General.Original.Duplicate(7) as UIGeneralItem;
            SubItems.AddChild(newItem);
            SubItemsArray.Add(newItem);

            newItem.Refresh(_Data.Subs[idx], this, depth + 1);
            newItem.Visible = depth <= 1;
        }

        if (Expanded)
        {
            Collapse.Visible = true;
            SubItemsBg.Visible = true;
        }
        else
        {
            Expand.Visible = SubItemsArray.Count > 0;
            SubItemsBg.Visible = false;
        }
    }

    public void OnClose()
    {
        //Game.Input.DeselectFleet(_Fleet);
    }
}