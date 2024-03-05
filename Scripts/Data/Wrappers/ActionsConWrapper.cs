using Godot;
using Godot.Collections;
using System.Collections.Generic;

public class ActionsConWrapper
{
    public class Info
    {
        public int Value = 0;
        public float Ratio = 0.0f;

        public string Name = "Res";
    };

    protected DataBlock _AConBuildings = null;
    protected DataBlock _AConColony = null;
    protected DataBlock _AConShipyard = null;
    protected DataBlock _AConTreasury = null;
    protected DataBlock _Colony = null;
    public List<Info> Items = new List<Info>();

    public ActionsConWrapper(DataBlock actionConBuildings, DataBlock actionConColony, DataBlock actionConShipyard, DataBlock actionConTreasury)
    {
        _AConBuildings = actionConBuildings;
        _AConColony = actionConColony;
        _AConShipyard = actionConShipyard;
        _AConTreasury = actionConTreasury;

        Refresh();
    }

    public void Refresh()
    {
        Items.Clear();
        int totalValue = 0;

        List<DataBlock> data = new List<DataBlock>
        {
            _AConBuildings,
            _AConColony,
            _AConShipyard,
            _AConTreasury
        };

        for ( int idx = 0; idx < data.Count; idx++)
        {
            Info info = new Info();
        
            info.Value = data[idx].GetSub("Priority").ValueI;
            info.Name = data[idx].Name;
        
            totalValue += info.Value;
        
            Items.Add(info);
        }

        // calculate ratio
        for (int idx = 0; idx < Items.Count; idx++)
        {
            Items[idx].Ratio = 1.0f * Items[idx].Value / totalValue;
        }
    }

    public int GetProduction(string actionCon, int totalProd)
    {
        if (Items[0].Name == actionCon)
        {
            int prod = totalProd;
            for (int idx = 1; idx < Items.Count; idx++)
            {
                prod -= Mathf.FloorToInt(Items[idx].Ratio * totalProd);
            }
            return prod;
        }
        else
        {
            for (int idx = 1; idx < Items.Count; idx++)
            {
                if (Items[idx].Name == actionCon)
                {
                    return Mathf.FloorToInt(Items[idx].Ratio * totalProd);
                }
            }
        }
        return 0;
    }
}
