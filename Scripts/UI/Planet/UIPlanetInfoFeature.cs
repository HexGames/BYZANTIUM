using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIPlanetInfoFeature : Control
{
    // is beeing duplicated
    private Control BGIdle;
    private Control BGSelected;

    private Button IconBtn;
    private Control IconSelected;
    private TextureRect IconTexture;
    private UITooltipTrigger IconTooltip;

    private UIText NameText;
    private UIText DescriptionText;

    private Control ActionsBg;
    private UIButton Investigate;

    // Runtime
    public FeatureData _Feature = null;

    public override void _Ready()
    {
        BGIdle = GetNode<Control>("PanelBg");
        BGSelected = GetNode<Control>("SelectedBG");

        IconBtn = GetNode<Button>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Button");
        IconSelected = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Selected");
        IconTexture = GetNode<TextureRect>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Icon");
        IconTooltip = GetNode<UITooltipTrigger>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Button/ToolTip");

        NameText = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Name");
        DescriptionText = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Description");

        ActionsBg = GetNode<Control>("MarginContainer/VBoxContainer/Actions");
        Investigate = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/Investigate");
    }

    public void Refresh(FeatureData feature)
    {
        _Feature = feature;

        IconTexture.Texture = Game.self.Def.AssetLib.GetTexture2D_District(feature.FeatureDef.Icon + ".png");

        NameText.SetTextWithReplace("$name", feature.FeatureDef.Name);
        DescriptionText.SetTextWithReplace("$description", feature.FeatureDef.Economy_PerSession.ToString_Short());

        CloseActions();
    }

    public void OpenActions()
    {
        BGIdle.Visible = false;
        BGSelected.Visible = true;

        IconBtn.Visible = false;
        IconSelected.Visible = true;

        ActionsBg.Visible = true;

        Investigate.Visible = true;
    }

    public void CloseActions()
    {
        BGIdle.Visible = true;
        BGSelected.Visible = false;

        IconBtn.Visible = true;
        IconSelected.Visible = false;

        ActionsBg.Visible = false;
    }

    public void OnSelect()
    {
        //Game.self.GalaxyUI.PlanetInfo.OnSelectFeature(this);
    }
}