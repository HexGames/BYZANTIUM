using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [ExportCategory("Def Buildings")]
    [Export]
    public DataBlock PlanetsDefData = null;
    [Export]
    public Array<DataBlock> Planets = new Array<DataBlock>();
    [Export]
    public Array<DataBlock> PlanetsCustom = new Array<DataBlock>();
    [Export]
    public Array<string> PlanetsNames = new Array<string>();

    //public List<ActionTargetInfo> BuildingsInfo = new List<ActionTargetInfo>();

    //public void _Ready_Buildings()
    //{
    //    for (int idx = 0; idx < Buildings.Count; idx++)
    //    {
    //        BuildingsInfo.Add(new ActionTargetInfo(Buildings[idx]));
    //    }
    //}

    public DataBlock GetPlanet(string name)
    {
        for (int idx = 0; idx < Planets.Count; idx++)
        {
            if (Planets[idx].ValueS == name)
            {
                return Planets[idx];
            }
        }
        return null;
    }

    public DataBlock GetPlanetCustom(string name)
    {
        for (int idx = 0; idx < PlanetsCustom.Count; idx++)
        {
            if (PlanetsCustom[idx].ValueS == name)
            {
                return PlanetsCustom[idx];
            }
        }
        return null;
    }

    //public ActionTargetInfo GetBuildingInfo(string name)
    //{
    //    for (int idx = 0; idx < BuildingsInfo.Count; idx++)
    //    {
    //        if (BuildingsInfo[idx]._Data.ValueS == name)
    //        {
    //            return BuildingsInfo[idx];
    //        }
    //    }
    //    return null;
    //}

    public void SavePlanetsDef()
    {
        Data.SaveToFile(PlanetsDefData, "Defs_Mod/Planets_s.mod", this);
    }

    public void LoadPlanetsDefFunc()
    {
        PlanetsDefData = Data.LoadFile("Defs_Mod/Planets.mod", this);
        
        Array<DataBlock> mods = new Array<DataBlock>();
        mods = PlanetsDefData.GetSubs("Mod", true);

        Planets.Clear();
        Planets = PlanetsDefData.GetSubs("Planet", true);

        PlanetsCustom.Clear();
        PlanetsCustom = PlanetsDefData.GetSubs("Custom");

        for (int modIdx = 0; modIdx < mods.Count; modIdx++)
        {
            for (int idx = 0; idx < Planets.Count; idx++)
            {
                DataBlock features = Planets[idx].GetSub("Features");
                if (features != null)
                {
                    Array<DataBlock> subs = features.Subs;
                    for (int subIdx = 0; subIdx < subs.Count; subIdx++)
                    {
                        if (subs[subIdx].Name.StartsWith("Mod:" + mods[modIdx].ValueS))
                        {
                            //Data.ChangeDataType(subs[subIdx], subs[subIdx].Name.Replace("Mod:", ""), this);
                            //subs[subIdx].Subs = mods[modIdx].Subs;
                            subs.AddRange(mods[modIdx].Subs);
                            subs.RemoveAt(subIdx);
                        }
                    }
                }
            }
        }

        SavePlanetsDef();
    }

    public void LoadPlanetNamesFunc()
    {
        PlanetsDefData = Data.LoadFile("Defs_Mod/PlanetsNames.mod", this);

        PlanetsNames.Clear();
        for ( int idx = 0; idx < PlanetsDefData.GetSubs().Count; idx++)
        {
            PlanetsNames.Add( PlanetsDefData.GetSubs()[idx].Name );
        }
    }
}
