using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public partial class UIPlanetInfoDistrict : Control
{
    // is beeing duplicated
    private Control BGIdle;
    private Control BGSelected;

    private Button IconBtn;
    private Control IconSelected;
    private TextureRect IconTexture;
    private UITooltipTrigger IconTooltip;

    private Control PopBg;
    private Control PopSelected;
    private UIText PopText_TEMP;
    private UITooltipTrigger PopTooltip;

    private Control NoPopBg;
    //private UIText NoPopText;
    private UITooltipTrigger NoPopTooltip;

    private Control ControlUncontroled;
    private Control ControlPrivate;
    private Control ControlStateOwned;

    private UIText Production;

    private Control Construction;
    private List<UIPanel> ConstructionPipsOn = new List<UIPanel>();
    private List<UIPanel> ConstructionPipsOff = new List<UIPanel>();
    private UIPanel ConstructionInProgress;
    private UIText ConstructionInProgressText;

    private Control Investment;
    private List<UIPanel> InvestmentPipsOn = new List<UIPanel>();
    private List<UIPanel> InvestmentPipsOff = new List<UIPanel>();

    private Control PopProgress;
    private UIText PopProgressTurns;
    private ProgressBar PopsProgressCurrent;
    private ProgressBar PopsProgressNextTurn;


    private Control ActionsBg;
    private UIButton RegainControlBtn;
    private UIButton ImproveBtn;
    private UIButton InvestBtn;
    private UIButton IncreaseInvestmentBtn;
    private UIButton DecreaseInvestmentBtn;
    private UIButton StopInvestmentBtn;
    private UIButton PrivatizeBtn;
    private UIButton NationalizeBuyBtn;
    private UIButton NationalizeForcedBtn;
    private UIButton ChangeDistrictBtn;
    private UIButton PlanDistrictBtn;

    // Runtime
    public DistrictData _District = null;

    public override void _Ready()
    {
        BGIdle = GetNode<Control>("PanelBg");
        BGSelected = GetNode<Control>("SelectedBG");

        IconBtn = GetNode<Button>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Button");
        IconSelected = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Selected");
        IconTexture = GetNode<TextureRect>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Icon");
        IconTooltip = GetNode<UITooltipTrigger>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Button/ToolTip");

        PopBg = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/Icon/PopBg");
        PopSelected = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/Icon/PopSelected");
        PopText_TEMP = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/Icon/PopBg/IconText");
        PopTooltip = GetNode<UITooltipTrigger>("MarginContainer/VBoxContainer/HBoxContainer/Icon/PopBg/ToolTip");

        NoPopBg = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/Icon/NoPopBg");
        //NoPopText = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/Icon/NoPopBg/IconText");
        NoPopTooltip = GetNode<UITooltipTrigger>("MarginContainer/VBoxContainer/HBoxContainer/Icon/NoPopBg/ToolTip");

        ControlUncontroled = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Effects/Control/Uncontroled");
        ControlPrivate = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Effects/Control/PrivateBusiness");
        ControlStateOwned = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Effects/Control/StateOwned");

        Production = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Effects/Production");

        Construction = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction");
        ConstructionPipsOn.Clear();
        ConstructionPipsOn.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Pip_1"));
        ConstructionPipsOn.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Pip_2"));
        ConstructionPipsOn.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Pip_3"));
        
        ConstructionPipsOff.Clear();
        ConstructionPipsOff.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Unused_1"));
        ConstructionPipsOff.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Unused_2"));
        ConstructionPipsOff.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Unused_3"));
        
        ConstructionInProgress = GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Delay");
        ConstructionInProgressText = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Delay/Delay");

        Investment = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Invrest");
        InvestmentPipsOn.Clear();
        InvestmentPipsOn.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Invrest/Pip_1"));
        InvestmentPipsOn.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Invrest/Pip_2"));
        InvestmentPipsOn.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Invrest/Pip_3"));

        InvestmentPipsOff.Clear();
        InvestmentPipsOff.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Invrest/Unused_1"));
        InvestmentPipsOff.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Invrest/Unused_2"));
        InvestmentPipsOff.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Invrest/Unused_3"));

        PopProgress = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/PopProgress");
        PopProgressTurns = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/PopProgress/Panel/Turns");
        PopsProgressCurrent = GetNode<ProgressBar>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/PopProgress/Panel/Progress");
        PopsProgressNextTurn = GetNode<ProgressBar>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/PopProgress/Panel/NextTurn");

        ActionsBg = GetNode<Control>("MarginContainer/VBoxContainer/Actions");
        RegainControlBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/RegainControl");
        ImproveBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/Improve");
        InvestBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/Invest");
        IncreaseInvestmentBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/IncreaseInvestment");
        DecreaseInvestmentBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/DecreaseInvestment");
        StopInvestmentBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/StopInvestment");
        PrivatizeBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/Privatize");
        NationalizeBuyBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/NationalizeBuy");
        NationalizeForcedBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/NationalizeForced");
        ChangeDistrictBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/ChangeDistrict");
        PlanDistrictBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/PlanDistrict");
    }

    public void RefreshDistrict(DistrictData district)
    {
        _District = district;

        PopData pop = _District.GetPop();
        if (pop != null)
        {
            IconTexture.Texture = Game.self.Def.AssetLib.GetTexture2D_District(_District.DistrictDef.Icon + ".png");

            int growthProgress = pop.Data.GetSubValueI("GrowthProgress");
            if (growthProgress == 1000)
            {
                PopBg.Visible = true;
                PopText_TEMP.SetTextWithReplace("$$", "H");
                NoPopBg.Visible = false;

                if (_District.IsStateOwned())
                {
                    ControlStateOwned.Visible = true;
                    ControlPrivate.Visible = false;
                    ControlUncontroled.Visible = false;
                }
                else if (_District.IsPrivate())
                {
                    ControlStateOwned.Visible = false;
                    ControlPrivate.Visible = true;
                    ControlUncontroled.Visible = false;
                }
                else
                {
                    ControlStateOwned.Visible = false;
                    ControlPrivate.Visible = false;
                    ControlUncontroled.Visible = true;
                }

                PopProgress.Visible = false;

                Production.Visible = true;
                Production.SetTextWithReplace("$value", _District.Economy_PerTurn.ToString_Short(28));

                if (_District._Data.HasSub("Factory"))
                {
                    Construction.Visible = true;
                    int value = _District._Data.GetSub("Factory").ValueI;
                    int cooldown = _District._Data.GetSub("Factory_Cooldown").ValueI;

                    if (cooldown == 0) ConstructionInProgress.Visible = false;
                    for (int idx = 0; idx < ConstructionPipsOn.Count; idx++)
                    {
                        if (idx < value)
                        {
                            ConstructionPipsOn[idx].Visible = true;
                            ConstructionPipsOff[idx].Visible = false;
                        }
                        else if (idx == value && cooldown > 0)
                        {
                            ConstructionPipsOn[idx].Visible = false;
                            ConstructionPipsOff[idx].Visible = false;
                            ConstructionInProgress.Visible = true;
                            ConstructionInProgressText.SetTextWithReplace("$v", cooldown.ToString());
                        }
                        else
                        {
                            ConstructionPipsOn[idx].Visible = false;
                            ConstructionPipsOff[idx].Visible = true;
                        }
                    }
                }
                else
                {
                    Construction.Visible = false;
                }

                if (_District._Data.HasSub("Investment"))
                {
                    Investment.Visible = true;
                    int value = _District._Data.GetSub("Investment").ValueI;

                    for (int idx = 0; idx < InvestmentPipsOn.Count; idx++)
                    {
                        if (idx < value)
                        {
                            InvestmentPipsOn[idx].Visible = true;
                            InvestmentPipsOff[idx].Visible = false;
                        }
                        else
                        {
                            InvestmentPipsOn[idx].Visible = false;
                            InvestmentPipsOff[idx].Visible = true;
                        }
                    }
                }
                else
                {
                    Investment.Visible = false;
                }
            }
            else
            {
                PopBg.Visible = false;
                NoPopBg.Visible = true;

                if (_District.IsStateOwned())
                {
                    ControlStateOwned.Visible = true;
                    ControlPrivate.Visible = false;
                    ControlUncontroled.Visible = false;
                }
                else if (_District.IsPrivate())
                {
                    ControlStateOwned.Visible = false;
                    ControlPrivate.Visible = true;
                    ControlUncontroled.Visible = false;
                }
                else
                {
                    ControlStateOwned.Visible = false;
                    ControlPrivate.Visible = false;
                    ControlUncontroled.Visible = true;
                }

                PopProgress.Visible = true;
                PopsProgressCurrent.MaxValue = 1000;
                PopsProgressCurrent.Value = growthProgress;
                if (_District._Colony._System.Data.GetSubValueS("ActionGrowth/FocusColony") == _District._Colony.ColonyName)
                {
                    PopsProgressNextTurn.MaxValue = _District._Colony._System.Pops_PerTurn.GrowthProgressMax;
                    PopsProgressNextTurn.Value = _District._Colony._System.Pops_PerTurn.GrowthProgressNextTurn;
                    PopProgressTurns.SetTextWithReplace("$t", _District._Colony._System.Pops_PerTurn.GrowthTurns.ToString());
                }
                else
                {
                    PopsProgressNextTurn.MaxValue = 1000;
                    PopsProgressNextTurn.Value = 0;
                    PopProgressTurns.SetTextWithReplace("$t", "oo");
                }

                Production.Visible = false;
                Construction.Visible = false;
                Investment.Visible = false;
            }
        }
        else if (_District._Planet.IsHabitable())
        {
            IconTexture.Texture = Game.self.Def.AssetLib.GetTexture2D_District("Empty.png");

            PopBg.Visible = false;
            NoPopBg.Visible = true;

            ControlStateOwned.Visible = false;
            ControlPrivate.Visible = false;
            ControlUncontroled.Visible = false;

            Production.Visible = true;
            Production.SetTextWithReplace("$value", "New " + Helper.GetIcon("Pops", 28) + " in " + "5" + Helper.GetIcon("Turn", 28));

            Construction.Visible = false;
            Investment.Visible = false;
        }
        else
        {
            PopBg.Visible = false;
            NoPopBg.Visible = false;
        }

        if (_District.DistrictDef != null)
        {
            IconTooltip.Title = IconTooltip.Title_Original.Replace("$name", _District.DistrictDef.Name);
        }
        else
        {
            GD.Print("No DistrictDef for " + _District._Data.ValueS);
        }

        CloseActions();
    }

    public void OpenActions(bool district)
    {
        BGIdle.Visible = false;
        BGSelected.Visible = true;

        IconBtn.Visible = !district;
        IconSelected.Visible = district;
        PopSelected.Visible = !district;

        ActionsBg.Visible = true;
        if (district)
        {
            var stockpiles = _District._Colony._System._Player.Stockpiles_PerTurn;
            string control = _District.DistrictDef._Data.GetSubValueS("Control/Type");
            int changeCooldown = _District._Data.GetSubValueI("Change_Cooldown");
            bool canInvest = _District.DistrictDef.CanInvest();
            int investLevel = _District._Data.GetSubValueI("Investment");
            int investLessCost = 1;
            int investMoreCost = 1;
            bool canPrivatize = _District.DistrictDef.CanPrivatize();
            bool canNationalize = _District.DistrictDef.CanNationalize();

            ActionsBg.Visible = true;
            RegainControlBtn.Visible = false;
            ImproveBtn.Visible = true;
            if (canInvest)
            {
                if (investLevel == 0)
                {
                    InvestBtn.Visible = true;
                    IncreaseInvestmentBtn.Visible = false;
                    InvestBtn.BtnText.SetTextWithReplace("$v", investMoreCost.ToString());
                }
                else if (investLevel < 3)
                {
                    InvestBtn.Visible = false;
                    IncreaseInvestmentBtn.Visible = true;
                    IncreaseInvestmentBtn.BtnText.SetTextWithReplace("$v", investMoreCost.ToString());
                }
                else
                {
                    InvestBtn.Visible = false;
                    IncreaseInvestmentBtn.Visible = false;
                }

                if (investLevel > 1)
                {
                    DecreaseInvestmentBtn.Visible = true;
                    StopInvestmentBtn.Visible = false;
                    DecreaseInvestmentBtn.BtnText.SetTextWithReplace("$v", investLessCost.ToString());
                }
                else if (investLevel == 1)
                {
                    DecreaseInvestmentBtn.Visible = false;
                    StopInvestmentBtn.Visible = true;
                    DecreaseInvestmentBtn.BtnText.SetTextWithReplace("$v", investLessCost.ToString());
                }
                else
                {
                    DecreaseInvestmentBtn.Visible = false;
                    StopInvestmentBtn.Visible = false;
                }
            }
            else
            {
                InvestBtn.Visible = false;
                IncreaseInvestmentBtn.Visible = false;
                DecreaseInvestmentBtn.Visible = false;
                StopInvestmentBtn.Visible = false;
            }

            if (_District.DistrictDef.Type == "District")
            {
                // privatize
                if (canPrivatize)
                {
                    PrivatizeBtn.Visible = true;
                    PrivatizeBtn.BtnText.SetTextWithReplace("$v", "150");

                    if (changeCooldown > 0)
                    {
                        PrivatizeBtn.Disabled = true;
                        PrivatizeBtn.BtnText.SelfModulate = new Color(0.75f, 0.75f, 0.75f, 1.0f);
                        PrivatizeBtn.Cooldown.Visible = true;
                        PrivatizeBtn.Cooldown.SetTextWithReplace("$t", changeCooldown.ToString());
                    }
                    else
                    {
                        PrivatizeBtn.Disabled = false;
                        PrivatizeBtn.BtnText.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        PrivatizeBtn.Cooldown.Visible = false;
                    }
                }
                else
                {
                    PrivatizeBtn.Visible = false;
                }

                // nationalize
                if (canNationalize)
                {
                    NationalizeBuyBtn.Visible = true;
                    NationalizeForcedBtn.Visible = true;

                    if (stockpiles.BC >= 250)
                    {
                        NationalizeBuyBtn.Disabled = false;
                        NationalizeBuyBtn.BtnText.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        NationalizeBuyBtn.BtnText.SetTextWithReplace("$v", "-250");
                    }
                    else
                    {
                        NationalizeBuyBtn.Disabled = true;
                        NationalizeBuyBtn.BtnText.SelfModulate = new Color(0.75f, 0.75f, 0.75f, 1.0f);
                        NationalizeBuyBtn.BtnText.SetTextWithReplace("$v", Helper.GetColorPrefix_Bad() + "-250" + Helper.GetColorSufix());
                    }

                    if (stockpiles.Influence >= 150)
                    {
                        NationalizeForcedBtn.Disabled = false;
                        NationalizeBuyBtn.BtnText.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        NationalizeForcedBtn.BtnText.SetTextWithReplace("$v", "-150");
                    }
                    else
                    {
                        NationalizeForcedBtn.Disabled = true;
                        NationalizeForcedBtn.BtnText.SelfModulate = new Color(0.75f, 0.75f, 0.75f, 1.0f);
                        NationalizeForcedBtn.BtnText.SetTextWithReplace("$v", Helper.GetColorPrefix_Bad() + "-150" + Helper.GetColorSufix());
                    }

                    if (changeCooldown > 0)
                    {
                        NationalizeBuyBtn.Disabled = true;
                        NationalizeBuyBtn.BtnText.SelfModulate = new Color(0.75f, 0.75f, 0.75f, 1.0f);
                        NationalizeBuyBtn.Cooldown.Visible = true;
                        NationalizeBuyBtn.Cooldown.SetTextWithReplace("$t", changeCooldown.ToString());

                        NationalizeForcedBtn.Disabled = true;
                        NationalizeForcedBtn.BtnText.SelfModulate = new Color(0.75f, 0.75f, 0.75f, 1.0f);
                        NationalizeForcedBtn.Cooldown.Visible = true;
                        NationalizeForcedBtn.Cooldown.SetTextWithReplace("$t", changeCooldown.ToString());
                    }
                    else
                    {
                        NationalizeBuyBtn.Cooldown.Visible = false;
                        NationalizeForcedBtn.Cooldown.Visible = false;
                    }
                }
                else
                {
                    NationalizeBuyBtn.Visible = false;
                    NationalizeForcedBtn.Visible = false;
                }

                // change district
                if (_District.Pop.GetProgress() == 1000)
                {
                    ChangeDistrictBtn.Visible = true;
                    //PlanDistrictBtn.Visible = false;
                    if (changeCooldown > 0)
                    {
                        ChangeDistrictBtn.Disabled = true;
                        ChangeDistrictBtn.BtnText.SelfModulate = new Color(0.75f, 0.75f, 0.75f, 1.0f);
                        ChangeDistrictBtn.Cooldown.Visible = true;
                        ChangeDistrictBtn.Cooldown.SetTextWithReplace("$t", changeCooldown.ToString());
                    }
                    else
                    {
                        ChangeDistrictBtn.Disabled = false;
                        ChangeDistrictBtn.BtnText.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        ChangeDistrictBtn.Cooldown.Visible = false;
                    }
                }
                else
                {
                    ChangeDistrictBtn.Visible = false;
                    //PlanDistrictBtn.Visible = true;
                }
            }
            else
            {
                InvestBtn.Visible = false;
                IncreaseInvestmentBtn.Visible = false;
                DecreaseInvestmentBtn.Visible = false;
                StopInvestmentBtn.Visible = false;
                PrivatizeBtn.Visible = false;
                NationalizeBuyBtn.Visible = false;
                NationalizeForcedBtn.Visible = false;
                ChangeDistrictBtn.Visible = false;
            }
            PlanDistrictBtn.Visible = false;
        }
        else
        {
            ActionsBg.Visible = false;
            RegainControlBtn.Visible = false;
            ImproveBtn.Visible = false;
            InvestBtn.Visible = false;
            IncreaseInvestmentBtn.Visible = false;
            DecreaseInvestmentBtn.Visible = false;
            StopInvestmentBtn.Visible = false;
            PrivatizeBtn.Visible = false;
            NationalizeBuyBtn.Visible = false;
            NationalizeForcedBtn.Visible = false;
            ChangeDistrictBtn.Visible = false;
            PlanDistrictBtn.Visible = false;
        }
    }

    public void CloseActions()
    {
        BGIdle.Visible = true;
        BGSelected.Visible = false;

        IconBtn.Visible = true;
        IconSelected.Visible = false;
        PopSelected.Visible = false;

        ActionsBg.Visible = false;
    }

    public void OnSelect()
    {
        Game.self.GalaxyUI.PlanetInfo.SelectDistrict(this);
    }
    public void OnSelectPop()
    {
        Game.self.GalaxyUI.PlanetInfo.SelectPop(this);
    }

    public void OnRegainControl()
    {

    }
    public void OnImprove()
    {
        ActionDistrict.Improve(_District);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnInvest()
    {
        ActionDistrict.Invest(_District);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnIncreaseInvestment()
    {
        ActionDistrict.Invest(_District);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnDecreaseInvestment()
    {
        ActionDistrict.Devest(_District);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnStopInvestment()
    {
        ActionDistrict.Devest(_District);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnPrivatize()
    {
        ActionDistrict.Privatize(_District);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnNationalizeBuy()
    {
        ActionDistrict.Nationalize(_District, true);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnNationalizeForced()
    {
        ActionDistrict.Nationalize(_District, false);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }

    public void OnChangeDistrict()
    {
        Game.self.GalaxyUI.PlanetInfo.OpenChooseDistrictWindow(this);
        ChangeDistrictBtn.Visible = false;
    }
}