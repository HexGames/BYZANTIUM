using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Transactions;

public partial class UIPopsControl : Control
{
    [ExportCategory("Links")]
    [Export]
    public RichTextLabel PlayerName = null;
    private string PlayerName_Original = "";
    [Export]
    public RichTextLabel Upkeep = null;
    private string Upkeep_Original = "";
    [Export]
    public RichTextLabel ControlledPops = null;
    private string ControlledPops_Original = "";
    [Export]
    public RichTextLabel IndependentPops = null;
    private string IndependentPops_Original = "";
    [Export]
    public RichTextLabel HostilePops = null;
    private string HostilePops_Original = "";

    [ExportCategory("Runtime")]
    [Export]
    public SectorData _Sector = null;
    [Export]
    public ColonyData _Colony = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        PlayerName_Original = PlayerName.Text;
        Upkeep_Original = Upkeep.Text;
        ControlledPops_Original = ControlledPops.Text;
        IndependentPops_Original = IndependentPops.Text;
        HostilePops_Original = HostilePops.Text;
    }

    public void Refresh(ColonyData colony)
    {
        _Colony = colony;
        _Sector = null;

        PlayerName.Text = PlayerName_Original.Replace("$player", _Colony._System._Sector._Player.PlayerName + "'s World");

        Upkeep.Text = Upkeep_Original.Replace("$value", _Colony.Resources_PerTurn.GetLimit("Authority").ToString_Used(true));

        ControlledPops.Text = ControlledPops_Original.Replace("$value", _Colony.Resources_PerTurn.GetPops().ToString_CPops());

        IndependentPops.Text = IndependentPops_Original.Replace("$value", _Colony.Resources_PerTurn.GetPops().ToString_IPops());

        HostilePops.Text = HostilePops_Original.Replace("$value", "0");
    }

    //public void Refresh(SectorData sector)
    //{
    //    _Colony = null;
    //    _Sector = sector;
    //
    //    PlayerName.Text = PlayerName_Original.Replace("$player", _Sector._Player.PlayerName + "'s Sector");
    //
    //    Upkeep.Text = Upkeep_Original.Replace("$value", _Sector.Resources_PerTurn.Get("Influence").String_Res_GetUpkeep());
    //
    //    ControlledPops.Text = ControlledPops_Original.Replace("$value", "xx");
    //
    //    IndependentPops.Text = IndependentPops_Original.Replace("$value", "yy");
    //
    //    HostilePops.Text = HostilePops_Original.Replace("$value", "0");
    //}
}