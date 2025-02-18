using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIDiplomacyBarItem : Control
{
    [ExportCategory("Links")]
    [Export]
    private Panel IconBg = null;
    private TextureRect Icon = null;
    private UIText Pops = null;
    private UIText Systems = null;
    private UIText Economy = null;
    private UIText Ships = null;

    [ExportCategory("Runtime")]
    [Export]
    public PlayerData _Player = null;

    public override void _Ready()
    {
        IconBg = GetNode<Panel>("Panel/VBoxContainer/IconBg");
        Icon = GetNode<TextureRect>("Panel/VBoxContainer/IconBg/Icon");
        Pops = GetNode<UIText>("Panel/VBoxContainer/Pops");
        Systems = GetNode<UIText>("Panel/VBoxContainer/Systems");
        Economy = GetNode<UIText>("Panel/VBoxContainer/Economy");
        Ships = GetNode<UIText>("Panel/VBoxContainer/Ships");
    }

    private bool Refreshed = false;
    public void NeedsRefresh()
    {
        Refreshed = false;
        if (Visible) Refresh(_Player);
    }

    public void Refresh(PlayerData player)
    {
        if (player == null) return;
        Visible = true;
        if (_Player == player && Refreshed) return;

        _Player = player;
        Refreshed = true;

        IconBg.SelfModulate = Game.self.UILib.GetPlayerColor(player.PlayerID);
        Icon.Texture = Game.self.Assets.GetTexture2D_Flag(player.PlayerName + ".png");

        Pops.SetTextWithReplace("$v", player.Stats_PerTurn.Pops.ToString());
        Systems.SetTextWithReplace("$v", player.Stats_PerTurn.Systems.ToString());
        Economy.SetTextWithReplace("$v", player.Stats_PerTurn.DistrictLevels.ToString());
        Ships.SetTextWithReplace("$v", player.Stats_PerTurn.FleetPower.ToString());
    }

    public void OnSelect()
    {
        //Game.self.Input.OnOpenDiplomacy(_Player);
    }
}