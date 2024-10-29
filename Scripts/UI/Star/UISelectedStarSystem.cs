using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISelectedStarSystem : Control
{
    [ExportCategory("Links")]
    [Export]
    public RichTextLabel OwnerName;
    private static string OwnerName_Original = "";
    [Export]
    public TextureRect OwnerColor;

    [Export]
    public UIText Pops;
    [Export]
    public UIText Factories;
    [Export]
    public UIText DefenceBases;

    [Export]
    public UIText Control;
    [Export]
    public UIText ControlChange;
    [Export]
    public UIText Corruption;
    [Export]
    public UIText CorruptionChange;
    [Export]
    public UIText Happiness;
    [Export]
    public UIText HappinessChange;

    [Export]
    public UIText Wealth;
    [Export]
    public UIText WealthChange;
    [Export]
    public UIText Inequality;
    [Export]
    public UIText InequalityChange;

    [Export]
    public Array<Button> EconomyBtns;
    [Export]
    public Array<Control> EconomyPips;
    [Export]
    public Array<UITooltipTrigger> EconomyTooltips;
    [Export]
    public UIText EconomyEffect;

    [Export]
    public Array<Button> StateBtns;
    [Export]
    public Array<Control> StatePips;
    [Export]
    public Array<UITooltipTrigger> StateTooltips;
    [Export]
    public UIText StateEffect;

    [Export]
    public Array<Button> SocialBtns;
    [Export]
    public Array<Control> SocialPips;
    [Export]
    public Array<UITooltipTrigger> SocialTooltips;
    [Export]
    public UIText SocialEffect;

    [Export]
    public Array<Button> MigrationBtns;
    [Export]
    public Array<Control> MigrationPips;
    [Export]
    public Array<UITooltipTrigger> MigrationTooltips;
    [Export]
    public UIText MigrationEffect;

    [Export]
    public UIText IncomeBCPerPop;
    [Export]
    public UIText IncomeInfluencePerPop;
    [Export]
    public UIText IncomeProductionPerPop;

    [Export]
    public UIText IncomeBC;
    [Export]
    public UIText IncomeInfluence;
    [Export]
    public UIText IncomeProduction;

    [ExportCategory("Runtime")]
    [Export]
    public SystemData _System = null;

    public override void _Ready()
    {
        if (OwnerName_Original.Length == 0) OwnerName_Original = OwnerName.Text;
    }

    public void Refresh(SystemData system)
    {
        _System = system;

        OwnerName.Text = OwnerName_Original.Replace("$name", _System._Player.PlayerName);
        OwnerColor.SelfModulate = Game.self.UILib.GetPlayerColor(_System._Player.PlayerID);

        Pops.Text = Pops.Original.Replace("$pops", _System.Pops_PerTurn.ToString_Pops());
        Factories.Text = Factories.Original.Replace("$val", _System.Buildings_PerTurn.ToString_Factories()).Replace("$max", _System.Buildings_PerTurn.ToString_FactoriesMax());
        DefenceBases.Text = DefenceBases.Original.Replace("$val", _System.Buildings_PerTurn.ToString_Bases()).Replace("$max", _System.Buildings_PerTurn.ToString_BasesMax());

        Control.Text = Control.Original.Replace("$val", _System.Control_PerTurn.ToString_Control());
        if (_System.Resources_PerTurn.SpecialIncome.Control > 0) ControlChange.Text = ControlChange.Original.Replace("$v", Helper.GetColorPrefix_Good() + _System.Resources_PerTurn.SpecialIncome.ToString_Control() + Helper.GetColorSufix());
        else if (_System.Resources_PerTurn.SpecialIncome.Control < 0) ControlChange.Text = ControlChange.Original.Replace("$v", Helper.GetColorPrefix_Bad() + _System.Resources_PerTurn.SpecialIncome.ToString_Control() + Helper.GetColorSufix());
        else ControlChange.Text = "";

        Corruption.Text = Corruption.Original.Replace("$val", _System.Control_PerTurn.ToString_Corruption());
        if (_System.Resources_PerTurn.SpecialIncome.Corruption < 0) CorruptionChange.Text = CorruptionChange.Original.Replace("$v", Helper.GetColorPrefix_Good() + _System.Resources_PerTurn.SpecialIncome.ToString_Corruption() + Helper.GetColorSufix());
        else if (_System.Resources_PerTurn.SpecialIncome.Corruption > 0) CorruptionChange.Text = CorruptionChange.Original.Replace("$v", Helper.GetColorPrefix_Bad() + _System.Resources_PerTurn.SpecialIncome.ToString_Corruption() + Helper.GetColorSufix());
        else CorruptionChange.Text = "";

        Happiness.Text = Happiness.Original.Replace("$val", _System.Control_PerTurn.ToString_Happiness());
        if (_System.Resources_PerTurn.SpecialIncome.Happiness > 0) HappinessChange.Text = HappinessChange.Original.Replace("$v", Helper.GetColorPrefix_Good() + _System.Resources_PerTurn.SpecialIncome.ToString_Happiness() + Helper.GetColorSufix());
        else if (_System.Resources_PerTurn.SpecialIncome.Happiness < 0) HappinessChange.Text = HappinessChange.Original.Replace("$v", Helper.GetColorPrefix_Bad() + _System.Resources_PerTurn.SpecialIncome.ToString_Happiness() + Helper.GetColorSufix());
        else HappinessChange.Text = "";

        Wealth.Text = Wealth.Original.Replace("$val", _System.Control_PerTurn.ToString_Wealth());
        if (_System.Resources_PerTurn.SpecialIncome.Wealth > 0) WealthChange.Text = WealthChange.Original.Replace("$v", Helper.GetColorPrefix_Good() + _System.Resources_PerTurn.SpecialIncome.ToString_Wealth() + Helper.GetColorSufix());
        else if (_System.Resources_PerTurn.SpecialIncome.Wealth < 0) WealthChange.Text = WealthChange.Original.Replace("$v", Helper.GetColorPrefix_Bad() + _System.Resources_PerTurn.SpecialIncome.ToString_Wealth() + Helper.GetColorSufix());
        else WealthChange.Text = "";

        Inequality.Text = Inequality.Original.Replace("$val", _System.Control_PerTurn.ToString_Inequality());
        if (_System.Resources_PerTurn.SpecialIncome.Inequality < 0) InequalityChange.Text = InequalityChange.Original.Replace("$v", Helper.GetColorPrefix_Good() + _System.Resources_PerTurn.SpecialIncome.ToString_Inequality() + Helper.GetColorSufix());
        else if (_System.Resources_PerTurn.SpecialIncome.Inequality > 0) InequalityChange.Text = InequalityChange.Original.Replace("$v", Helper.GetColorPrefix_Bad() + _System.Resources_PerTurn.SpecialIncome.ToString_Inequality() + Helper.GetColorSufix());
        else InequalityChange.Text = "";

        // economy
        DefEffectWrapper currentEffectDef = _System.Control_PerTurn.GetEconomyEffect();
        for (int idx = 0; idx < EconomyBtns.Count; idx++) EconomyBtns[idx].Disabled = !ActionFocus.CanSetControlAt(system, "Economy", idx);
        for (int idx = 0; idx < EconomyPips.Count; idx++) EconomyPips[idx].Visible = (idx == _System.Control_PerTurn.Economy_Level);
        for (int idx = 0; idx < EconomyTooltips.Count; idx++)
        {
            DefEffectWrapper effectDef = Game.self.Def.GetEffectInfo("Economy_" + idx.ToString());
            if (effectDef != null)
            {
                EconomyTooltips[idx].Title = effectDef.Name;
                EconomyTooltips[idx].Row_1 = effectDef.Res_PerSession.ToString_Long();
            }
            else
            {
                EconomyTooltips[idx].Title = "No Effect for Economy_" + idx.ToString();
            }
        }
        if (currentEffectDef != null)
        {
            EconomyEffect.Text = EconomyEffect.Original.Replace( "$value", currentEffectDef.Name);
        }
        else
        {
            EconomyEffect.Text = EconomyEffect.Original.Replace("", currentEffectDef.Name);
        }

        // state
        currentEffectDef = _System.Control_PerTurn.GetStateEffect();
        for (int idx = 0; idx < StateBtns.Count; idx++) StateBtns[idx].Disabled = !ActionFocus.CanSetControlAt(system, "State", idx);
        for (int idx = 0; idx < StatePips.Count; idx++) StatePips[idx].Visible = (idx == _System.Control_PerTurn.State_Level);
        for (int idx = 0; idx < StateTooltips.Count; idx++)
        {
            DefEffectWrapper effectDef = Game.self.Def.GetEffectInfo("State_" + idx.ToString());
            if (effectDef != null)
            {
                StateTooltips[idx].Title = effectDef.Name;
                StateTooltips[idx].Row_1 = effectDef.Res_PerSession.ToString_Long();
            }
            else
            {
                StateTooltips[idx].Title = "No Effect for State_" + idx.ToString();
            }
        }
        if (currentEffectDef != null)
        {
            StateEffect.Text = StateEffect.Original.Replace("$value", currentEffectDef.Name);
        }
        else
        {
            StateEffect.Text = StateEffect.Original.Replace("", currentEffectDef.Name);
        }

        // social
        currentEffectDef = _System.Control_PerTurn.GetSocialEffect();
        for (int idx = 0; idx < SocialBtns.Count; idx++) SocialBtns[idx].Disabled = !ActionFocus.CanSetControlAt(system, "Social", idx);
        for (int idx = 0; idx < SocialPips.Count; idx++) SocialPips[idx].Visible = (idx == _System.Control_PerTurn.Social_Level);
        for (int idx = 0; idx < SocialTooltips.Count; idx++)
        {
            DefEffectWrapper effectDef = Game.self.Def.GetEffectInfo("Social_" + idx.ToString());
            if (effectDef != null)
            {
                SocialTooltips[idx].Title = effectDef.Name;
                SocialTooltips[idx].Row_1 = effectDef.Res_PerSession.ToString_Long();
            }
            else
            {
                SocialTooltips[idx].Title = "No Effect for Social_" + idx.ToString();
            }
        }
        if (currentEffectDef != null)
        {
            SocialEffect.Text = SocialEffect.Original.Replace("$value", currentEffectDef.Name);
        }
        else
        {
            SocialEffect.Text = SocialEffect.Original.Replace("", currentEffectDef.Name);
        }

        // migration
        currentEffectDef = _System.Control_PerTurn.GetMigrationEffect();
        //for (int idx = 0; idx < MigrationBtns.Count; idx++) MigrationBtns[idx].Disabled = !ActionFocus.CanSetControlAt(system, "Migration", idx);
        for (int idx = 0; idx < MigrationPips.Count; idx++) MigrationPips[idx].Visible = (idx == _System.Control_PerTurn.Migration_Level);
        for (int idx = 0; idx < MigrationTooltips.Count; idx++)
        {
            DefEffectWrapper effectDef = Game.self.Def.GetEffectInfo("Migration_" + idx.ToString());
            if (effectDef != null)
            {
                MigrationTooltips[idx].Title = effectDef.Name;
                MigrationTooltips[idx].Row_1 = effectDef.Res_PerSession.ToString_Long();
            }
            else
            {
                MigrationTooltips[idx].Title = "No Effect for Migration_" + idx.ToString();
            }
        }
        if (currentEffectDef != null)
        {
            MigrationEffect.Text = MigrationEffect.Original.Replace("$value", currentEffectDef.Name);
        }
        else
        {
            MigrationEffect.Text = MigrationEffect.Original.Replace("", currentEffectDef.Name);
        }

        IncomeBCPerPop.Text = IncomeBCPerPop.Original.Replace("$val", _System.Resources_PerTurn.GetIncome("BC").ToString_PerPop());
        IncomeInfluencePerPop.Text = IncomeInfluencePerPop.Original.Replace("$val", _System.Resources_PerTurn.GetIncome("Influence").ToString_PerPop());
        IncomeProductionPerPop.Text = IncomeProductionPerPop.Original.Replace("$val", _System.Resources_PerTurn.GetIncome("Production").ToString_PerPop());


        IncomeBC.Text = IncomeBC.Original.Replace("$val", _System.Resources_PerTurn.GetIncome("BC").ToString_IncomeTotal(_System));
        IncomeInfluence.Text = IncomeInfluence.Original.Replace("$val", _System.Resources_PerTurn.GetIncome("Influence").ToString_IncomeTotal(_System));
        IncomeProduction.Text = IncomeProduction.Original.Replace("$val", _System.Resources_PerTurn.GetIncome("Production").ToString_IncomeTotal(_System));

        //PrivateBusiness.Text = PrivateBusiness_Original.Replace("$val", _System.Resources_PerTurn.GetBuildings().ToString_PrivateBusinesses()).Replace("$max", _System.Resources_PerTurn.GetBuildings().ToString_PrivateBusinessesMax());

        //ControlResults.Text = ControlResults_Original;
        //
        //if (_System._Player.Systems.Count >= 2)
        //{
        //    MigrationBg.Visible = true;
        //    MigrationResults.Text = MigrationResults_Original;
        //}
        //else
        //{
        //    MigrationBg.Visible = false;
        //}
        //
        //TaxResults.Text = TaxResults_Original;
        //
        //IncomeBC.Text = IncomeBC_Original.Replace("$val", Helper.ResValueToString(ResourcesWrapper.GetSystemBC(_System), 10, true));
        //IncomeInfluence.Text = IncomeBC_Original.Replace("$val", Helper.ResValueToString(ResourcesWrapper.GetSystemInfluence(_System), 10, true));
        //IncomeProduction.Text = IncomeProduction_Original.Replace("$val", Helper.ResValueToString(ResourcesWrapper.GetSystemProduction(_System), 10, true));
    }

    public void OnEconomy_0()
    {
        ActionFocus.SetControlAt(_System, "Economy", 0);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnEconomy_1()
    {
        ActionFocus.SetControlAt(_System, "Economy", 1);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnEconomy_2()
    {
        ActionFocus.SetControlAt(_System, "Economy", 2);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnEconomy_3()
    {
        ActionFocus.SetControlAt(_System, "Economy", 3);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnEconomy_4()
    {
        ActionFocus.SetControlAt(_System, "Economy", 4);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnState_0()
    {
        ActionFocus.SetControlAt(_System, "State", 0);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnState_1()
    {
        ActionFocus.SetControlAt(_System, "State", 1);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnState_2()
    {
        ActionFocus.SetControlAt(_System, "State", 2);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnState_3()
    {
        ActionFocus.SetControlAt(_System, "State", 3);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnState_4()
    {
        ActionFocus.SetControlAt(_System, "State", 4);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnSocial_0()
    {
        ActionFocus.SetControlAt(_System, "Social", 0);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnSocial_1()
    {
        ActionFocus.SetControlAt(_System, "Social", 1);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnSocial_2()
    {
        ActionFocus.SetControlAt(_System, "Social", 2);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnSocial_3()
    {
        ActionFocus.SetControlAt(_System, "Social", 3);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnSocial_4()
    {
        ActionFocus.SetControlAt(_System, "Social", 4);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnMigration_0()
    {
        ActionFocus.SetControlAt(_System, "Migration", 0);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnMigration_1()
    {
        ActionFocus.SetControlAt(_System, "Migration", 1);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnMigration_2()
    {
        ActionFocus.SetControlAt(_System, "Migration", 2);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnMigration_3()
    {
        ActionFocus.SetControlAt(_System, "Migration", 3);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }

    public void OnMigration_4()
    {
        ActionFocus.SetControlAt(_System, "Migration", 4);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }
    public void OnMigration_5()
    {
        ActionFocus.SetControlAt(_System, "Migration", 5);
        Game.self.GalaxyUI.SystemInfo.Refresh(_System.Star);
    }
}