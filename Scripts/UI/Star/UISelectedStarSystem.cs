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
    public RichTextLabel Pops;
    private static string Pops_Original = "";
    [Export]
    public RichTextLabel Control;
    private static string Control_Original = "";

    //[Export]
    //public RichTextLabel PopsPub;
    //private static string PopsPub_Original = "";
    //[Export]
    //public RichTextLabel PopsPrv;
    //private static string PopsPrv_Original = "";
    //[Export]
    //public RichTextLabel PopsUnc;
    //private static string PopsUnc_Original = "";

    [Export]
    public RichTextLabel Factories;
    private static string Factories_Original = "";
    //[Export]
    //public RichTextLabel PrivateBusiness;
    //private static string PrivateBusiness_Original = "";
    [Export]
    public RichTextLabel DefenceBases;
    private static string DefenceBases_Original = "";

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
    public RichTextLabel Income;
    private static string Income_Original = "";
    [Export]
    public RichTextLabel Production;
    private static string Production_Original = "";

    [ExportCategory("Runtime")]
    [Export]
    public SystemData _System = null;

    public override void _Ready()
    {
        if (OwnerName_Original.Length == 0) OwnerName_Original = OwnerName.Text;

        if (Pops_Original.Length == 0) Pops_Original = Pops.Text;
        if (Control_Original.Length == 0) Control_Original = Control.Text;

        //if (PopsPub_Original.Length == 0) PopsPub_Original = PopsPub.Text;
        //if (PopsPrv_Original.Length == 0) PopsPrv_Original = PopsPrv.Text;
        //if (PopsUnc_Original.Length == 0) PopsUnc_Original = PopsUnc.Text;

        if (Factories_Original.Length == 0) Factories_Original = Factories.Text;
        //if (PrivateBusiness_Original.Length == 0) PrivateBusiness_Original = PrivateBusiness.Text;
        if (DefenceBases_Original.Length == 0) DefenceBases_Original = DefenceBases.Text;

        if (ControlResults_Original.Length == 0) ControlResults_Original = ControlResults.Text;
        if (MigrationResults_Original.Length == 0) MigrationResults_Original = MigrationResults.Text;
        if (TaxResults_Original.Length == 0) TaxResults_Original = TaxResults.Text;

        if (Income_Original.Length == 0) Income_Original = Income.Text;
        if (Production_Original.Length == 0) Production_Original = Production.Text;
    }

    public void Refresh(SystemData system)
    {
        _System = system;

        OwnerName.Text = OwnerName_Original.Replace("$name", _System._Player.PlayerName);
        OwnerColor.SelfModulate = Game.self.UILib.GetPlayerColor(_System._Player.PlayerID);

        Pops.Text = Pops_Original.Replace("$pops", _System.Pops_PerTurn.ToString_Pops());
        //Control.Text = Control_Original.Replace("$val", _System.Resources_PerTurn.GetPops().ToString_PopsUncontrolled());

        //PopsPub.Text = PopsPub_Original.Replace("$pops", _System.Resources_PerTurn.GetPops().ToString_PopsPublic());
        //PopsPrv.Text = PopsPrv_Original.Replace("$pops", _System.Resources_PerTurn.GetPops().ToString_PopsPrivate());
        //PopsUnc.Text = PopsUnc_Original.Replace("$pops", _System.Resources_PerTurn.GetPops().ToString_PopsUncontrolled());

        Factories.Text = Factories_Original.Replace("$val", _System.Buildings_PerTurn.ToString_Factories()).Replace("$max", _System.Buildings_PerTurn.ToString_FactoriesMax());
        //PrivateBusiness.Text = PrivateBusiness_Original.Replace("$val", _System.Resources_PerTurn.GetBuildings().ToString_PrivateBusinesses()).Replace("$max", _System.Resources_PerTurn.GetBuildings().ToString_PrivateBusinessesMax());
        DefenceBases.Text = DefenceBases_Original.Replace("$val", _System.Buildings_PerTurn.ToString_Bases()).Replace("$max", _System.Buildings_PerTurn.ToString_BasesMax());

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

        Income.Text = Income_Original.Replace("$val", Helper.ResValueToString(ResourcesWrapper.GetSystemBC(_System)));
        Production.Text = Production_Original.Replace("$prod", Helper.ResValueToString(ResourcesWrapper.GetSystemProduction(_System)));
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