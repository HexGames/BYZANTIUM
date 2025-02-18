using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class SystemData : Node
{
    [ExportCategory("SystemParent")]
    [Export]
    public PlayerData _Player = null;

    [ExportCategory("SystemRawData")]
    [Export]
    public DataBlock Data = null;

    public string SystemName { get { return Star.StarName; } }

    [Export]
    public bool Capital = false;
    [Export]
    public DataBlock Resources = null;
    [Export]
    public DataBlock Trades = null;

    //[ExportCategory("SystemData-Actions")]
    //[Export]
    //public DataBlock ActionBuild = null;

    [ExportCategory("SystemData-Links")]
    [Export]
    public Array<ColonyData> Colonies = new Array<ColonyData>();
    [Export]
    public StarData Star = null;

    // resources
    //public ResourcesWrapper Resources_PerTurn = null;
    // pops
    public PopsWrapper Pops_PerTurn = null;
    // control
    public ControlWrapper Control_PerTurn = null;
    // infrastructure
    //public InfrastructureWrapper Infrastructure_PerTurn = null;
    // economy
    public SystemEconomyWrapper Economy_PerTurn = null;
    // shipbuilding
    public ShipbuildingWrapper Shipbuilding_PerTurn = null;

    // invest
    public DistrictData DistrictToInvest_PerTurn = null;

    // actions
    public List<ActionEconomyColonize> ActionEconomyColonize_PerTurn = new List<ActionEconomyColonize>();

    // --------------------------------------------------------------------------------------------
    public void Init_DistrictData()
    {
        for (int colonyIdx = 0; colonyIdx < Colonies.Count; colonyIdx++)
        {
            ColonyData colony = Colonies[colonyIdx];
            colony.Init_DistrictData();
        }
    }

    public void Init_Resources()
    {
        Pops_PerTurn = new PopsWrapper(this);
        Control_PerTurn = new ControlWrapper(this);
        //Infrastructure_PerTurn = new InfrastructureWrapper(this);
        Economy_PerTurn = new SystemEconomyWrapper(this);
        Shipbuilding_PerTurn = new ShipbuildingWrapper(this);

        for (int colonyIdx = 0; colonyIdx < Colonies.Count; colonyIdx++)
        {
            ColonyData colony = Colonies[colonyIdx];
            colony.Init_Resources();
        }
    }
    // --------------------------------------------------------------------------------------------
    public void RefreshSystemDistricts_1()
    {
        for (int colonyIdx = 0; colonyIdx < Colonies.Count; colonyIdx++)
        {
            ColonyData colony = Colonies[colonyIdx];

            for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
            {
                DistrictData district = colony.Districts[districtIdx];
                district.Economy_PerTurn.RefreshBase_1_1();
                //if (district.IsPrivate() && (DistrictToInvest_PerTurn == null || DistrictToInvest_PerTurn.Economy_PerTurn.ReinvestProgress < district.Economy_PerTurn.ReinvestProgress))
                //{
                //    DistrictToInvest_PerTurn = district;
                //}
            }
        }

        for (int colonyIdx = 0; colonyIdx < Colonies.Count; colonyIdx++)
        {
            ColonyData colony = Colonies[colonyIdx];

            for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
            {
                DistrictData district = colony.Districts[districtIdx];

                for (int otherColonyIdx = 0; otherColonyIdx < Colonies.Count; otherColonyIdx++)
                {
                    ColonyData otherColony = Colonies[otherColonyIdx];

                    for (int otherDistrictIdx = 0; otherDistrictIdx < otherColony.Districts.Count; otherDistrictIdx++)
                    {
                        DistrictData otherDistrict = otherColony.Districts[otherDistrictIdx];
                        //if (otherDistrict.Economy_PerTurn.Resource == district.Economy_PerTurn.Resource)
                        //{
                        //    district.Economy_PerTurn.AddBonusFromSystem_1_2(otherDistrict.Economy_PerTurn.Production_BonusToSystem);
                        //}
                    }
                }
            }
        }

        for (int colonyIdx = 0; colonyIdx < Colonies.Count; colonyIdx++)
        {
            ColonyData colony = Colonies[colonyIdx];

            for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
            {
                DistrictData district = colony.Districts[districtIdx];
                district.Economy_PerTurn.RefreshFinal_1_3();
            }
        }
    }

    public void RefreshInvest_2()
    {
        if (DistrictToInvest_PerTurn == null)
            return;

        int reinvest = 0;
        for (int colonyIdx = 0; colonyIdx < Colonies.Count; colonyIdx++)
        {
            ColonyData colony = Colonies[colonyIdx];

            for (int districtIdx = 0; districtIdx < colony.Districts.Count; districtIdx++)
            {
                DistrictData district = colony.Districts[districtIdx];
                //reinvest += district.Economy_PerTurn.ReinvestToSystem;
            }
        }

        //DistrictToInvest_PerTurn.Economy_PerTurn.ReinvestFromSystem = reinvest;
        //DistrictToInvest_PerTurn.Economy_PerTurn.ReinvestPerTurn += reinvest;
        //DistrictToInvest_PerTurn.Economy_PerTurn.RecalculateReinvestTurns();
    }


    // --------------------------------------------------------------------------------------------
    public bool IsConstructionQueueBusy()
    {
        return true;
    }

    // --------------------------------------------------------------------------------------------
    public int GetPopsCurrent()
    {
        int pops = 0;
        //for (int colonyIdx = 0; colonyIdx < Colonies.Count; colonyIdx++)
        //{
        //    for (int districtIdx = 0; districtIdx < Colonies[colonyIdx].Districts.Count; districtIdx++)
        //    {
        //        if (Colonies[colonyIdx].Districts[districtIdx].Pop.Data.GetSubValueI("Pop", "GrowthProgress") == 1000)
        //        {
        //            pops++;
        //        }
        //    }
        //}
        return pops;
    }
    // --------------------------------------------------------------------------------------------
    public int GetTaxPerc()
    {
        return (Data.GetSubValueI("ActionTax", "Tax")) * 25;
    }

    // --------------------------------------------------------------------------------------------
    public ColonyData GetColony(string colony)
    {
        for (int idx = 0; idx < Colonies.Count; idx++)
        {
            if (Colonies[idx].ColonyName == colony)
            {
                return Colonies[idx];
            }
        }

        return null;
    }

    // --------------------------------------------------------------------------------------------
    public ColonyData GetGrowthColony()
    {
        return GetColony(Data.GetSubValueS("ActionGrowth", "FocusColony"));
    }

    // --------------------------------------------------------------------------------------------
    public List<DataBlock> GetTrades(bool outgoing, string resource)
    {
        List<DataBlock> ret = new List<DataBlock>();

        Array<DataBlock> trades = Trades.GetSubs();
        for (int idx = 0; idx < trades.Count; idx++)
        {
            if (trades[idx].GetSubValueS("Resource") == resource && ((trades[idx].HasSub("Outgoing") && outgoing) || ((trades[idx].HasSub("Incoming") && outgoing == false))))
            {
                ret.Add(trades[idx]);
            }
        }

        return ret;
    }

    // --------------------------------------------------------------------------------------------
    public int GetNumberOfPrivateDistricts()
    {
        int privateDistricts = 0;
        for (int colonyIdx = 0; colonyIdx < Colonies.Count; colonyIdx++)
        {
            for (int districtIdx = 0; districtIdx < Colonies[colonyIdx].Districts.Count; districtIdx++)
            {
                if (Colonies[colonyIdx].Districts[districtIdx].IsPrivate())
                {
                    privateDistricts++;
                }
            }
        }
        return privateDistricts;
    }   
}
