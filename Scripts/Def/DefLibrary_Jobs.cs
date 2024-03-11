using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [ExportCategory("Def Jobs")]
    [Export]
    public DataBlock JobsList = null;
    [Export]
    public Array<DataBlock> Jobs = new Array<DataBlock>();

    public List<ActionTargetInfo> JobsInfo = new List<ActionTargetInfo>();

    public void _Ready_Jobs()
    {
        for (int idx = 0; idx < Jobs.Count; idx++)
        {
            JobsInfo.Add(new ActionTargetInfo(Jobs[idx]));
        }
    }

    public DataBlock GetJob(string name)
    {
        for (int idx = 0; idx < Jobs.Count; idx++)
        {
            if (Jobs[idx].ValueS == name)
            {
                return Jobs[idx];
            }
        }
        return null;
    }

    public ActionTargetInfo GetJobInfo(string name)
    {
        for (int idx = 0; idx < JobsInfo.Count; idx++)
        {
            if (JobsInfo[idx]._Data.ValueS == name)
            {
                return JobsInfo[idx];
            }
        }
        return null;
    }

    public void SaveJobsDef()
    {
        Data.SaveToFile(JobsList, "Defs_Mod/Jobs.mod", this);
    }

    public void LoadJobsDefFunc()
    {
        JobsList = Data.LoadFile("Defs_Mod/Jobs.mod", this);

        Jobs.Clear();
        Jobs = JobsList.GetSubs("Job");
    }
}
