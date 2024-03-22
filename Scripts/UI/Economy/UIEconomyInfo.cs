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
    public Control Local;
    [Export]
    public UIEconomyInfoItem Pops;
    [Export]
    public UIEconomyInfoItem Growth;
    [Export]
    public UIEconomyInfoItem Energy;
    [Export]
    public UIEconomyInfoItem Minerals;
    [Export]
    public UIEconomyInfoItem Production;
    [Export]
    public UIEconomyInfoItem Shipbuilding;
    [Export]
    public UIEconomyInfoItem Trade;
    [Export]
    public UIEconomyInfoItem Research;
    [Export]
    public UIEconomyInfoItem Culture;
    [Export]
    public UIEconomyInfoItem Authority;
    [Export]
    public UIEconomyInfoItem Influence;
    [Export]
    public UIEconomyInfoItem BC;

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
        //Production.Visible = false;
        //Shipbuilding.Visible = false;

        Local.Visible = false;

        Energy.Refresh(_Sector.Resources_PerTurn.GetUsedPerTotalString("Energy"));
        Energy.Visible = true;

        Minerals.Refresh(_Sector.Resources_PerTurn.GetUsedPerTotalString("Minerals"));
        Minerals.Visible = true;

        Production.Refresh(_Sector.Resources_PerTurn.GetIncomeString("Production"));
        Production.Visible = true;

        Shipbuilding.Refresh(_Sector.Resources_PerTurn.GetIncomeString("Shipbuilding"));
        Shipbuilding.Visible = true;

        Trade.Refresh(_Sector.Resources_PerTurn.GetUsedPerTotalString("Trade"));
        Trade.Visible = true;

        Research.Refresh(_Sector.Resources_PerTurn.GetIncomeString("TechPoints"));
        Research.Visible = true;

        Culture.Refresh(_Sector.Resources_PerTurn.GetIncomeString("CulturePoints"));
        Culture.Visible = true;

        Authority.Refresh(_Sector.Resources_PerTurn.GetUsedPerTotalString("Authority"));
        Authority.Visible = true;

        Influence.Refresh(_Sector.Resources_PerTurn.GetUsedPerTotalString("Influence"));
        Influence.Visible = true;

        BC.Refresh(_Sector.Resources_PerTurn.GetIncomeString("BC"));
        BC.Visible = true;
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

        Energy.Refresh(_System.Resources_PerTurn.GetUsedPerTotalString("Energy"));
        Energy.Visible = true;

        Minerals.Refresh(_System.Resources_PerTurn.GetUsedPerTotalString("Minerals"));
        Minerals.Visible = true;

        Production.Refresh(_System.Resources_PerTurn.GetIncomeString("Production"));
        Production.Visible = true;

        Shipbuilding.Refresh(_System.Resources_PerTurn.GetIncomeString("Shipbuilding"));
        Shipbuilding.Visible = true;

        Trade.Refresh(_System.Resources_PerTurn.GetUsedPerTotalString("Trade"));
        Trade.Visible = true;

        Research.Refresh(_System.Resources_PerTurn.GetIncomeString("TechPoints"));
        Research.Visible = true;

        Culture.Refresh(_System.Resources_PerTurn.GetIncomeString("CulturePoints"));
        Culture.Visible = true;

        Authority.Refresh(_System.Resources_PerTurn.GetUsedPerTotalString("Authority"));
        Authority.Visible = true;

        Influence.Refresh(_System.Resources_PerTurn.GetUsedPerTotalString("Influence"));
        Influence.Visible = true;

        BC.Refresh(_System.Resources_PerTurn.GetIncomeString("BC"));
        BC.Visible = true;
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

        Pops.Refresh(_Colony.Resources_PerTurn.GetUsedPerTotalString("Pops", 1000));
        Pops.Visible = true;

        Growth.Refresh(_Colony.Resources_PerTurn.GetPercentString("Growth"));
        Growth.Visible = true;

        Local.Visible = true;

        Energy.Refresh(_Colony.Resources_PerTurn.GetUsedPerTotalString("Energy"));
        Energy.Visible = true;

        Minerals.Refresh(_Colony.Resources_PerTurn.GetUsedPerTotalString("Minerals"));
        Minerals.Visible = true;

        Production.Refresh(_Colony.Resources_PerTurn.GetIncomeString("Production"));
        Production.Visible = true;

        Shipbuilding.Refresh(_Colony.Resources_PerTurn.GetIncomeString("Shipbuilding"));
        Shipbuilding.Visible = true;

        Trade.Refresh(_Colony.Resources_PerTurn.GetUsedPerTotalString("Trade"));
        Trade.Visible = true;

        Research.Refresh(_Colony.Resources_PerTurn.GetIncomeString("TechPoints"));
        Research.Visible = true;

        Culture.Refresh(_Colony.Resources_PerTurn.GetIncomeString("CulturePoints"));
        Culture.Visible = true;

        Authority.Refresh(_Colony.Resources_PerTurn.GetUsedPerTotalString("Authority"));
        Authority.Visible = true;

        Influence.Refresh(_Colony.Resources_PerTurn.GetUsedPerTotalString("Influence"));
        Influence.Visible = true;

        BC.Refresh(_Colony.Resources_PerTurn.GetIncomeString("BC"));
        BC.Visible = true;
    }
}