using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

// Editor
public partial class DefLibrary : Node
{
    [Export]
    public Array<DataBlock> Buildings = new Array<DataBlock>();

    public List<ActionTargetInfo> BuildingsInfo = new List<ActionTargetInfo>();

    public void _Ready_Buildings()
    {
        for (int idx = 0; idx < Buildings.Count; idx++)
        {
            BuildingsInfo.Add(new ActionTargetInfo(Buildings[idx]));
        }
    }

    public DataBlock GetBuilding(string name)
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

    public ActionTargetInfo GetBuildingInfo(string name)
    {
        for (int idx = 0; idx < BuildingsInfo.Count; idx++)
        {
            if (BuildingsInfo[idx]._Data.ValueS == name)
            {
                return BuildingsInfo[idx];
            }
        }
        return null;
    }

    public void GenerateBuidingsDefFunc()
    {
        Buildings.Clear();

        {
            DataBlock buiding = Data.CreateData("Building", "City", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 500, this);
            Data.AddData(cost, "Metal:Income", -10, this);

            Data.AddData(buiding, "Turns", 5, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "Max_Buildings", 1, this);


            Buildings.Add(buiding);
        }

        {
            DataBlock buiding = Data.CreateData("Building", "Private_Business", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 1000, this);

            Data.AddData(buiding, "Turns", 5, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "Buy/Sell:Metal", 1, this);
            Data.AddData(Benefit, "Buy/Sell:Energy", 1, this);

            Buildings.Add(buiding);
        }

        {
            DataBlock buiding = Data.CreateData("Building", "Power_Plants", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 300, this);
            Data.AddData(cost, "Metal:Income", -10, this);

            Data.AddData(buiding, "Turns", 5, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "Energy:Income", 2, this);

            Buildings.Add(buiding);
        }

        {
            DataBlock buiding = Data.CreateData("Building", "Mines", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 500, this);

            Data.AddData(buiding, "Turns", 5, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "Metal:Income", 2, this);

            Buildings.Add(buiding);
        }

        {
            DataBlock buiding = Data.CreateData("Building", "Goverment_Offices", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 500, this);
            Data.AddData(cost, "Metal:Income", -10, this);

            Data.AddData(buiding, "Turns", 5, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "Authority", 1, this);

            Buildings.Add(buiding);
        }

        {
            DataBlock buiding = Data.CreateData("Building", "Diplomatic_Offices", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 500, this);
            Data.AddData(cost, "Metal:Income", -10, this);

            Data.AddData(buiding, "Turns", 5, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "Influence", 1, this);

            Buildings.Add(buiding);
        }

        {
            DataBlock buiding = Data.CreateData("Building", "Research_Labs", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 500, this);
            Data.AddData(cost, "Metal:Income", -10, this);

            Data.AddData(buiding, "Turns", 5, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "TechPoints:Income", 1, this);

            Buildings.Add(buiding);
        }

        {
            DataBlock buiding = Data.CreateData("Building", "Cultural_Center", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 500, this);
            Data.AddData(cost, "Metal:Income", -10, this);

            Data.AddData(buiding, "Turns", 5, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "CivicPoints:Income", 1, this);

            Buildings.Add(buiding);
        }

        {
            DataBlock buiding = Data.CreateData("Building", "Hydroponics_Farms", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 500, this);
            Data.AddData(cost, "Metal:Income", -10, this);

            Data.AddData(buiding, "Turns", 5, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "Pop:Max", 20, this);

            Buildings.Add(buiding);
        }

        {
            DataBlock buiding = Data.CreateData("Building", "Nature_Biodomes", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 1000, this);
            Data.AddData(cost, "Metal:Income", -20, this);

            Data.AddData(buiding, "Turns", 5, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "Happiness", 1, this);

            Buildings.Add(buiding);
        }

        {
            DataBlock buiding = Data.CreateData("Building", "City_Biodomes", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 1000, this);
            Data.AddData(cost, "Metal:Income", -20, this);

            Data.AddData(buiding, "Turns", 5, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "Pop:Max", 10, this);

            Buildings.Add(buiding);
        }

        // Station
        {
            DataBlock buiding = Data.CreateData("Building", "Shipyard", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 500, this);
            Data.AddData(cost, "Metal:Income", -50, this);

            Data.AddData(buiding, "Turns", 7, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);

            Buildings.Add(buiding);
        }

        {
            DataBlock buiding = Data.CreateData("Building", "Living_Quarters", this);

            DataBlock cost = Data.AddData(buiding, "Cost", this);
            Data.AddData(cost, "Credits", 1000, this);
            Data.AddData(cost, "Metal:Income", -50, this);

            Data.AddData(buiding, "Turns", 7, this);

            DataBlock Benefit = Data.AddData(buiding, "Benefit", this);
            Data.AddData(Benefit, "Pop:Max", 1, this);

            Buildings.Add(buiding);
        }
    }
}
