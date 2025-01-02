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

    private Control CostBg;
    private UIText CostText;
    private UIText CostTime;

    // Runtime
    public DistrictData _ChangingDistrict = null;
    public DistrictNew _DistrictNew = null;

    public override void _Ready()
    {
        Btn = GetNode<Button>("MarginContainer/Button");

        IconTexture = GetNode<TextureRect>("MarginContainer/HBoxContainer/Icon/Icon");
        IconTooltip = GetNode<UITooltipTrigger>("MarginContainer/Button/ToolTip");

        NameText = GetNode<UIText>("MarginContainer/HBoxContainer/VBoxContainer/Name");
        DescriptionText = GetNode<UIText>("MarginContainer/HBoxContainer/VBoxContainer/Description");

        CostBg = GetNode<Control>("MarginContainer/CostBg");
        CostText = GetNode<UIText>("MarginContainer/CostBg/Cost");
        CostTime = GetNode<UIText>("MarginContainer/HBoxContainer/Time");
    }

    public void RefreshDistrict(DistrictData changingDistrict, DistrictNew districtNew)
    {
        _ChangingDistrict = changingDistrict;
        _DistrictNew = districtNew;

        IconTexture.Texture = Game.self.Def.AssetLib.GetTexture2D_District(_DistrictNew.DistrictDef.Icon + ".png");
        NameText.SetTextWithReplace("$name", _DistrictNew.DistrictDef.Name);
        DescriptionText.SetTextWithReplace("$description", _DistrictNew.Economy.ToString_Short(28));

        bool fullPop = _ChangingDistrict.Pop.GetProgress() == 1000;

        string cost = "";
        Btn.Disabled = false;
        if (_DistrictNew.Cost_BC > 0)
        {
            if (_DistrictNew.Cost_BC <= Game.self.HumanPlayer.Stockpiles_PerTurn.BC)
            {
                cost = Helper.ResValueToString(_DistrictNew.Cost_BC) + Helper.GetIcon("BC");
            }
            else
            {
                Btn.Disabled = true;
                cost = Helper.GetColorPrefix_Bad() + Helper.ResValueToString(_DistrictNew.Cost_BC) + Helper.GetColorSufix() + Helper.GetIcon("BC");
            }
        }
        if (_DistrictNew.Cost_Inf > 0)
        {
            if (_DistrictNew.Cost_Inf <= Game.self.HumanPlayer.Stockpiles_PerTurn.Influence)
            {
                cost = Helper.ResValueToString(_DistrictNew.Cost_Inf) + Helper.GetIcon("Influence");
            }
            else
            {
                Btn.Disabled = true;
                cost = Helper.GetColorPrefix_Bad() + Helper.ResValueToString(_DistrictNew.Cost_Inf) + Helper.GetColorSufix() + Helper.GetIcon("Influence");
            }
        }

        if (cost != "")
        {
            CostBg.Visible = true;
            CostText.SetTextWithReplace("$cost", cost);
        }

        CostTime.SetTextWithReplace("$t", _DistrictNew.Cost_Time.ToString());
    }

    public void OnSelect()
    {
        Game.self.GalaxyUI.PlanetInfo.SelectChosenDistrict(this);
    }
}