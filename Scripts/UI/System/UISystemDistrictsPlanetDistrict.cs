using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISystemDistrictsPlanetDistrict : Control
{
    // is beeing duplicated
    private UIActionButton Build;
    private TextureRect Feature;
    private UITooltipTrigger FeatureToolTip;
    private TextureRect Icon;
    private UITooltipTrigger IconToolTip;
    private UIText Level;
    private UITooltipTrigger LevelToolTip;
    private UIText Pops;
    private UITooltipTrigger PopsToolTip;
    private UIText Production;
    private UITooltipTrigger ProductionToolTip;

    private UISystemDistrictsPlanetDistrictPops PopsDetail = null;

    // runtime
    public DistrictData _District = null;

    public override void _Ready()
    {
        string prefix = "";
        if (HasNode("Bottom"))
        {
            prefix = "Top/";
            PopsDetail = GetNode<UISystemDistrictsPlanetDistrictPops>("Bottom");
        }
        else
        {
            PopsDetail = null;
        }

        Build = GetNode<UIActionButton>(prefix + "Build");
        Feature = GetNode<TextureRect>(prefix + "HBoxContainer/Feature");
        FeatureToolTip = GetNode<UITooltipTrigger>(prefix + "HBoxContainer/Feature/ToolTip");
        Icon = GetNode<TextureRect>(prefix + "HBoxContainer/Icon");
        IconToolTip = GetNode<UITooltipTrigger>(prefix + "HBoxContainer/Icon/ToolTip");
        Level = GetNode<UIText>(prefix + "HBoxContainer/VBoxContainer/Level");
        LevelToolTip = GetNode<UITooltipTrigger>(prefix + "HBoxContainer/VBoxContainer/Level/ToolTip");
        Pops = GetNode<UIText>(prefix + "HBoxContainer/VBoxContainer/Pops");
        PopsToolTip = GetNode<UITooltipTrigger>(prefix + "HBoxContainer/VBoxContainer/Pops/ToolTip");
        Production = GetNode<UIText>(prefix + "HBoxContainer/Production");
        ProductionToolTip = GetNode<UITooltipTrigger>(prefix + "HBoxContainer/Production/ToolTip");
    }

    public void Refresh(DistrictData district)
    {
        _District = district;


    }
}