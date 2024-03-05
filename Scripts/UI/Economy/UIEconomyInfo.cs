using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIEconomyInfo : Control
{
    [ExportCategory("Links")]
    [Export]
    public RichTextLabel Title;
    private string Title_Original;
    [Export]
    public Control NoLocal;
    [Export]
    public Control Local;
    [Export]
    public UIEconomyInfoItem Pops;
    [Export]
    public UIEconomyInfoItem Growth;
    [Export]
    public UIEconomyInfoItem Infrastructure;
    [Export]
    public UIEconomyInfoItem Prod;
    [Export]
    public UIEconomyInfoItem PrivateIndustry;
    [Export]
    public UIEconomyInfoItem Energy;
    [Export]
    public UIEconomyInfoItem Minerals;
    [Export]
    public UIEconomyInfoItem Treasury;
    [Export]
    public UIEconomyInfoItem Research;
    [Export]
    public UIEconomyInfoItem Culture;
    [Export]
    public UIEconomyInfoItem Authority;
    [Export]
    public UIEconomyInfoItem Influence;

    [ExportCategory("Runtime")]
    [Export]
    public SectorData _Sector = null;
    [Export]
    public SystemData _System = null;
    [Export]
    public ColonyData _Colony = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        Title_Original = Title.Text;
    }

    public void Refresh(SectorData sector)
    {
        _Colony = null;
        _System = null;
        _Sector = sector;

        Title.Text = Title_Original.Replace("$title", sector.SectorName + " Sector");
        Title.Visible = true;

        //Pops.Visible = false;
        //Growth.Visible = false;
        //Infrastructure.Visible = false;
        //Prod.Visible = false;
        //PrivateIndustry.Visible = false;

        Local.Visible = false;
        NoLocal.Visible = true;

        Energy.Refresh(_Sector.ResourcesPerTurn.GetUsedPerTotalString("Energy"));
        Energy.Visible = true;

        Minerals.Refresh(_Sector.ResourcesPerTurn.GetUsedPerTotalString("Minerals"));
        Minerals.Visible = true;

        Treasury.Refresh(_Sector.ResourcesPerTurn.GetIncomeString("BC"));
        Treasury.Visible = true;

        Research.Refresh(_Sector.ResourcesPerTurn.GetIncomeString("TechPoints"));
        Research.Visible = true;

        Culture.Refresh(_Sector.ResourcesPerTurn.GetIncomeString("CivicPoints"));
        Culture.Visible = true;

        Authority.Refresh(_Sector.ResourcesPerTurn.GetUsedPerTotalString("Authority"));
        Authority.Visible = true;

        Influence.Refresh(_Sector.ResourcesPerTurn.GetUsedPerTotalString("Influence"));
        Influence.Visible = true;
    }

    public void Refresh(SystemData system)
    {
        _Colony = null;
        _System = system;
        _Sector = null;

        Title.Text = Title_Original.Replace("$title", _System.Star.StarName + " System");
        Title.Visible = true;

        //Pops.Visible = false;
        //Growth.Visible = false;
        //Infrastructure.Visible = false;
        //Prod.Visible = false;
        //PrivateIndustry.Visible = false;

        Local.Visible = false;
        NoLocal.Visible = true;

        Energy.Refresh(_System.ResourcesPerTurn.GetUsedPerTotalString("Energy"));
        Energy.Visible = false;

        Minerals.Refresh(_System.ResourcesPerTurn.GetUsedPerTotalString("Minerals"));
        Minerals.Visible = false;

        Treasury.Refresh(_System.ResourcesPerTurn.GetIncomeString("BC"));
        Treasury.Visible = true;

        Research.Refresh(_System.ResourcesPerTurn.GetIncomeString("TechPoints"));
        Research.Visible = true;

        Culture.Refresh(_System.ResourcesPerTurn.GetIncomeString("CivicPoints"));
        Culture.Visible = true;

        Authority.Refresh(_System.ResourcesPerTurn.GetUsedPerTotalString("Authority"));
        Authority.Visible = true;

        Influence.Refresh(_System.ResourcesPerTurn.GetUsedPerTotalString("Influence"));
        Influence.Visible = true;
    }

    public void Refresh(ColonyData colony)
    {
        _Colony = colony;
        _System = null;
        _Sector = null;

        {
            Title.Text = Title_Original.Replace("$title", "Colony " + colony.ColonyName);
            Title.Visible = true;
        }

        Pops.Refresh(_Colony.ResourcesPerTurn.GetUsedPerTotalString("Pops", 1000));
        Pops.Visible = true;

        Growth.Refresh(_Colony.ResourcesPerTurn.GetPercentString("Growth"));
        Growth.Visible = true;

        Infrastructure.Refresh(_Colony.ResourcesPerTurn.GetPercentString("Infrastructure"));
        Infrastructure.Visible = true;

        Prod.Refresh(_Colony.ResourcesPerTurn.GetIncomeString("Prod"));
        Prod.Visible = true;

        PrivateIndustry.Refresh(_Colony.ResourcesPerTurn.GetPercentString("PrivateIndustry"));
        PrivateIndustry.Visible = true;

        Local.Visible = true;
        NoLocal.Visible = false;

        Energy.Refresh(_Colony.ResourcesPerTurn.GetUsedPerTotalString("Energy"));
        Energy.Visible = true;

        Minerals.Refresh(_Colony.ResourcesPerTurn.GetUsedPerTotalString("Minerals"));
        Minerals.Visible = true;

        Treasury.Refresh(_Colony.ResourcesPerTurn.GetIncomeString("BC"));
        Treasury.Visible = true;

        Research.Refresh(_Colony.ResourcesPerTurn.GetIncomeString("TechPoints"));
        Research.Visible = true;

        Culture.Refresh(_Colony.ResourcesPerTurn.GetIncomeString("CivicPoints"));
        Culture.Visible = true;

        Authority.Refresh(_Colony.ResourcesPerTurn.GetUsedPerTotalString("Authority"));
        Authority.Visible = true;

        Influence.Refresh(_Colony.ResourcesPerTurn.GetUsedPerTotalString("Influence"));
        Influence.Visible = true;
    }
}