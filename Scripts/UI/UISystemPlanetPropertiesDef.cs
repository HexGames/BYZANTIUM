using Godot;
using Godot.Collections;
using System;

public partial class UISystemPlanetPropertiesDef : Resource
{
    [Export]
    public int Row = 0;
    [Export]
    public Dictionary<int, string> Values = new Dictionary<int, string>();
    [Export]
    public string TooltipDefault = "";
    [Export]
    public Dictionary<string, string> Tooltips = new Dictionary<string, string>();
}