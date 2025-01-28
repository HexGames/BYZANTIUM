using Godot;
using Godot.Collections;
using System.Linq;

// Editor
public partial class UIPips : Control
{
    [Export]
    public Array<Control> Pips = null;

    public void Set(int level)
    {
        for (int idx = 0; idx < Pips.Count; idx++)
        {
            Pips[idx].Visible = idx < level;
        }
    }
}
