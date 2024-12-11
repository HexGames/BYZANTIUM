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
    public UIText Pops;
    [Export]
    public UIText PopsGrowth;
    [Export]
    public UIText PopsTurns;
    [Export]
    private ProgressBar PopsProgressCurrent = null;
    [Export]
    private ProgressBar PopsProgressNextTurn = null;
    [Export]
    public UITooltipTrigger PopsGrowthTooltip = null;
    [Export]
    public UIText PopsUnhappy;
    [Export]
    public UIText PopsNeutral;
    [Export]
    public UIText PopsHappy;

    [Export]
    public UIText ControlText;

    [Export]
    public UIText ControlChangeText;
    [Export]
    public Control ControlChangeDecrease;
    [Export]
    public Control ControlChangeIncrease;
    [Export]
    public ColorRect ControlLoyal;
    [Export]
    public ColorRect ControlUnsure;
    [Export]
    public ColorRect ControlWavering;
    [Export]
    public ColorRect ControlTurmoil;
    [Export]
    public ColorRect ControlRebelious;
    [Export]
    public TextureRect ControlCursor;
    [Export]
    public Control ControlAlert;

    [Export]
    public UIText Infrastructure;
    [Export]
    public UIText InfrastructureGrowth;
    [Export]
    public UIText InfrastructureTurns;
    [Export]
    private ProgressBar InfrastructureProgressCurrent = null;
    [Export]
    private ProgressBar InfrastructureProgressNextTurn = null;
    [Export]
    public Control InfrastructureAlert;
    [Export]
    public UIText InfrastructureAlertPoints;

    [Export]
    public Control TaxPip_1;
    [Export]
    public Control TaxPip_2;
    [Export]
    public Control TaxPip_3;

    [Export]
    public UIText IncomeBC;
    [Export]
    public UIText IncomeInf;
    [Export]
    public UIText IncomeRes;

    [Export]
    public Control NoShipbuilding;
    [Export]
    public Control Shipbuilding;
    [Export]
    public Button ShipbuildingLeft;
    [Export]
    public Button ShipbuildingRight;
    [Export]
    public TextureRect ShipbuildingIcon;
    [Export]
    public UIText ShipbuildingName;
    [Export]
    public UIText ShipbuildingIncome;
    [Export]
    public UIText ShipbuildingProgressTurns;
    [Export]
    public ProgressBar ShipbuildingProgressCurrent;
    [Export]
    public ProgressBar ShipbuildingProgressNextTurn;


    [ExportCategory("Runtime")]
    [Export]
    public SystemData _System = null;

    public void Refresh(SystemData system)
    {
        //GD.Print("-- xxx ---");

        _System = system;

        OwnerName.SetTextWithReplace("$name", _System._Player.PlayerName);
        OwnerColor.SelfModulate = Game.self.UILib.GetPlayerColor(_System._Player.PlayerID);

        Pops.SetTextWithReplace("$val", _System.Pops_PerTurn.ToString_Pops(), "$max", _System.Pops_PerTurn.ToString_PopsMax());
        PopsGrowth.SetTextWithReplace("$v", _System.Pops_PerTurn.ToString_GrowthTotal());
        PopsTurns.SetTextWithReplace("$t", _System.Pops_PerTurn.ToString_GrowthTurns());

        PopsProgressCurrent.MaxValue = _System.Pops_PerTurn.GrowthProgressMax;
        PopsProgressCurrent.Value = _System.Pops_PerTurn.GrowthProgress;
        PopsProgressNextTurn.MaxValue = _System.Pops_PerTurn.GrowthProgressMax;
        PopsProgressNextTurn.Value = _System.Pops_PerTurn.GrowthProgressNextTurn;

        PopsGrowthTooltip.Row_1_Right = PopsGrowthTooltip.Row_1_Right_Original
            .Replace("$v1", _System.Pops_PerTurn.ToString_GrowthFromPops())
            .Replace("$v2", _System.Pops_PerTurn.ToString_GrowthIncommingTrade());
        int outTrade = _System.Pops_PerTurn.GrowthOutgoingTrade;
        int waste = _System.Pops_PerTurn.GrowthWaste;
        PopsGrowthTooltip.Row_2_Right = PopsGrowthTooltip.Row_2_Right_Original
            .Replace("$v1", (outTrade < 0 ? Helper.GetColorPrefix_Bad() : "") + _System.Pops_PerTurn.ToString_GrowthOutgoingTrade() + (outTrade < 0 ? Helper.GetColorSufix() : ""))
            .Replace("$v2", (waste < 0 ? Helper.GetColorPrefix_Bad() : "") + _System.Pops_PerTurn.ToString_GrowthWaste() + (waste < 0 ? Helper.GetColorSufix() : ""));

        PopsUnhappy.SetTextWithReplace("$v", _System.Pops_PerTurn.ToString_Unhappy());
        PopsNeutral.SetTextWithReplace("$v", _System.Pops_PerTurn.ToString_Neutral());
        PopsHappy.SetTextWithReplace("$v", _System.Pops_PerTurn.ToString_Happy());

        ControlText.SetTextWithReplace("$val", _System.Control_PerTurn.ToString_ControlTotal());//.Replace("$max", _System.Control_PerTurn.ToString_Control());

        if (_System.Control_PerTurn.RebellionChange < 0)
        {
            ControlChangeText.Visible = true;
            ControlChangeText.SetTextWithReplace("$v", Helper.GetColorPrefix_Good() + (-_System.Control_PerTurn.RebellionChange).ToString() + Helper.GetColorSufix());
            ControlChangeDecrease.Visible = true;
            ControlChangeIncrease.Visible = false;
        }
        else if (_System.Control_PerTurn.RebellionChange < 0)
        {
            ControlChangeText.Visible = true;
            ControlChangeText.SetTextWithReplace("$v", Helper.GetColorPrefix_Bad() + _System.Control_PerTurn.RebellionChange.ToString() + Helper.GetColorSufix());
            ControlChangeDecrease.Visible = false;
            ControlChangeIncrease.Visible = true;
        }
        else
        {
            ControlChangeText.Visible = false;
            ControlChangeDecrease.Visible = false;
            ControlChangeIncrease.Visible = false;
        }

        int range = (_System.Control_PerTurn.RebellionMax - _System.Control_PerTurn.RebellionLoyal);
        int remaining = 240;
        int size = (_System.Control_PerTurn.RebellionMax - _System.Control_PerTurn.RebellionRebelious) * 240 / range;
        remaining -= size;
        ControlRebelious.CustomMinimumSize = new Vector2(size, 16);
        size = (_System.Control_PerTurn.RebellionRebelious - _System.Control_PerTurn.RebellionTurmoil) * 240 / range;
        remaining -= size;
        ControlTurmoil.CustomMinimumSize = new Vector2(size, 16);
        size = (_System.Control_PerTurn.RebellionTurmoil - _System.Control_PerTurn.RebellionWavering) * 240 / range;
        remaining -= size;
        ControlWavering.CustomMinimumSize = new Vector2(size, 16);
        size = (_System.Control_PerTurn.RebellionWavering - _System.Control_PerTurn.RebellionUnsure) * 240 / range;
        remaining -= size;
        ControlUnsure.CustomMinimumSize = new Vector2(size, 16);
        ControlLoyal.CustomMinimumSize = new Vector2(remaining, 16);
        
        if (_System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionRebelious) ControlCursor.SelfModulate = ControlRebelious.Color;
        else if (_System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionTurmoil) ControlCursor.SelfModulate = ControlTurmoil.Color;
        else if (_System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionWavering) ControlCursor.SelfModulate = ControlWavering.Color;
        else if (_System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionUnsure) ControlCursor.SelfModulate = ControlUnsure.Color;
        else ControlCursor.SelfModulate = ControlLoyal.Color;

        if (_System.Control_PerTurn.RebellionCurrent <= _System.Control_PerTurn.RebellionLoyal) ControlCursor.SetPosition(new Vector2(8, 28));
        else if (_System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionMax) ControlCursor.SetPosition(new Vector2(280, 28));
        else ControlCursor.SetPosition(new Vector2(24 + (240 * (_System.Control_PerTurn.RebellionCurrent - _System.Control_PerTurn.RebellionLoyal) / range), 28));

        ControlAlert.Visible = _System.Control_PerTurn.RebellionChange < 0 && _System.Control_PerTurn.RebellionCurrent >= _System.Control_PerTurn.RebellionUnsure;

        Infrastructure.SetTextWithReplace("$val", _System.Infrastructure_PerTurn.ToString_InfrastructureUsed(), "$max", _System.Infrastructure_PerTurn.ToString_Infrastructure());
        InfrastructureGrowth.SetTextWithReplace("$v", _System.Infrastructure_PerTurn.ToString_Construction());
        InfrastructureTurns.SetTextWithReplace("$t", _System.Infrastructure_PerTurn.ToString_ConstructionTurns());
        
        InfrastructureProgressCurrent.MaxValue = _System.Infrastructure_PerTurn.ConstructionProgressMax;
        InfrastructureProgressCurrent.Value = _System.Infrastructure_PerTurn.ConstructionProgress;
        InfrastructureProgressNextTurn.MaxValue = _System.Infrastructure_PerTurn.ConstructionProgressMax;
        InfrastructureProgressNextTurn.Value = _System.Infrastructure_PerTurn.ConstructionProgressNextTurn;
        
        int points = _System.Infrastructure_PerTurn.Infrastructure - _System.Infrastructure_PerTurn.InfrastructureUsed;
        InfrastructureAlert.Visible = points > 0;
        InfrastructureAlertPoints.SetTextWithReplace("$v", points.ToString());

        int taxLevel = _System.Economy_PerTurn.Tax;
        TaxPip_1.Visible = taxLevel == 1;
        TaxPip_2.Visible = taxLevel == 2;
        TaxPip_3.Visible = taxLevel == 3;

        IncomeBC.SetTextWithReplace("$val", _System.Economy_PerTurn.ToString_BC());
        IncomeInf.SetTextWithReplace("$val", _System.Economy_PerTurn.ToString_Influence());
        IncomeRes.SetTextWithReplace("$val", _System.Economy_PerTurn.ToString_Research());

        if (_System.Shipbuilding_PerTurn.Shipbuilding > 0)
        {
            Shipbuilding.Visible = true;
            NoShipbuilding.Visible = false;

            ShipbuildingLeft.Visible = _System.Shipbuilding_PerTurn.DesignIdx > 0;
            ShipbuildingRight.Visible = _System.Shipbuilding_PerTurn.DesignIdx < _System.Shipbuilding_PerTurn.DesignIdxMax - 1;
            //ShipbuildingIcon = _System.Shipbuilding_PerTurn.DesignCurrent.
            ShipbuildingName.SetTextWithReplace("$name", _System.Shipbuilding_PerTurn.DesignCurrent.DesignName);
            ShipbuildingIncome.SetTextWithReplace("$v", _System.Shipbuilding_PerTurn.ToString_Shipbuilding());
            ShipbuildingProgressTurns.SetTextWithReplace("$t", _System.Shipbuilding_PerTurn.ToString_Turns());
            ShipbuildingProgressCurrent.MaxValue = _System.Shipbuilding_PerTurn.ProgressMax;
            ShipbuildingProgressCurrent.Value = _System.Shipbuilding_PerTurn.ProgressCurrent;
            ShipbuildingProgressNextTurn.MaxValue = _System.Shipbuilding_PerTurn.ProgressMax;
            ShipbuildingProgressNextTurn.Value = _System.Shipbuilding_PerTurn.ProgressNextTurn;
        }
        else
        {
            Shipbuilding.Visible = false;
            NoShipbuilding.Visible = true;
        }
    }

    public void OnTax_0()
    {
        ActionTax.SetTax(_System, 0);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
        if (Game.self.Input.SelectedPlanet != null)
        {
            Game.self.GalaxyUI.PlanetInfo.Refresh(Game.self.Input.SelectedPlanet);
        }
    }

    public void OnTax_1()
    {
        ActionTax.SetTax(_System, 1);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
        if (Game.self.Input.SelectedPlanet != null)
        {
            Game.self.GalaxyUI.PlanetInfo.Refresh(Game.self.Input.SelectedPlanet);
        }
    }

    public void OnTax_2()
    {
        ActionTax.SetTax(_System, 2);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
        if (Game.self.Input.SelectedPlanet != null)
        {
            Game.self.GalaxyUI.PlanetInfo.Refresh(Game.self.Input.SelectedPlanet);
        }
    }

    public void OnShipPrevious()
    {
        DesignData design = _System._Player.GetDesignAtIdx(_System.Shipbuilding_PerTurn.DesignIdx - 1);
        ActionShipbuilding.ChangeShipTo(_System, design);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnShipNext()
    {
        DesignData design = _System._Player.GetDesignAtIdx(_System.Shipbuilding_PerTurn.DesignIdx + 1);
        ActionShipbuilding.ChangeShipTo(_System, design);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }
}