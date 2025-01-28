using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISystemDistrictsPlanetDistrictPops : Control
{
    // is beeing duplicated
    public Container PopsParent;
    public Array<UISystemDistrictsPlanetDistrictPopsItem> Pops = new Array<UISystemDistrictsPlanetDistrictPopsItem>();

    [ExportCategory("Runtime")]
    [Export]
    public StarData _Star = null;
    [Export]
    public SystemData _System = null;
    public override void _Ready()
    {
        PopsParent = GetNode<Container>("HBoxContainer");
        Pops.Clear();
        for (int idx = 0; idx < PopsParent.GetChildCount(); idx++)
        {
            Node node = PopsParent.GetChild(idx);
            if (node is UISystemDistrictsPlanetDistrictPopsItem)
            {
                Pops.Add(node as UISystemDistrictsPlanetDistrictPopsItem);
            }
        }
    }

    public void Refresh(StarData star)
    {
        _Star = star;
        _System = _Star.System;
    }
}