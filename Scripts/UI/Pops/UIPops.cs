using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Transactions;

public partial class UIPops : Control
{
    [ExportCategory("Links")]
    [Export]
    public RichTextLabel Population = null;
    private string Population_Original = "";
    [Export]
    public RichTextLabel Growth = null;
    private string Growth_Original = "";
    [Export]
    public RichTextLabel Controlled = null;
    private string Controlled_Original = "";

    [ExportCategory("Runtime")]
    [Export]
    public SectorData _Sector = null;
    [Export]
    public ColonyData _Colony = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        Population_Original = Population.Text;
        Growth_Original = Growth.Text;
        Controlled_Original = Controlled.Text;
    }

    public void Refresh(ColonyData colony)
    {
        _Colony = colony;
        _Sector = null;

        Population.Text = Population_Original.Replace("$value", _Colony.Resources_PerTurn.GetPops().ToString_Pops());

        Growth.Text = Growth_Original.Replace("$value", _Colony.Resources_PerTurn.GetPops().ToString_TrueGrowth());

        Controlled.Text = Controlled_Original.Replace("$value", _Colony.Resources_PerTurn.GetPops().ToString_CPops());
    }

    //public void Refresh(SectorData sector)
    //{
    //    _Colony = null;
    //    _Sector = sector;
    //
    //    Population.Text = Population_Original.Replace("$value", "9999");
    //
    //    Growth.Text = Growth_Original.Replace("$value", "888");
    //
    //    Controlled.Text = Controlled_Original.Replace("$value", "xx");
    //}
}