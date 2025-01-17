using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIDiplomacyWindow : Control
{
    [ExportCategory("Links")]
    [Export]
    private UIText TitleLabel = null;
    [Export]
    private Panel IconBg = null;
    [Export]
    private TextureRect Icon = null;

    [Export]
    private UIText PopsValue = null;
    [Export]
    private UIText SystemsValue = null;
    [Export]
    private UIText EconomyValue = null;
    [Export]
    private UIText ShipsValue = null;

    [Export]
    private Control Treaties = null;
    [Export]
    private Control NoTreaties = null;


    [ExportCategory("Runtime")]
    [Export]
    public PlayerData _Player = null;

    public void Refresh(PlayerData player)
    {
        _Player = player;
        RelationData relation = Game.self.Map.Data.GetRelation(Game.self.HumanPlayer, _Player);

        TitleLabel.SetTextWithReplace("$name", player.PlayerName);

        IconBg.SelfModulate = Game.self.UILib.GetPlayerColor(player.PlayerID);
        Icon.Texture = Game.self.Assets.GetTexture2D_Flag(player.PlayerName + ".png");

        PopsValue.SetTextWithReplace("$v", player.Stats_PerTurn.Pops.ToString());
        SystemsValue.SetTextWithReplace("$v", player.Stats_PerTurn.Systems.ToString());
        EconomyValue.SetTextWithReplace("$v", player.Stats_PerTurn.DistrictLevels.ToString());
        ShipsValue.SetTextWithReplace("$v", player.Stats_PerTurn.FleetPower.ToString());

        if (relation != null)
        {
            Treaties.Visible = true;
            NoTreaties.Visible = false;
        }
        else
        {
            Treaties.Visible = false;
            NoTreaties.Visible = true;
        }
    }

    public void OnCloseWindow()
    {
        Game.self.Input.OnCloseDiplomacy();
    }
}