using Godot;

public class DefEmpireWrapper
{
    public DataBlock _Data;

    public int FlagID;
    public Color ColorMain;
    public Color ColorBg;
    public string Specie;
    public string StartingStarName;
    public string StartingPlanetType;
    public int StartingTemperature;
    public string Ethic;

    public DefEmpireWrapper(DataBlock targetData)
    {
        _Data = targetData;

        FlagID = _Data.GetSub("FlagID").ValueI;
        ColorMain = new Color(_Data.GetSub("Color").ValueS);
        ColorBg = new Color(_Data.GetSub("ColorBg").ValueS);
        Specie = _Data.GetSub("Specie").ValueS;
        StartingStarName = _Data.GetSub("StartingStarName").ValueS;
        StartingPlanetType = _Data.GetSub("StartingPlanetType").ValueS;
        StartingTemperature = _Data.GetSub("StartingTemperature").ValueI;
        Ethic = _Data.GetSub("Ethic").ValueS;
    }
}