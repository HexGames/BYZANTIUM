using Godot;
using System.Collections.Generic;
using System.Reflection.Metadata;

public class DefBenefitWrapper
{
    public class Resource
    {
        public int Base = 0;
        public int PerLevel = 0;
        public int PerPop = 0;
        public int SystemPerPopMultiplier = 0;
        public int SystemPerPop = 0;
        public int SystemPerPopBonuis = 0;
        public int LocalBonus = 0;
        public int LocalPerPopBonus = 0;

        public void Load(DataBlock fromData)
        {
            if (fromData == null) return;
            Base = fromData.GetSubValueI("Base");
            PerLevel = fromData.GetSubValueI("PerLevel");
            PerPop = fromData.GetSubValueI("PerPop");
            SystemPerPopMultiplier = fromData.GetSubValueI("SystemPerPopMultiplier");
            SystemPerPop = fromData.GetSubValueI("SystemPerPop");
            SystemPerPopBonuis = fromData.GetSubValueI("SystemPerPopBonuis");
            LocalBonus = fromData.GetSubValueI("LocalBonus");
            LocalPerPopBonus = fromData.GetSubValueI("LocalPerPopBonus");
        }
    };
    //public DataBlock _Data;

    public Resource BC = new Resource();
    public Resource Research = new Resource();
    public Resource Influence = new Resource();
    public Resource Shipbuilding = new Resource();
    public Resource Growth = new Resource();

    public List<int> Tax_PerPop = new List<int>();
    public List<int> Tax_PerLevel = new List<int>();
    public int MaxPop = 0;
    public int MapPopBonus = 0;
    public int TerraformCost = 0;

    public DefBenefitWrapper(DataBlock targetData)
    {
        //_Data = targetData;
        DataBlock _Data = targetData;

        BC.Load(_Data.GetSub("BC"));
        Research.Load(_Data.GetSub("Research"));
        Influence.Load(_Data.GetSub("Influence"));
        Shipbuilding.Load(_Data.GetSub("Shipbuilding"));
        Growth.Load(_Data.GetSub("Growth"));

        Tax_PerPop.Clear();
        Tax_PerPop.Add(_Data.GetSubValueI("Extra", "Tax_1_PerPop"));
        Tax_PerPop.Add(_Data.GetSubValueI("Extra", "Tax_2_PerPop"));
        Tax_PerPop.Add(_Data.GetSubValueI("Extra", "Tax_3_PerPop"));
        Tax_PerLevel.Clear();
        Tax_PerLevel.Add(_Data.GetSubValueI("Extra", "Tax_1_PerLevel"));
        Tax_PerLevel.Add(_Data.GetSubValueI("Extra", "Tax_2_PerLevel"));
        Tax_PerLevel.Add(_Data.GetSubValueI("Extra", "Tax_3_PerLevel"));

        MaxPop = _Data.GetSubValueI("Extra", "MaxPop");
        MapPopBonus = _Data.GetSubValueI("Extra", "MapPopBonus");
        TerraformCost = _Data.GetSubValueI("Extra", "TerraformCost");
    }
}