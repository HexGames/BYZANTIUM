using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class PopsWrapper
{
    private static int TRADE_CAPACITY = 100;

    public int Pops = 0;
    public int PopsMax = 0;
    
    public int GrowthFromPops = 0;
    public int GrowthWaste = 0;
    public int GrowthOutgoingTrade = 0;
    public int GrowthOutgoingTradeCount = 0;
    public int GrowthIncommingTrade = 0;
    public int GrowthIncommingTradeCount = 0;
    public int GrowthTotal = 0;

    public int GrowthProgress = 0;
    public int GrowthProgressNextTurn = 0;
    public int GrowthProgressMax = 0;
    public int GrowthTurns = 0;
    public int PopsUnhappy = 0;
    public int PopsNeutral = 0;
    public int PopsHappy = 0;

    public SystemData _System;

    public PopsWrapper(SystemData system)
    {
        _System = system;
    }

    public void Clear()
    {
        Pops = 0;
        PopsMax = 0;

        GrowthFromPops = 0;
        GrowthWaste = 0;
        GrowthOutgoingTrade = 0;
        GrowthOutgoingTradeCount = 0;
        GrowthIncommingTrade = 0;
        GrowthIncommingTradeCount = 0;

        GrowthProgress = 0;
        GrowthProgressNextTurn = 0;
        GrowthProgressMax = 0;
        GrowthTurns = 0;
        PopsUnhappy = 0;
        PopsNeutral = 0;
        PopsHappy = 0;
    }

    public void RefreshBase()
    {
        Clear();

        Pops = _System.GetPopsCurrent();
        PopsMax = _System.Star.GetPopsMax();
        GrowthFromPops = 0;
        GrowthWaste = 0;
        for (int colonyIdx = 0; colonyIdx < _System.Colonies.Count; colonyIdx++)
        {
            int waste = 0;
            GrowthFromPops += _System.Colonies[colonyIdx].GetGrowth(out waste);
            GrowthWaste += waste;
        }
        GrowthTotal = GrowthFromPops + GrowthWaste;
    }

    public void RefreshOutgoingTrade()
    {
        GrowthOutgoingTrade = 0;

        List<DataBlock> trades = _System.GetTrades(true, "Growth");
        GrowthOutgoingTradeCount = trades.Count;

        if (trades.Count > 0)
        {
            int maxTradeQ = TRADE_CAPACITY * trades.Count;

            GrowthOutgoingTrade = Mathf.Max(maxTradeQ, GrowthWaste);
            GrowthWaste -= GrowthOutgoingTrade;

            if (GrowthOutgoingTrade < maxTradeQ)
            {
                int remaining = Mathf.Max(maxTradeQ - GrowthOutgoingTrade, GrowthTotal / 2);
                GrowthOutgoingTrade += remaining;
                GrowthTotal -= remaining;
            }
        }
    }

    public void RefreshIncomingTrade()
    {
        GrowthIncommingTrade = 0;

        List<DataBlock> trades = _System.GetTrades(false, "Growth");
        GrowthIncommingTradeCount = trades.Count;

        for (int idx = 0; idx < trades.Count; idx++)
        {
            SystemData otherSystem = _System._Player.GetSystem(trades[idx].GetSubValueS("OtherSystem"));
            GrowthIncommingTrade += otherSystem.Pops_PerTurn.GrowthOutgoingTrade / otherSystem.Pops_PerTurn.GrowthOutgoingTradeCount;
        }

        GrowthTotal += GrowthIncommingTrade;
    }

    public void Refresh()
    { 
        ColonyData colony = _System.GetGrowthColony();
        if (colony != null)
        {
            GrowthProgress = colony.GetGrowthProgress();
            GrowthProgressMax = 1000;
            GrowthProgressNextTurn = Mathf.Min(GrowthProgress + GrowthTotal, GrowthProgressMax);
            if (GrowthTotal > 0) GrowthTurns = ((GrowthProgressMax - GrowthProgress) + (GrowthTotal - 1)) / GrowthTotal;
            else GrowthTurns = 999;
        }
        else
        {
            GrowthProgress = 0;
            GrowthProgressMax = 1000; 
            GrowthTurns = 999;
        }

        for (int colonyIdx = 0; colonyIdx < _System.Colonies.Count; colonyIdx++)
        {
            colony = _System.Colonies[colonyIdx];
            for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
            {
                DistrictData district = colony.Districts[districtIdx];
                if (district.HasFullPop())
                {
                    int happiness = district.GetPop().Data.GetSubValueI("Happiness");
                    if (happiness > 70)
                    {
                        PopsHappy++;
                    }
                    else if (happiness < 30)
                    {
                        PopsUnhappy++;
                    }
                    else
                    {
                        PopsNeutral++;
                    }
                }
            }
        }
    }

    public string ToString_Pops() { return Pops.ToString(); }
    public string ToString_PopsMax() { return PopsMax.ToString(); }
    public string ToString_GrowthFromPops() { return Helper.ResValueToString(GrowthFromPops, 10, true); }
    public string ToString_GrowthWaste() { return Helper.ResValueToString(GrowthWaste, 10, true); }
    public string ToString_GrowthIncommingTrade() { return Helper.ResValueToString(GrowthIncommingTrade, 10, true); }
    public string ToString_GrowthOutgoingTrade() { return Helper.ResValueToString(GrowthOutgoingTrade, 10, true); }
    public string ToString_GrowthTotal() { return Helper.ResValueToString(GrowthTotal, 10, true); }
    public string ToString_GrowthTurns() { return GrowthTurns < 900 ? GrowthTurns.ToString() : "oo"; }
    public string ToString_Unhappy() { return PopsUnhappy.ToString(); }
    public string ToString_Neutral() { return PopsNeutral.ToString(); }
    public string ToString_Happy() { return PopsHappy.ToString(); }
}
