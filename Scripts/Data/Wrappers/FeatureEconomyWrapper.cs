using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

public class FeatureEconomyWrapper
{
    public string Resource;
    public int ResourceIncome;
    public int ResourceLocalBonus;
    public int MaxPop;
    public int Happiness;

    DefFeatureWrapper _FeatureDef;

    public FeatureEconomyWrapper(DefFeatureWrapper featureDef)
    {
        _FeatureDef = featureDef;
    }

    public void Refresh()
    {
        Resource = _FeatureDef._Data.GetSubValueS("Resource");
        if (_FeatureDef._Data.HasSub("Benefit"))
        {
            ResourceIncome = _FeatureDef._Data.GetSubValueI("Benefit", "Income");
            ResourceLocalBonus = _FeatureDef._Data.GetSubValueI("Benefit", "LocalBonus");
            if (_FeatureDef._Data.HasSub("Benefit", "Extra"))
            {
                MaxPop = _FeatureDef._Data.GetSubValueI("Benefit", "Extra", "MaxPop");
                Happiness = _FeatureDef._Data.GetSubValueI("Benefit", "Extra", "Happiness");
            }
        }
        else
        {
            ResourceIncome = 0;
            ResourceLocalBonus = 0;
            MaxPop = 0;
            Happiness = 0;
        }
    }

    public string ToString_Short(int iconSize = 24)
    {
        string income = "";

        if (ResourceIncome != 0) income += Helper.ResValueToString(ResourceIncome) + Helper.GetIcon(Resource, iconSize);
        if (ResourceLocalBonus != 0) income += (income.Length > 1 ? "\n" : "") + Helper.ResValueToString(ResourceLocalBonus, 10, true) + Helper.GetIcon(Resource, iconSize) + " to all " + Helper.GetIcon(Resource, iconSize) + Helper.GetIcon("Building", iconSize);
        if (MaxPop > 0) income += (income.Length > 10 ? "\n" : "") + MaxPop.ToString() + Helper.GetIcon("Pops", iconSize) + "Max";
        else if (MaxPop < 0) income += (income.Length > 10 ? "\n" : "") + Helper.GetColorPrefix_Bad() + MaxPop.ToString() + Helper.GetColorSufix() + Helper.GetIcon("Pops", iconSize) + "Max";
        if (Happiness > 0) income += (income.Length > 1 ? "  " : "") + Happiness.ToString() + Helper.GetIcon("Happiness", iconSize);
        else if (Happiness < 0) income += (income.Length > 1 ? "  " : "") + Helper.GetColorPrefix_Bad() + Happiness.ToString() + Helper.GetColorSufix() + Helper.GetIcon("Happiness", iconSize);

        return income;
    }
}
