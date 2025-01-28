using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISystemDistrictsPlanetName : Control
{
    // is beeing duplicated
    public Control ButtonsBg;
    public UIActionButton Colonize;
    public UIActionButton Terraforming;
    public TextureRect TypeIcon;
    public UITooltipTrigger TypeIconToolTip;
    public UIText PlanetName;
    public UIText Pops;
    public UITooltipTrigger PopsToolTip;

    [ExportCategory("Runtime")]
    [Export]
    public PlanetData _Planet = null;

    public override void _Ready()
    {
        ButtonsBg = GetNode<Control>("Button");
        Colonize = GetNode<UIActionButton>("Button/Colonize");
        Terraforming = GetNode<UIActionButton>("Button/Terraforming");
        TypeIcon = GetNode<TextureRect>("Name/HBoxContainer/Icon");
        TypeIconToolTip = GetNode<UITooltipTrigger>("Name/HBoxContainer/Icon/ToolTip");
        PlanetName = GetNode<UIText>("Name/HBoxContainer/Name");
        Pops = GetNode<UIText>("Name/HBoxContainer/Name/Pops");
        PopsToolTip = GetNode<UITooltipTrigger>("Name/HBoxContainer/Name/Pops/ToolTip");
    }

    public void Refresh(PlanetData planet)
    {
        _Planet = planet;

        PlanetName.SetTextWithReplace("$value", _Planet.PlanetName);
        TypeIcon.Texture = Game.self.Def.AssetLib.GetTexture2D_Planet(_Planet.Data.GetSubValueS("Type") + ".png");

        Pops.Visible = false;

        ButtonsBg.Visible = false;
        Colonize.Visible = false;
        Terraforming.Visible = false;
    }
}