using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Transactions;

public partial class UIPopsFactions : Control
{
    [ExportCategory("Links")]
    [Export]
    public RichTextLabel PlayerName = null;
    public string PlayerName_Original = "";
    [Export]
    public RichTextLabel Value = null;
    public string Value_Original = "";
    [Export]
    public Array<UIPopsFactionsItem> Factions = new Array<UIPopsFactionsItem>();

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
        Value_Original = Value.Text;
    }

    public void Refresh(ColonyData colony)
    {
        _Colony = colony;
        _Sector = null;

        for (int idx = 0; idx < Factions.Count; idx++)
        {
            Factions[idx].Visible = false;
        }

        PlayerName.Text = PlayerName_Original.Replace("$player", _Colony._System._Sector._Player.PlayerName);
        Value.Text = Value_Original.Replace("$perc", _Colony.Resources_PerTurn.GetLimit("Authority").ToString_Used(true));
    }
    public void RefreshFaction(int idx)
    {
        while (idx >= Factions.Count)
        {
            UIPopsFactionsItem newFactionInfo = Factions[0].Duplicate(7) as UIPopsFactionsItem;
            Factions[0].GetParent().AddChild(newFactionInfo);
            Factions.Add(newFactionInfo);
        }

        Factions[idx].Refresh();
        Factions[idx].Visible = true;
    }
}