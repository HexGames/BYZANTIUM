using Godot;

public class DefFeatureWrapper
{
    public DataBlock _Data;

    public PlanetData _Planet;
    public DataBlock _FeatureOld;

    public string Name = "";
    public string Type = "";
    public Color Color = new Color();
    public DefBenefitWrapper Benefit = null;

    //public FeatureEconomyWrapper Economy_PerSession = null;

    public DefFeatureWrapper(DataBlock targetData)
    {
        _Data = targetData;

        Name = _Data.ValueS;
        Type = _Data.GetSubValueS("Type");
        if (_Data.GetSubValueS("Color") != "") Color = new Color(_Data.GetSubValueS("Color").Replace("c_", ""));
        Benefit = new DefBenefitWrapper(_Data.GetSub("Benefit"));

        //Economy_PerSession = new FeatureEconomyWrapper(this);
        //Economy_PerSession.Refresh();
    }
}