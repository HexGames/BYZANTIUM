using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIStarInfoSystem : Control
{
    [ExportCategory("Links")]
    [Export]
    public UIText OwnerName;
    [Export]
    public TextureRect OwnerColor;
    [Export]
    public TextureRect OwnerIcon;

    [Export]
    public UIText ProjectName;
    [Export]
    public UIText ProjectTime;
    [Export]
    public UIProgress ProjectProgress;
    [Export]
    public UIPips ProjectBudgetLevel;

    //[Export]
    //public UIText PrivateProjectName;
    [Export]
    public UIText PrivateProjectTime;
    [Export]
    public UIProgress PrivateProjectProgress;

    [Export]
    public UIText PopsGrowthTime;
    [Export]
    public UIProgress PopsGrowthProgress;

    [Export]
    public UIText PopsValue;
    [Export]
    public UIText PopsHappyValue;
    [Export]
    public UIText PopsNeutralValue;
    [Export]
    public UIText PopsUnhappyValue;
    [Export]
    public UIIconValue PopsWealth;
    [Export]
    public UIIconValue PopsInequality;
    [Export]
    public UIIconValue PopsCorruption;
    [Export]
    public UIIconValue PopsSocialUnrest;

    [Export]
    public UIText StabilityChangeText;
    [Export]
    public Control StabilityChangeDecrease;
    [Export]
    public Control StabilityChangeIncrease;
    [Export]
    public TextureRect StabilityCursor;
    [Export]
    public UIPips StabilityControlLevel;
    [Export]
    public Control StabilityAlert;

    [Export]
    public UIPips TaxLevel;
    [Export]
    public UIPips WelfareLevel;

    [Export]
    public UIText IncomeBC;
    [Export]
    public UIText IncomeInf;
    [Export]
    public UIText IncomeRes;

    [Export]
    public UIText ShipbuildingIncome;
    [Export]
    public TextureRect ShipbuildingIcon;
    [Export]
    public UIText ShipbuildingName;
    [Export]
    public UIText ShipbuildingTurns;
    [Export]
    public UIProgress ShipbuildingProgress;


    [ExportCategory("Runtime")]
    [Export]
    public SystemData _System = null;

    public void Refresh(SystemData system)
    {
        //GD.Print("-- xxx ---");

        _System = system;

        OwnerName.SetTextWithReplace("$name", _System._Player.PlayerName);
        OwnerColor.SelfModulate = Game.self.UILib.GetPlayerColor(_System._Player.PlayerID);
        //OwnerIcon.Texture = Game.self.Assets.GetTexture2D_Flag(_System._Player.PlayerName + ".png");

        // --- projects
        //ProjectName.SetTextWithReplace("$name", "???");
        ProjectTime.SetTextWithReplace("$t", "oo");
        ProjectProgress.SetProgress(0, 0, 100);
        ProjectBudgetLevel.SetPips(0);

        //PrivateProjectName.;
        PrivateProjectTime.SetTextWithReplace("$t", "oo");
        PrivateProjectProgress.SetProgress(0, 0, 100);

        // --- pops
        PopsGrowthTime.SetTextWithReplace("$t", "oo");
        PopsGrowthProgress.SetProgress(0, 0, 100);
        PopsValue.SetTextWithReplace("$val", _System.GetPopsCurrent().ToString());

        PopsHappyValue.SetTextWithReplace("$v", "?");
        PopsNeutralValue.SetTextWithReplace("$v", "?");
        PopsUnhappyValue.SetTextWithReplace("$v", "?");

        PopsWealth.SetValue(0);
        PopsInequality.SetValue(0);
        PopsCorruption.SetValue(0);
        PopsSocialUnrest.SetValue(0);

        // stability
        StabilityChangeText.SetTextWithReplace("$v", "0");
        StabilityChangeDecrease.Visible = false;
        StabilityChangeIncrease.Visible = false;
        if (_System.Control_PerTurn.RebellionCurrent <= _System.Control_PerTurn.RebellionLoyal) StabilityCursor.SetPosition(new Vector2(8, 28));
        else if (_System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionMax) StabilityCursor.SetPosition(new Vector2(280, 28));
        else StabilityCursor.SetPosition(new Vector2(24 + (240 * (_System.Control_PerTurn.RebellionCurrent - _System.Control_PerTurn.RebellionLoyal) / (_System.Control_PerTurn.RebellionMax - _System.Control_PerTurn.RebellionLoyal)), 28));
        StabilityControlLevel.SetPips(0);
        StabilityAlert.Visible = false;

        // tax and Wealfare
        TaxLevel.SetPips(0);
        WelfareLevel.SetPips(0);

        // income
        IncomeBC.SetTextWithReplace("$val", _System.Economy_PerTurn.ToString_BC());
        IncomeInf.SetTextWithReplace("$val", _System.Economy_PerTurn.ToString_Influence());
        IncomeRes.SetTextWithReplace("$val", _System.Economy_PerTurn.ToString_Research());

        // shipbuilding
        ShipbuildingIncome.SetTextWithReplace("$val", _System.Economy_PerTurn.ToString_Shipbuilding());
        //ShipbuildingIcon.Texture = 
        ShipbuildingName.SetTextWithReplace("$name", "Space Ship");
        ShipbuildingTurns.SetTextWithReplace("$t", "oo");
        ShipbuildingProgress.SetProgress(0, 0, 100);

        //Pops.SetTextWithReplace("$val", _System.Pops_PerTurn.ToString_Pops(), "$max", _System.Pops_PerTurn.ToString_PopsMax());
        //PopsGrowth.SetTextWithReplace("$v", _System.Pops_PerTurn.ToString_GrowthTotal());
        //PopsTurns.SetTextWithReplace("$t", _System.Pops_PerTurn.ToString_GrowthTurns());
        //
        //PopsProgressCurrent.MaxValue = _System.Pops_PerTurn.GrowthProgressMax;
        //PopsProgressCurrent.Value = _System.Pops_PerTurn.GrowthProgress;
        //PopsProgressNextTurn.MaxValue = _System.Pops_PerTurn.GrowthProgressMax;
        //PopsProgressNextTurn.Value = _System.Pops_PerTurn.GrowthProgressNextTurn;
        //
        //PopsGrowthTooltip.Row_1_Right = PopsGrowthTooltip.Row_1_Right_Original
        //    .Replace("$v1", _System.Pops_PerTurn.ToString_GrowthFromPops())
        //    .Replace("$v2", _System.Pops_PerTurn.ToString_GrowthIncommingTrade());
        //int outTrade = _System.Pops_PerTurn.GrowthOutgoingTrade;
        //int waste = _System.Pops_PerTurn.GrowthWaste;
        //PopsGrowthTooltip.Row_2_Right = PopsGrowthTooltip.Row_2_Right_Original
        //    .Replace("$v1", (outTrade < 0 ? Helper.GetColorPrefix_Bad() : "") + _System.Pops_PerTurn.ToString_GrowthOutgoingTrade() + (outTrade < 0 ? Helper.GetColorSufix() : ""))
        //    .Replace("$v2", (waste < 0 ? Helper.GetColorPrefix_Bad() : "") + _System.Pops_PerTurn.ToString_GrowthWaste() + (waste < 0 ? Helper.GetColorSufix() : ""));
        //
        //PopsUnhappy.SetTextWithReplace("$v", _System.Pops_PerTurn.ToString_Unhappy());
        //PopsNeutral.SetTextWithReplace("$v", _System.Pops_PerTurn.ToString_Neutral());
        //PopsHappy.SetTextWithReplace("$v", _System.Pops_PerTurn.ToString_Happy());
        //
        ////
        //Wealth.SetTextWithReplace("$val", _System.Economy_PerTurn.WealthLevel.ToString());
        //WealthProgressCurrent.MaxValue = _System.Economy_PerTurn.WealthMax;
        //WealthProgressCurrent.Value = _System.Economy_PerTurn.WealthProgress;
        //WealthProgressNextTurn.MaxValue = _System.Economy_PerTurn.WealthMax;
        //WealthProgressNextTurn.Value = _System.Economy_PerTurn.WealthProgress;
        //WealthTooltip.Title = "Wealth";
        //WealthTooltip.Row_1 = "bah";
        //
        //WealthInequality.SetTextWithReplace("$val", _System.Economy_PerTurn.Inequality.ToString());
        //WealthInequalityTooltip.Title = "Inequality";
        //WealthInequalityTooltip.Row_1 = "bah";
        //
        //if (_System.DistrictToInvest_PerTurn != null)
        //{
        //    WealthInvestBg.Visible = true;
        //    WealthInvest.SetTextWithReplace("$t", _System.DistrictToInvest_PerTurn.Economy_PerTurn.ReinvestTurns.ToString());
        //    WealthInvestCurrent.MaxValue = _System.DistrictToInvest_PerTurn.Economy_PerTurn.ReinvestMax;
        //    WealthInvestCurrent.Value = _System.DistrictToInvest_PerTurn.Economy_PerTurn.ReinvestProgress;
        //    WealthInvestNextTurn.MaxValue = _System.DistrictToInvest_PerTurn.Economy_PerTurn.ReinvestMax;
        //    WealthInvestNextTurn.Value = _System.DistrictToInvest_PerTurn.Economy_PerTurn.ReinvestProgress;
        //    WealthInvestTooltip.Title = "Invest";
        //    WealthInvestTooltip.Row_1 = "bah";
        //}
        //else
        //{
        //    WealthInvestBg.Visible = false;
        //}
        //
        ////
        //ControlText.SetTextWithReplace("$val", _System.Control_PerTurn.ToString_ControlTotal());//.Replace("$max", _System.Control_PerTurn.ToString_Control());
        //
        //if (_System.Control_PerTurn.RebellionChange < 0)
        //{
        //    ControlChangeText.Visible = true;
        //    ControlChangeText.SetTextWithReplace("$v", Helper.GetColorPrefix_Good() + (-_System.Control_PerTurn.RebellionChange).ToString() + Helper.GetColorSufix());
        //    ControlChangeDecrease.Visible = true;
        //    ControlChangeIncrease.Visible = false;
        //}
        //else if (_System.Control_PerTurn.RebellionChange < 0)
        //{
        //    ControlChangeText.Visible = true;
        //    ControlChangeText.SetTextWithReplace("$v", Helper.GetColorPrefix_Bad() + _System.Control_PerTurn.RebellionChange.ToString() + Helper.GetColorSufix());
        //    ControlChangeDecrease.Visible = false;
        //    ControlChangeIncrease.Visible = true;
        //}
        //else
        //{
        //    ControlChangeText.Visible = false;
        //    ControlChangeDecrease.Visible = false;
        //    ControlChangeIncrease.Visible = false;
        //}
        //
        //int range = (_System.Control_PerTurn.RebellionMax - _System.Control_PerTurn.RebellionLoyal);
        //int remaining = 240;
        //int size = (_System.Control_PerTurn.RebellionMax - _System.Control_PerTurn.RebellionRebelious) * 240 / range;
        //remaining -= size;
        //ControlRebelious.CustomMinimumSize = new Vector2(size, 16);
        //size = (_System.Control_PerTurn.RebellionRebelious - _System.Control_PerTurn.RebellionTurmoil) * 240 / range;
        //remaining -= size;
        //ControlTurmoil.CustomMinimumSize = new Vector2(size, 16);
        //size = (_System.Control_PerTurn.RebellionTurmoil - _System.Control_PerTurn.RebellionWavering) * 240 / range;
        //remaining -= size;
        //ControlWavering.CustomMinimumSize = new Vector2(size, 16);
        //size = (_System.Control_PerTurn.RebellionWavering - _System.Control_PerTurn.RebellionUnsure) * 240 / range;
        //remaining -= size;
        //ControlUnsure.CustomMinimumSize = new Vector2(size, 16);
        //ControlLoyal.CustomMinimumSize = new Vector2(remaining, 16);
        //
        //if (_System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionRebelious) ControlCursor.SelfModulate = ControlRebelious.Color;
        //else if (_System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionTurmoil) ControlCursor.SelfModulate = ControlTurmoil.Color;
        //else if (_System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionWavering) ControlCursor.SelfModulate = ControlWavering.Color;
        //else if (_System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionUnsure) ControlCursor.SelfModulate = ControlUnsure.Color;
        //else ControlCursor.SelfModulate = ControlLoyal.Color;
        //
        //if (_System.Control_PerTurn.RebellionCurrent <= _System.Control_PerTurn.RebellionLoyal) ControlCursor.SetPosition(new Vector2(8, 28));
        //else if (_System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionMax) ControlCursor.SetPosition(new Vector2(280, 28));
        //else ControlCursor.SetPosition(new Vector2(24 + (240 * (_System.Control_PerTurn.RebellionCurrent - _System.Control_PerTurn.RebellionLoyal) / range), 28));
        //
        //ControlCorruption.SetTextWithReplace("$val", "??");
        //ControlCorruptionTooltip.Title = "Corruption";
        //ControlCorruptionTooltip.Row_1 = "bah";
        //
        //ControlAlert.Visible = _System.Control_PerTurn.RebellionChange < 0 && _System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionUnsure;

        //Infrastructure.SetTextWithReplace("$val", _System.Infrastructure_PerTurn.ToString_InfrastructureUsed(), "$max", _System.Infrastructure_PerTurn.ToString_Infrastructure());
        //InfrastructureGrowth.SetTextWithReplace("$v", _System.Infrastructure_PerTurn.ToString_Construction());
        //InfrastructureTurns.SetTextWithReplace("$t", _System.Infrastructure_PerTurn.ToString_ConstructionTurns());
        //
        //InfrastructureProgressCurrent.MaxValue = _System.Infrastructure_PerTurn.ConstructionProgressMax;
        //InfrastructureProgressCurrent.Value = _System.Infrastructure_PerTurn.ConstructionProgress;
        //InfrastructureProgressNextTurn.MaxValue = _System.Infrastructure_PerTurn.ConstructionProgressMax;
        //InfrastructureProgressNextTurn.Value = _System.Infrastructure_PerTurn.ConstructionProgressNextTurn;
        //
        //int points = _System.Infrastructure_PerTurn.Infrastructure - _System.Infrastructure_PerTurn.InfrastructureUsed;
        //InfrastructureAlert.Visible = points > 0;
        //InfrastructureAlertPoints.SetTextWithReplace("$v", points.ToString());

        //int taxLevel = _System.Economy_PerTurn.Tax;
        //TaxPip_1.Visible = taxLevel == 1;
        //TaxPip_2.Visible = taxLevel == 2;
        //TaxPip_3.Visible = taxLevel == 3;
        //
        //IncomeBC.SetTextWithReplace("$val", _System.Economy_PerTurn.ToString_BC());
        //IncomeInf.SetTextWithReplace("$val", _System.Economy_PerTurn.ToString_Influence());
        //IncomeRes.SetTextWithReplace("$val", _System.Economy_PerTurn.ToString_Research());
        //
        //if (_System.Shipbuilding_PerTurn.Shipbuilding > 0)
        //{
        //    Shipbuilding.Visible = true;
        //    NoShipbuilding.Visible = false;
        //
        //    ShipbuildingLeft.Visible = _System.Shipbuilding_PerTurn.DesignIdx > 0;
        //    ShipbuildingRight.Visible = _System.Shipbuilding_PerTurn.DesignIdx < _System.Shipbuilding_PerTurn.DesignIdxMax - 1;
        //    //ShipbuildingIcon = _System.Shipbuilding_PerTurn.DesignCurrent.
        //    ShipbuildingName.SetTextWithReplace("$name", _System.Shipbuilding_PerTurn.DesignCurrent.DesignName);
        //    ShipbuildingIncome.SetTextWithReplace("$v", _System.Shipbuilding_PerTurn.ToString_Shipbuilding());
        //    ShipbuildingProgressTurns.SetTextWithReplace("$t", _System.Shipbuilding_PerTurn.ToString_Turns());
        //    ShipbuildingProgressCurrent.MaxValue = _System.Shipbuilding_PerTurn.ProgressMax;
        //    ShipbuildingProgressCurrent.Value = _System.Shipbuilding_PerTurn.ProgressCurrent;
        //    ShipbuildingProgressNextTurn.MaxValue = _System.Shipbuilding_PerTurn.ProgressMax;
        //    ShipbuildingProgressNextTurn.Value = _System.Shipbuilding_PerTurn.ProgressNextTurn;
        //}
        //else
        //{
        //    Shipbuilding.Visible = false;
        //    NoShipbuilding.Visible = true;
        //}
    }

    //public void OnTax_0()
    //{
    //    ActionTax.SetTax(_System, 0);
    //    Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    //    if (Game.self.Input.SelectedPlanet != null)
    //    {
    //        Game.self.GalaxyUI.PlanetInfo.Refresh(Game.self.Input.SelectedPlanet);
    //    }
    //}
    //
    //public void OnTax_1()
    //{
    //    ActionTax.SetTax(_System, 1);
    //    Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    //    if (Game.self.Input.SelectedPlanet != null)
    //    {
    //        Game.self.GalaxyUI.PlanetInfo.Refresh(Game.self.Input.SelectedPlanet);
    //    }
    //}
    //
    //public void OnTax_2()
    //{
    //    ActionTax.SetTax(_System, 2);
    //    Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    //    if (Game.self.Input.SelectedPlanet != null)
    //    {
    //        Game.self.GalaxyUI.PlanetInfo.Refresh(Game.self.Input.SelectedPlanet);
    //    }
    //}

    //public void OnShipPrevious()
    //{
    //    DesignData design = _System._Player.GetDesignAtIdx(_System.Shipbuilding_PerTurn.DesignIdx - 1);
    //    ActionShipbuilding.ChangeShipTo(_System, design);
    //    Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    //}

    //public void OnShipNext()
    //{
    //    DesignData design = _System._Player.GetDesignAtIdx(_System.Shipbuilding_PerTurn.DesignIdx + 1);
    //    ActionShipbuilding.ChangeShipTo(_System, design);
    //    Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    //}
}