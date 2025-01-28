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

    //private Control PopBg;
    //private Control PopSelected;
    //private UIText PopText_TEMP;
    //private UITooltipTrigger PopTooltip;

    //private Control NoPopBg;
    //private UIText NoPopText;
    //private UITooltipTrigger NoPopTooltip;

    //private Control ControlUncontroled;
    //private Control ControlPrivate;
    //private Control ControlStateOwned;

    private Control SystemInvestment;
    private UIText Production;

    //private Control Construction;
    //private List<UIPanel> ConstructionPipsOn = new List<UIPanel>();
    //private List<UIPanel> ConstructionPipsOff = new List<UIPanel>();
    //private UIPanel ConstructionInProgress;
    //private UIText ConstructionInProgressText;

    private Control UpgradeProgress;
    private UIText UpgradeProgressTurns;
    private ProgressBar UpgradeProgressCurrent;
    private ProgressBar UpgradeProgressNextTurn;

    private Control PrivateProgress;
    private UIText PrivateProgressTurns;
    private ProgressBar PrivateProgressCurrent;
    private ProgressBar PrivateProgressNextTurn;

    private Control Investment;
    private List<UIPanel> InvestmentPipsOn = new List<UIPanel>();
    private List<UIPanel> InvestmentPipsOff = new List<UIPanel>();

    private Control PopProgress;
    private UIText PopProgressTurns;
    private ProgressBar PopsProgressCurrent;
    private ProgressBar PopsProgressNextTurn;


    private Control ActionsBg;
    private UIButton CancelChangeBtn;
    private UIButton RegainControlBtn;
    private UIButton PlanDistrictBtn;
    private UIButton UpgradeBtn;
    private UIButton InvestBtn;
    private UIButton IncreaseInvestmentBtn;
    private UIButton DecreaseInvestmentBtn;
    private UIButton StopInvestmentBtn;
    private UIButton PrivatizeBtn;
    private UIButton NationalizeBtn;
    private UIButton ChangeDistrictBtn;

    // Runtime
    public DistrictData _District = null;

    private UIButton SelectedButton = null;

    public override void _Ready()
    {
        BGIdle = GetNode<Control>("PanelBg");
        BGSelected = GetNode<Control>("SelectedBG");

        IconBtn = GetNode<Button>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Button");
        IconSelected = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Selected");
        IconTexture = GetNode<TextureRect>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Icon");
        IconTooltip = GetNode<UITooltipTrigger>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Button/ToolTip");

        //PopBg = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/Icon/PopBg");
        //PopSelected = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/Icon/PopSelected");
        //PopText_TEMP = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/Icon/PopBg/IconText");
        //PopTooltip = GetNode<UITooltipTrigger>("MarginContainer/VBoxContainer/HBoxContainer/Icon/PopBg/ToolTip");

        //NoPopBg = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/Icon/NoPopBg");
        //NoPopText = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/Icon/NoPopBg/IconText");
        //NoPopTooltip = GetNode<UITooltipTrigger>("MarginContainer/VBoxContainer/HBoxContainer/Icon/NoPopBg/ToolTip");

        //ControlUncontroled = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Effects/Control/Uncontroled");
        //ControlPrivate = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Effects/Control/PrivateBusiness");
        //ControlStateOwned = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Effects/Control/StateOwned");

        SystemInvestment = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Effects/SystemInvestment");

        Production = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Effects/Production");

        //Construction = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction");
        //ConstructionPipsOn.Clear();
        //ConstructionPipsOn.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Pip_1"));
        //ConstructionPipsOn.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Pip_2"));
        //ConstructionPipsOn.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Pip_3"));
        //
        //ConstructionPipsOff.Clear();
        //ConstructionPipsOff.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Unused_1"));
        //ConstructionPipsOff.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Unused_2"));
        //ConstructionPipsOff.Add(GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Unused_3"));
        //
        //ConstructionInProgress = GetNode<UIPanel>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Delay");
        //ConstructionInProgressText = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Construction/Delay/Delay");

        UpgradeProgress = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/ProgressUpgrade");
        UpgradeProgressTurns = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/ProgressUpgrade/Turns");
        UpgradeProgressCurrent = GetNode<ProgressBar>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/ProgressUpgrade/Panel/Progress");
        UpgradeProgressNextTurn = GetNode<ProgressBar>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/ProgressUpgrade/Panel/NextTurn");

        PrivateProgress = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Progress");
        PrivateProgressTurns = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Progress/Panel/Turns");
        PrivateProgressCurrent = GetNode<ProgressBar>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Progress/Panel/Progress");
        PrivateProgressNextTurn = GetNode<ProgressBar>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Progress/Panel/NextTurn");

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
        CancelChangeBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/CancelChange");
        RegainControlBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/RegainControl");
        UpgradeBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/Upgrade");
        InvestBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/Invest");
        IncreaseInvestmentBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/IncreaseInvestment");
        DecreaseInvestmentBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/DecreaseInvestment");
        StopInvestmentBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/StopInvestment");
        PrivatizeBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/Privatize");
        NationalizeBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/Nationalize");
        ChangeDistrictBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/ChangeDistrict");
        PlanDistrictBtn = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/PlanDistrict");

        CancelChangeBtn.Selected.Visible = false;
        RegainControlBtn.Selected.Visible = false;
        UpgradeBtn.Selected.Visible = false;
        InvestBtn.Selected.Visible = false;
        IncreaseInvestmentBtn.Selected.Visible = false;
        DecreaseInvestmentBtn.Selected.Visible = false;
        StopInvestmentBtn.Selected.Visible = false;
        PrivatizeBtn.Selected.Visible = false;
        NationalizeBtn.Selected.Visible = false;
        ChangeDistrictBtn.Selected.Visible = false;
        PlanDistrictBtn.Selected.Visible = false;

        SelectedButton = null;
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
                PopProgress.Visible = false;

                if (_District._Colony._System.DistrictToInvest_PerTurn == _District)
                {
                    SystemInvestment.Visible = true;
                }
                else
                {
                    SystemInvestment.Visible = false;
                }

                Production.Visible = true;
                Production.SetTextWithReplace("$value", _District.Economy_PerTurn.ToString_Short(28));

                //
                if (_District._Data.HasSub("ActionChange"))
                {
                    UpgradeProgress.Visible = true;
                    int progress = _District._Data.GetSubValueI("ActionChange", "Progress");
                    int maxProgress = _District._Data.GetSubValueI("ActionChange", "ProgressMax");
                    UpgradeProgressCurrent.MaxValue = maxProgress;
                    UpgradeProgressCurrent.Value = progress;
                    UpgradeProgressNextTurn.MaxValue = maxProgress;
                    UpgradeProgressNextTurn.Value = progress + 1;

                    UpgradeProgressTurns.SetTextWithReplace("$t", (maxProgress - progress).ToString());
                }
                else
                {
                    UpgradeProgress.Visible = false;
                }

                //
                if (_District.Economy_PerTurn.ReinvestActive)
                {
                    PrivateProgress.Visible = true;
                    PrivateProgressCurrent.MaxValue = _District.Economy_PerTurn.ReinvestMax;
                    PrivateProgressCurrent.Value = _District.Economy_PerTurn.ReinvestProgress;
                    PrivateProgressNextTurn.MaxValue = _District.Economy_PerTurn.ReinvestMax;
                    PrivateProgressNextTurn.Value = _District.Economy_PerTurn.ReinvestProgress + _District.Economy_PerTurn.ReinvestPerTurn;
                    PrivateProgressTurns.SetTextWithReplace("$t", _District.Economy_PerTurn.ReinvestTurns.ToString());
                }
                else
                {
                    PrivateProgress.Visible = false;
                }

                //
                if (district.DistrictDef.Control_Type == "Private")
                {
                    if (_District._Data.HasSub("InvestLevel"))
                    {
                        Investment.Visible = true;
                        int value = _District._Data.GetSub("InvestLevel").ValueI;

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
                    Investment.Visible = false;
                }
            }
            else
            {
                //PopBg.Visible = false;
                //NoPopBg.Visible = true;

                //if (_District.IsStateOwned())
                //{
                //    ControlStateOwned.Visible = true;
                //    ControlPrivate.Visible = false;
                //    ControlUncontroled.Visible = false;
                //}
                //else if (_District.IsPrivate())
                //{
                //    ControlStateOwned.Visible = false;
                //    ControlPrivate.Visible = true;
                //    ControlUncontroled.Visible = false;
                //}
                //else
                //{
                //    ControlStateOwned.Visible = false;
                //    ControlPrivate.Visible = false;
                //    ControlUncontroled.Visible = true;
                //}

                PopProgress.Visible = true;
                PopsProgressCurrent.MaxValue = 1000;
                PopsProgressCurrent.Value = growthProgress;
                if (_District._Colony._System.Data.GetSubValueS("ActionGrowth", "FocusColony") == _District._Colony.ColonyName)
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

                SystemInvestment.Visible = false;
                Production.Visible = false;
                //Construction.Visible = false;
                PrivateProgress.Visible = false;
                Investment.Visible = false;
            }
        }
        else if (_District._Colony.Planet.IsHabitable())
        {
            IconTexture.Texture = Game.self.Def.AssetLib.GetTexture2D_District("Empty.png");

            //PopBg.Visible = false;
            //NoPopBg.Visible = true;

            //ControlStateOwned.Visible = false;
            //ControlPrivate.Visible = false;
            //ControlUncontroled.Visible = false;

            SystemInvestment.Visible = false;
            Production.Visible = true;
            Production.SetTextWithReplace("$value", "New " + Helper.GetIcon("Pops", 28) + " in " + "5" + Helper.GetIcon("Turn", 28));

            //Construction.Visible = false;
            Investment.Visible = false;
        }
        else
        {
            //PopBg.Visible = false;
            //NoPopBg.Visible = false;
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
        //PopSelected.Visible = !district;

        ActionsBg.Visible = true;
        if (district)
        {
            var stockpiles = _District._Colony._System._Player.Stockpiles_PerTurn;
            bool fullPop = _District.Pop.GetProgress() == 1000;
            string control = _District.DistrictDef.Control_Type;
            bool hasAction = _District._Data.HasSub("ActionChange");
            int changeProgress = _District._Data.GetSubValueI("ActionChange", "Progress");
            int changeProgressMax = _District._Data.GetSubValueI("ActionChange", "ProgressMax");
            bool defCanInvest = _District.DistrictDef.CanInvest();
            int investLevel = _District._Data.GetSubValueI("InvestLevel");
            int investLessCost = 1;
            int investMoreCost = 1;
            bool canPrivatize = _District.DistrictDef.CanPrivatize();
            bool canNationalize = _District.DistrictDef.CanNationalize();

            ActionsBg.Visible = true;

            if (hasAction && changeProgress == 0)
            {
                CancelChangeBtn.Visible = true;
            }
            else
            {
                CancelChangeBtn.Visible = false;
            }

            //
            if (_District.DistrictDef.Control_Type == "State_Owned" || _District.DistrictDef.Control_Type == "Private")
            {
                RegainControlBtn.Visible = false;
            }

            //
            if (fullPop == false)
            {
                PlanDistrictBtn.Visible = true;
            }
            else
            {
                PlanDistrictBtn.Visible = false;
            }

            // 
            if (_District.DistrictDef.Control_Type == "State_Owned")
            {
                UpgradeBtn.Visible = true;
                UpgradeBtn.RefreshCooldown(changeProgressMax - changeProgress);
            }
            else
            {
                UpgradeBtn.Visible = false;
            }

            //
            if (fullPop && defCanInvest)
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

            //
            if (_District.DistrictDef.Type == "District")
            {
                //
                if (canPrivatize)
                {
                    PrivatizeBtn.Visible = true;
                    PrivatizeBtn.RefreshCooldown(changeProgressMax - changeProgress);
                }
                else
                {
                    PrivatizeBtn.Visible = false;
                }

                //
                if (canNationalize)
                {
                    NationalizeBtn.Visible = true;
                    NationalizeBtn.RefreshCooldown(changeProgressMax - changeProgress);
                }
                else
                {
                    NationalizeBtn.Visible = false;
                }

                //
                if (fullPop)
                {
                    ChangeDistrictBtn.Visible = true;
                    ChangeDistrictBtn.RefreshCooldown(changeProgressMax - changeProgress);
                }
                else
                {
                    ChangeDistrictBtn.Visible = false;
                }
            }
            else
            {
                InvestBtn.Visible = false;
                IncreaseInvestmentBtn.Visible = false;
                DecreaseInvestmentBtn.Visible = false;
                StopInvestmentBtn.Visible = false;
                PrivatizeBtn.Visible = false;
                NationalizeBtn.Visible = false;
                ChangeDistrictBtn.Visible = false;
            }
        }
        else
        {
            ActionsBg.Visible = false;
            RegainControlBtn.Visible = false;
            UpgradeBtn.Visible = false;
            InvestBtn.Visible = false;
            IncreaseInvestmentBtn.Visible = false;
            DecreaseInvestmentBtn.Visible = false;
            StopInvestmentBtn.Visible = false;
            PrivatizeBtn.Visible = false;
            NationalizeBtn.Visible = false;
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
        //PopSelected.Visible = false;

        ActionsBg.Visible = false;
    }

    public void OnSelect()
    {
        //Game.self.GalaxyUI.PlanetInfo.SelectDistrict(this);
    }
    public void OnSelectPop()
    {
        //Game.self.GalaxyUI.PlanetInfo.SelectPop(this);
    }

    public void OnCancelChange()
    {
        DeselectBtn();

        ActionDistrict.CancelChange(_District);

        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        //Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }

    public void OnRegainControl()
    {
        SelectBtn(RegainControlBtn);
    }

    public void OnUpgrade()
    {
        SelectBtn(UpgradeBtn);
        //Game.self.GalaxyUI.PlanetInfo.OpenChooseDistrictWindowUpgrade(this);
    }
    public void OnInvest()
    {
        DeselectBtn();
        ActionDistrict.Invest(_District);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
       //Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnIncreaseInvestment()
    {
        DeselectBtn();
        ActionDistrict.Invest(_District);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        //Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnDecreaseInvestment()
    {
        DeselectBtn();
        ActionDistrict.Devest(_District);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        //Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnStopInvestment()
    {
        DeselectBtn();
        ActionDistrict.Devest(_District);
        Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        //Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnPrivatize()
    {
        SelectBtn(PrivatizeBtn);
        //Game.self.GalaxyUI.PlanetInfo.OpenChooseDistrictWindowChangeControl(this);
        //ActionDistrict.Privatize(_District);
        //Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        //Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }
    public void OnNationalize()
    {
        SelectBtn(NationalizeBtn);
        //Game.self.GalaxyUI.PlanetInfo.OpenChooseDistrictWindowChangeControl(this);
        //ActionDistrict.Nationalize(_District, true);
        //Game.self.GalaxyUI.SystemInfo.Refresh(_District._Colony._System.Star);
        //Game.self.GalaxyUI.PlanetInfo.Refresh(_District._Colony.Planet);
    }

    public void OnChangeDistrict()
    {
        SelectBtn(ChangeDistrictBtn);
        //Game.self.GalaxyUI.PlanetInfo.OpenChooseDistrictWindowChangeType(this);
        //ChangeDistrictBtn.Visible = false;
    }

    public void OnPlanDistrct()
    {
        SelectBtn(PlanDistrictBtn);
        //Game.self.GalaxyUI.PlanetInfo.OpenChooseDistrictWindowChangeType(this);
        //ChangeDistrictBtn.Visible = false;
    }

    public void SelectBtn(UIButton button)
    {
        if (SelectedButton != null) SelectedButton.Selected.Visible = false;
        SelectedButton = button;
        SelectedButton.Selected.Visible = true;
    }
    public void DeselectBtn()
    {
        if (SelectedButton != null) SelectedButton.Selected.Visible = false;
        SelectedButton = null;
    }
}