using Godot;
using Godot.Collections;
using System.ComponentModel;

public partial class UIGeneral : Control
{
    [Export]
    public UIGeneralItem Original = null;
    [Export]
    public Array<UIGeneralItem> Items = new Array<UIGeneralItem>();

    public void Refresh(Array<DataBlock> selectedItems)
    {
        // grow
        while (Items.Count < selectedItems.Count)
        {
            UIGeneralItem newItem = Original.Duplicate(7) as UIGeneralItem;
            Original.GetParent().AddChild(newItem);
            Items.Add(newItem);
        }

        for (int idx = 0; idx < Items.Count; idx++)
        {
            if (idx < selectedItems.Count)
            {
                Items[idx].Refresh(selectedItems[idx]);
                Items[idx].Visible = true;
            }
            else
            {
                Items[idx].Visible = false;
            }
        }
    }
}