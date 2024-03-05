using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [ExportCategory("Def Campaigns")]
    [Export]
    public DataBlock CampaignsList = null;
    [Export]
    public Array<DataBlock> Campaigns = new Array<DataBlock>();

    public List<ActionTargetInfo> CampaignsInfo = new List<ActionTargetInfo>();

    public void _Ready_Campaigns()
    {
        for (int idx = 0; idx < Campaigns.Count; idx++)
        {
            CampaignsInfo.Add(new ActionTargetInfo(Campaigns[idx]));
        }
    }

    public DataBlock GetCampaign(string name)
    {
        for (int idx = 0; idx < Campaigns.Count; idx++)
        {
            if (Campaigns[idx].ValueS == name)
            {
                return Campaigns[idx];
            }
        }
        return null;
    }

    public ActionTargetInfo GetCampaignInfo(string name)
    {
        for (int idx = 0; idx < CampaignsInfo.Count; idx++)
        {
            if (CampaignsInfo[idx]._Data.ValueS == name)
            {
                return CampaignsInfo[idx];
            }
        }
        return null;
    }

    public void SaveCampaignsDef()
    {
        Data.SaveToFile(BuildingsList, "Defs_Mod/Buildings.mod", this);
    }

    public void LoadCampaignsDefFunc()
    {
        CampaignsList = Data.LoadFile("Defs_Mod/Campaigns.mod", this);

        Campaigns.Clear();
        Campaigns = CampaignsList.GetSubs("Campaign");
    }
}
