using Godot;
using Godot.Collections;
using System.Collections.Generic;

public class JobsWrapper
{
    public class Info
    {
        public string Name = "Res";

        public int Pops = 0;
        public int FocusValue = 0;
        public int FocusChange = 0;
        public ResourcesWrapper Benefit = null;

        public ResourcesWrapper.Info GetMainRes() { return Benefit.Get(Name); }
    };

    private DataBlock _Data = null;
    public List<Info> Jobs = new List<Info>();
    public int AllFocusValue = 0;
    public int AllFocusChange = 0;

    private DefLibrary Def = null;

    public JobsWrapper(DataBlock jobData, DefLibrary def)
    {
        _Data = jobData;
        Def = def;

        //Refresh();
    }

    public void Refresh(int colonyPops)
    {
        Jobs.Clear();
        AllFocusChange = 0;

        Array<DataBlock> jobsFocus = _Data.GetSubs("Focus");
        for (int idxData = 0; idxData < jobsFocus.Count; idxData++)
        {
            DataBlock focusData = jobsFocus[idxData];
            if (focusData.ValueS != "All")
            {
                Info newJob = new Info();
                newJob.Name = focusData.ValueS;

                newJob.FocusValue = focusData.GetSub("Value").ValueI;
                if (focusData.GetSub("Change") != null) newJob.FocusChange = focusData.GetSub("Change").ValueI;

                DataBlock defData = Def.GetJob(newJob.Name);
                newJob.Benefit = new ResourcesWrapper(defData.GetSub("Benefit"));

                Jobs.Add(newJob);

                //newRes.Benefit.MultiplyAll(0.001f * newRes.Pops);
            }
            else
            {
                if (focusData.GetSub("Change") != null) AllFocusChange = focusData.GetSub("Change").ValueI;
                AllFocusValue = focusData.GetSub("Value").ValueI;

                Array<DataBlock> allFocus = focusData.GetSubs("Job");
                int focusValue = AllFocusValue / allFocus.Count;
                for (int allIdx = 0; allIdx < allFocus.Count; allIdx++)
                {
                    Info newJob = new Info();
                    newJob.Name = allFocus[allIdx].ValueS;

                    DataBlock defData = Def.GetJob(newJob.Name);
                    newJob.Benefit = new ResourcesWrapper(defData.GetSub("Benefit"));

                    Jobs.Add(newJob);
                }
            }
        }

        int totalFocusValue = _Data.GetSub("TotalFocus").ValueI;
        int totalPops = 0;
        for (int idx = Jobs.Count - 1; idx >= 0; idx--)
        {
            if (idx != 0)
            {
                Jobs[idx].Pops = colonyPops * Jobs[idx].FocusValue / totalFocusValue;
                totalPops += Jobs[idx].Pops;
            }
            else // idx == 0
            {
                Jobs[idx].Pops = colonyPops - totalPops;
            }

            Jobs[idx].Benefit.MultiplyAll(0.001f * Jobs[idx].Pops);
        }
    }

    public Info Get(string name)
    {
        for (int idx = 0; idx < Jobs.Count; idx++)
        {
            if (Jobs[idx].Name == name)
            {
                return Jobs[idx];
            }
        }
        return null;
    }
}
