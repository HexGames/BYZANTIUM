using Godot;
using Godot.Collections;

public partial class UI3DManager : Control
{
    [Export]
    private Control UI3DStarParent = null;
    [Export]
    private Array<UI3DStar> UI3DStarPool = new Array<UI3DStar>();
    [Export]
    private Control UI3DPlanetParent = null;
    [Export]
    private Array<UI3DPlanet> UI3DPlanetPool = new Array<UI3DPlanet>();

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

        while (UI3DPlanetPool.Count < 25)
        {
            UI3DPlanet newItem = UI3DPlanetPool[0].Duplicate(7) as UI3DPlanet;
            UI3DPlanetPool[0].GetParent().AddChild(newItem);
            UI3DPlanetPool.Add(newItem);
        }

        for (int idx = 0; idx < UI3DPlanetPool.Count; idx++)
        {
            UI3DPlanetPool[idx].Visible = false;
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

    public UI3DPlanet Get_UI3DPlanet()
    {
        UI3DPlanet unusedItem = null;
        for (int idx = 0; idx < UI3DPlanetPool.Count; idx++)
        {
            if (UI3DPlanetPool[idx].GFX == null)
            {
                unusedItem = UI3DPlanetPool[idx];
                break;
            }
        }

        if (unusedItem == null)
        {
            unusedItem = UI3DPlanetPool[0].Duplicate(7) as UI3DPlanet;
            UI3DPlanetPool[0].GetParent().AddChild(unusedItem);
            UI3DPlanetPool.Add(unusedItem);
        }

        return unusedItem;
    }

    public void LODAlpha(float alpha)
    {
        UI3DStarParent.Modulate = new Color(1.0f, 1.0f, 1.0f, alpha);
        UI3DPlanetParent.Modulate = new Color(1.0f, 1.0f, 1.0f, 1.0f - alpha);
    }
}