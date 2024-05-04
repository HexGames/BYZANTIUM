using Godot;
using Godot.Collections;

public partial class UIItemList : Control
{
    [ExportCategory("Links")]
    [Export]
    private RichTextLabel TitleLabel = null;
    private string TitleLabel_Original = null;
    [Export]
    public Array<UIItemListItem> Properties = new Array<UIItemListItem>();

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _Data = null;
    //[Export]
    //public DataBlock _Layout = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        TitleLabel_Original = TitleLabel.Text;
    }

    public void Refresh(DataBlock data, string forceTitle = "")
    {
        string name = forceTitle;
        if (name == "") data.ValueToString();
        if (name == "") name = data.Name;

        TitleLabel.Text = TitleLabel_Original.Replace("$name", name);

        Array<DataBlock> subs = data.GetSubs();

        // grow
        while (Properties.Count < subs.Count)
        {
            UIItemListItem newItem = Properties[0].Duplicate(7) as UIItemListItem;
            Properties[0].GetParent().AddChild(newItem);
            Properties.Add(newItem);
        }

        for (int idx = 0; idx < Properties.Count; idx++)
        {
            if (idx < subs.Count && subs[idx].ToUIShow())
            {
                Properties[idx].Refresh(subs[idx]);
                Properties[idx].Visible = true;
            }
            else
            {
                Properties[idx].Visible = false;
            }
        }

        Visible = true;
    }
}