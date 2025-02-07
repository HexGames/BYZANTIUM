using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;

public class DefDistrictWrapper
{
    //public DataBlock _Data;

    public string Name = "";
    public string Type = "";
    public List<string> ChangeTo = new List<string>();
    public Color Color = new Color();
    public string Icon = "";
    public int Cost_BC = 0;
    public DefBenefitWrapper Benefit = null;
    public string Control = "";

    public DefDistrictWrapper(DataBlock targetData)
    {
        //_Data = targetData;
        DataBlock _Data = targetData;

        Name = _Data.ValueS;
        Type = _Data.GetSubValueS("Type");
        Array<DataBlock> changeToData = _Data.GetSubs("ChangeTo"); 
        if (_Data.GetSubValueS("Color") != "") Color = new Color(_Data.GetSubValueS("Color").Replace("c_", ""));
        Icon = _Data.GetSubValueS("Icon");
        Cost_BC = _Data.GetSubValueI("Cost", "BC");
        Benefit = new DefBenefitWrapper(_Data.GetSub("Benefit"));
        Control = _Data.GetSubValueS("Control");
    }
}