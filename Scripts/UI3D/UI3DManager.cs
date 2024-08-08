using Godot;
using Godot.Collections;

public partial class UI3DManager : Control
{
    [Export]
    private Array<UI3DStar> UI3DStarPool = new Array<UI3DStar>();

    public override void _Ready()
    {
        while (UI3DStarPool.Count < 25)
        {
            UI3DStar newItem = UI3DStarPool[0].Duplicate(7) as UI3DStar;
            UI3DStarPool[0].GetParent().AddChild(newItem);
            UI3DStarPool.Add(newItem);
        }

        for (int idx = 0; idx < UI3DStarPool.Count; idx++)
        {
            UI3DStarPool[idx].Visible = false;
        }
    }

    public UI3DStar Get_UI3DStar()
    {
        UI3DStar unusedItem = null;
        for (int idx = 0; idx < UI3DStarPool.Count; idx++)
        {
            if (UI3DStarPool[idx].GFX == null)
            {
                unusedItem = UI3DStarPool[idx];
                break;
            }
        }

        if (unusedItem == null)
        {
            unusedItem = UI3DStarPool[0].Duplicate(7) as UI3DStar;
            UI3DStarPool[0].GetParent().AddChild(unusedItem);
            UI3DStarPool.Add(unusedItem);
        }

        return unusedItem;
    }
}