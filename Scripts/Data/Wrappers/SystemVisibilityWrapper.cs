using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

public class SystemVisibilityWrapper
{
    public List<PlayerData> UncoveredBy = new List<PlayerData>();
    public List<PlayerData> VisibleBy = new List<PlayerData>();

    StarData _Star;
    DataBlock _VisibilityData;

    public SystemVisibilityWrapper(StarData star)
    {
        _Star = star;
        _VisibilityData = star.Data.GetSub("Visibility");
    }

    public void Clear()
    {
        UncoveredBy.Clear();
        VisibleBy.Clear();
    }

    public void Refresh()
    {
        Clear();

        MapData mapData = Game.self.Map.Data;
        Array<DataBlock> visibilityDataItems = _VisibilityData.GetSubs();
        for (int idx = 0; idx < visibilityDataItems.Count; idx++)
        {
            UncoveredBy.Add(mapData.GetPlayer(visibilityDataItems[0].Name));
        }
    }

    public void SetAsUncovered(PlayerData player)
    {
        if (UncoveredBy.Contains(player) == false)
        {
            UncoveredBy.Add(player);
            Data.AddData(_VisibilityData, player.PlayerName, Game.self.Def);
        }
    }

    public void SetAsVisibleForThisTurn(PlayerData player)
    {
        if (VisibleBy.Contains(player) == false)
        {
            VisibleBy.Add(player);
            SetAsUncovered(player);
        }
    }

    public bool IsVisibleBy(PlayerData player)
    {
        return VisibleBy.Contains(player);
    }

    public bool IsUncoveredBy(PlayerData player)
    {
        return UncoveredBy.Contains(player);
    }

}
