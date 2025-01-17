using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIDiplomacyBar : Control
{
    [ExportCategory("Links")]
    [Export]
    private Array<UIDiplomacyBarItem> Empires = new Array<UIDiplomacyBarItem>();

    public void Refresh()
    {
        PlayerData humanPlayer = Game.self.HumanPlayer;

        // grow
        while (Empires.Count < 1 + humanPlayer.Relations.Count)
        {
            UIDiplomacyBarItem newItem = Empires[0].Duplicate(7) as UIDiplomacyBarItem;
            Empires[0].GetParent().AddChild(newItem);
            Empires.Add(newItem);
        }

        Empires[0].Refresh(humanPlayer);
        for (int idx = 1; idx < Empires.Count; idx++)
        {
            Empires[idx].Refresh(humanPlayer.Relations[idx - 1].GetOtherPlayer(humanPlayer));
        }
    }
}