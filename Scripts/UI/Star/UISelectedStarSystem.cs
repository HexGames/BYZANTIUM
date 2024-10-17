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
    public UIText Corruption;
    [Export]
    public UIText Happiness;

    [Export]
    public UIText Wealth;
    [Export]
    public UIText Inquality;

    [Export]
    public RichTextLabel ControlResults;
    private static string ControlResults_Original = "";
    [Export]
    public Control MigrationBg;
    [Export]
    public RichTextLabel MigrationResults;
    private static string MigrationResults_Original = "";
    [Export]
    public RichTextLabel TaxResults;
    private static string TaxResults_Original = "";

    [Export]
    public RichTextLabel IncomeBC;
    private static string IncomeBC_Original = "";
    [Export]
    public RichTextLabel IncomeInfluence;
    private static string IncomeInfluence_Original = "";
    [Export]
    public RichTextLabel IncomeProduction;
    private static string IncomeProduction_Original = "";

    [ExportCategory("Runtime")]
    [Export]
    public SystemData _System = null;

    public override void _Ready()
    {
        if (OwnerName_Original.Length == 0) OwnerName_Original = OwnerName.Text;

        if (ControlResults_Original.Length == 0) ControlResults_Original = ControlResults.Text;
        if (MigrationResults_Original.Length == 0) MigrationResults_Original = MigrationResults.Text;
        if (TaxResults_Original.Length == 0) TaxResults_Original = TaxResults.Text;

        if (IncomeBC_Original.Length == 0) IncomeBC_Original = IncomeBC.Text;
        if (IncomeInfluence_Original.Length == 0) IncomeInfluence_Original = IncomeInfluence.Text;
        if (IncomeProduction_Original.Length == 0) IncomeProduction_Original = IncomeProduction.Text;
    }

    public void Refresh(SystemData system)
    {
        _System = system;

        OwnerName.Text = OwnerName_Original.Replace("$name", _System._Player.PlayerName);
        OwnerColor.SelfModulate = Game.self.UILib.GetPlayerColor(_System._Player.PlayerID);

        Pops.Text = Pops.Original.Replace("$pops", _System.Pops_PerTurn.ToString_Pops());
        Factories.Text = Factories.Original.Replace("$val", _System.Buildings_PerTurn.ToString_Factories()).Replace("$max", _System.Buildings_PerTurn.ToString_FactoriesMax());
        DefenceBases.Text = DefenceBases.Original.Replace("$val", _System.Buildings_PerTurn.ToString_Bases()).Replace("$max", _System.Buildings_PerTurn.ToString_BasesMax());

        Control.Text = Control.Original.Replace("$val", _System.Pops_PerTurn.ToString_Pops());
        Corruption.Text = Corruption.Original.Replace("$val", _System.Pops_PerTurn.ToString_Pops());
        Happiness.Text = Happiness.Original.Replace("$val", _System.Pops_PerTurn.ToString_Pops());

        Wealth.Text = Wealth.Original.Replace("$val", _System.Pops_PerTurn.ToString_Pops());
        Inquality.Text = Inquality.Original.Replace("$val", _System.Pops_PerTurn.ToString_Pops());

        //PrivateBusiness.Text = PrivateBusiness_Original.Replace("$val", _System.Resources_PerTurn.GetBuildings().ToString_PrivateBusinesses()).Replace("$max", _System.Resources_PerTurn.GetBuildings().ToString_PrivateBusinessesMax());

        ControlResults.Text = ControlResults_Original;

        if (_System._Player.Systems.Count >= 2)
        {
            MigrationBg.Visible = true;
            MigrationResults.Text = MigrationResults_Original;
        }
        else
        {
            MigrationBg.Visible = false;
        }

        TaxResults.Text = TaxResults_Original;

        IncomeBC.Text = IncomeBC_Original.Replace("$val", Helper.ResValueToString(ResourcesWrapper.GetSystemBC(_System), 10, true));
        IncomeInfluence.Text = IncomeBC_Original.Replace("$val", Helper.ResValueToString(ResourcesWrapper.GetSystemInfluence(_System), 10, true));
        IncomeProduction.Text = IncomeProduction_Original.Replace("$val", Helper.ResValueToString(ResourcesWrapper.GetSystemProduction(_System), 10, true));
    }

    public void OnControl_1()
    {
    }

    public void OnControl_2()
    {
    }

    public void OnControl_3()
    {
    }

    public void OnControl_4()
    {
    }

    public void OnControl_5()
    {
    }

    public void OnMigration_1()
    {
    }

    public void OnMigration_2()
    {
    }

    public void OnMigration_3()
    {
    }

    public void OnMigration_4()
    {
    }

    public void OnMigration_5()
    {
    }

    public void OnTax_1()
    {
    }

    public void OnTax_2()
    {
    }

    public void OnTax_3()
    {
    }

    public void OnTax_4()
    {
    }

    public void OnTax_5()
    {
    }
}