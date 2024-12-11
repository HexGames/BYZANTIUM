using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIPlanetInfoOtherDistrict : Control
{
    // is beeing duplicated
    private Button Btn;

    private TextureRect IconTexture;
    private UITooltipTrigger IconTooltip;

    private UIText NameText;
    private UIText DescriptionText;

    private Control InfluenceCostBg;
    private UIText InfluenceCostText;

    // Runtime
    public DistrictData _ChangingDistrict = null;
    public DefDistrictWrapper _DistrictDef = null;

    public override void _Ready()
    {
        Btn = GetNode<Button>("MarginContainer/Button");

        IconTexture = GetNode<TextureRect>("MarginContainer/HBoxContainer/Icon/Icon");
        IconTooltip = GetNode<UITooltipTrigger>("MarginContainer/Button/ToolTip");

        NameText = GetNode<UIText>("MarginContainer/HBoxContainer/VBoxContainer/Name");
        DescriptionText = GetNode<UIText>("MarginContainer/HBoxContainer/VBoxContainer/Description");

        InfluenceCostBg = GetNode<Control>("MarginContainer/CostBg");
        InfluenceCostText = GetNode<UIText>("MarginContainer/CostBg/Cost");
    }

    public void RefreshDistrict(DistrictData changingDistrict, DefDistrictWrapper districtDef)
    {
        _ChangingDistrict = changingDistrict;
        _DistrictDef = districtDef;

        IconTexture.Texture = Game.self.Def.AssetLib.GetTexture2D_District(_DistrictDef.Icon + ".png");
        NameText.SetTextWithReplace("$name", _DistrictDef.Name);
        DescriptionText.SetTextWithReplace("$description", _DistrictDef.Economy_PerSession.ToString_Short(28));

        bool needsCosts = _ChangingDistrict.Pop.GetProgress() < 1000;

        if (needsCosts && _DistrictDef.Cost > 0)
        {
            if (_DistrictDef.Cost <= Game.self.HumanPlayer.Stockpiles_PerTurn.Influence)
            {
                Btn.Disabled = false;
                InfluenceCostBg.Visible = true;
                InfluenceCostText.SetTextWithReplace("$v", _DistrictDef.Cost.ToString());
            }
            else
            {
                Btn.Disabled = true;
                InfluenceCostBg.Visible = true;
                InfluenceCostText.SetTextWithReplace("$v", Helper.GetColorPrefix_Bad() + _DistrictDef.Cost.ToString() + Helper.GetColorSufix());
            }
        }
        else
        {
            Btn.Disabled = false;
            InfluenceCostBg.Visible = false;
        }
    }

    public void OnSelect()
    {
        Game.self.GalaxyUI.PlanetInfo.SelectChosenDistrict(this);
    }
}