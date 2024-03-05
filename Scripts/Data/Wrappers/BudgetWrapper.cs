using Godot;
using Godot.Collections;
using System.Collections.Generic;

public class BudgetWrapper // not used
{
    public class Info
    {
        public int Locked = 0;
        public int Value = 0;
        public float Ratio = 0.0f;

        public bool IsTreasury = false;

        public string Name = "Res";
    };

    protected DataBlock _Data = null;
    public List<Info> Items = new List<Info>();

    public BudgetWrapper(DataBlock budget)
    {
        _Data = budget;

        Refresh();
    }

    public void Refresh()
    {
        Items.Clear();
        int totalValue = 0;
        Array<DataBlock> itemsData = _Data.GetSub("Production").GetSubs();
        for (int idx = 0; idx < itemsData.Count; idx++)
        {
            Info info = new Info();

            info.Locked = itemsData[idx].GetSub("Locked").ValueI;
            info.Value = itemsData[idx].GetSub("Value").ValueI;
            info.Name = itemsData[idx].ValueS;

            info.IsTreasury = itemsData[idx].Name == "Treasury";

            totalValue += info.Value;

            Items.Add(info);
        }

        for (int idx = 0; idx < Items.Count; idx++)
        {
            Items[idx].Ratio = 1.0f * totalValue / Items[idx].Value;
        }
    }

    public int GetProduction(string project, int totalProd)
    {
        if (Items[0].Name == project)
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
                if (Items[idx].Name == project)
                {
                    return Mathf.FloorToInt(Items[idx].Ratio * totalProd);
                }
            }
        }
        return 0;
    }
}
