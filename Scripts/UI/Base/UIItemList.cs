using Godot;
using Godot.Collections;
using System.ComponentModel;

//[Tool]
public partial class UIItemList : Control
{
    [ExportCategory("Links")]
    [Export]
    public Label Title = null;
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
        if (Engine.IsEditorHint()) return;
        Game = GetNode<Game>("/root/Main/Game");
    }

    public void Refresh(DataBlock data)
    {
        Title.Text = data.ValueToString();
        if (Title.Text == "") Title.Text = data.Name;

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
            if (idx < subs.Count)
            {
                Properties[idx].Refresh(subs[idx]);
                Properties[idx].Visible = true;
            }
            else
            {
                Properties[idx].Visible = false;
            }
        }
    }
}