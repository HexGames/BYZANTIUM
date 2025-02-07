using Godot;
using Godot.Collections;
using System.Linq;

// Editor
public partial class UIPips : Control
{
    private Array<Control> Pips = new Array<Control>();

    public override void _Ready()
    {
        Pips.Clear();
        int i = 0;
        for (int idx = 0; idx < GetChildCount(); idx++)
        {
            Node node = GetChild(idx);
            if (node is Control)
            {
                Pips.Add(node.GetNode<Control>("On"));
            }
        }
    }

    public void SetPips(int level)
    {
        for (int idx = 0; idx < Pips.Count; idx++)
        {
            Pips[idx].Visible = idx < level;
        }
    }
}
