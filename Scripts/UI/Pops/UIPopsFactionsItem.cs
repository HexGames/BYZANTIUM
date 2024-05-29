using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Transactions;

public partial class UIPopsFactionsItem : Control
{
    [ExportCategory("Links")]
    private RichTextLabel Faction = null;
    private static string Faction_Original = "";
    private RichTextLabel Value = null;
    private static string Value_Original = "";

    //[ExportCategory("Runtime")]
    //[Export]
    //public SectorData _Sector = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        Faction = GetNode<RichTextLabel>("MarginContainer/Faction");
        if (Faction_Original.Length == 0) Faction_Original = Faction.Text;
        Value = GetNode<RichTextLabel>("MarginContainer/Value");
        if (Value_Original.Length == 0) Value_Original = Value.Text;
    }

    public void Refresh()
    {
        Faction.Text = Faction_Original.Replace("$faction", "FactionName").Replace("$player", "PlayerName");
        Value.Text = Value_Original.Replace("$value","xx").Replace("$perc", "yy");
    }
}