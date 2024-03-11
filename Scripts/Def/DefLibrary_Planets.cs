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
        for (int idx = 0; idx < Buildings.Count; idx++)
        {
            if (Buildings[idx].ValueS == name)
            {
                return Buildings[idx];
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
        Data.SaveToFile(BuildingsList, "Defs_Mod/Planets_s.mod", this);
    }

    public void LoadPlanetsDefFunc()
    {
        PlanetsDefData = Data.LoadFile("Defs_Mod/Planets.mod", this);
        
        Array<DataBlock> mods = new Array<DataBlock>();
        mods = PlanetsDefData.GetSubs("Mod", true);

        Planets.Clear();
        Planets = PlanetsDefData.GetSubs("Planet", true);

        for (int modIdx = 0; modIdx < mods.Count; modIdx++)
        {
            for (int idx = 0; idx < Planets.Count; idx++)
            {
                Array<DataBlock> subs = Planets[idx].Subs;
                for ( int subIdx = 0; subIdx < subs.Count; subIdx++)
                {
                    if (subs[subIdx].Name.StartsWith("Mod:" + mods[modIdx].ValueS))
                    {
                        subs[subIdx].Name.Replace("Mod:", "");
                        subs[subIdx].Subs = mods[modIdx].Subs;
                    }
                }
            }
        }
    }
}
