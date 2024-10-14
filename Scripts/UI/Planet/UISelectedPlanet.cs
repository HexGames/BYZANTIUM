using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISelectedPlanet : Control
{
    [ExportCategory("Links")]
    [Export]
    public RichTextLabel PlanetName;
    private static string PlanetName_Original = "";
    [Export]
    public Control Uncolonized;
    [Export]
    public Control Colonized;
    [Export]
    public Control ColonyInfo;
    [Export]
    public RichTextLabel Pops;
    [Export]
    private static string Pops_Original = "";
    [Export]
    public RichTextLabel Control;
    [Export]
    private static string Control_Original = "";
    [Export]
    public RichTextLabel Factory;
    [Export]
    private static string Factory_Original = "";
    [Export]
    public RichTextLabel Bases;
    [Export]
    private static string Bases_Original = "";
    [Export]
    public UISelectedPlanetItem Planet;
    [Export]
    public Array<UISelectedPlanetItem> Features = new Array<UISelectedPlanetItem>();
    [Export]
    public Array<UISelectedPlanetItem> Districts = new Array<UISelectedPlanetItem>();

    [Export]
    public Control BuildDistrictSelector = null;
    [Export]
    public Control QueueControl = null;
    [Export]
    public Button IncreasePriority = null;
    [Export]
    public Button DecreasePriority = null;
    [Export]
    public Button CancelDistrict = null;
    [Export]
    public RichTextLabel PositionInQueue;
    [Export]
    private static string PositionInQueue_Original = "";
    [Export]
    public Control PossibleDistrictsBg = null;
    [Export]
    public Control PossibleDistrictsScrollBg = null;
    [Export]
    public Array<UISelectedPlanetItem> PossibleDistricts = new Array<UISelectedPlanetItem>();

    [ExportCategory("Runtime")]
    [Export]
    public PlanetData _Planet = null;
    [Export]
    public ColonyData _Colony = null;
    [Export]
    public StarData _Star = null;
    [Export]
    public SystemData _System = null;

    public override void _Ready()
    {
        if (PlanetName_Original.Length == 0) PlanetName_Original = PlanetName.Text;
        if (PositionInQueue_Original.Length == 0) PositionInQueue_Original = PositionInQueue.Text;
    }

    public void Refresh(PlanetData planet)
    {
        _Planet = planet;
        _Colony = planet.Colony;
        _Star = _Planet._Star;
        _System = _Star.System;

        BuildDistrictSelector.Visible = false;

        PlanetName.Text = PlanetName_Original.Replace("$name", _Star.StarName);

        // common
        {
            Planet.Visible = true;
            Planet.RefreshPlanet(_Planet);
            Array<DataBlock> featuresData = _Planet.Data.GetSub("Features").GetSubs();
            while (Features.Count < featuresData.Count)
            {
                UISelectedPlanetItem newItem = Features[0].Duplicate(7) as UISelectedPlanetItem;
                Features[0].GetParent().AddChild(newItem);
                Features.Add(newItem);
            }
            for (int idx = 0; idx < Features.Count; idx++)
            {
                if (idx < featuresData.Count)
                {
                    Features[idx].Visible = true;
                    Features[idx].RefreshFeature(featuresData[idx]);
                }
                else
                {
                    Features[idx].Visible = false;
                }
            }
        }

        if (_Colony != null)
        {
            Uncolonized.Visible = false;
            Colonized.Visible = true;
            ColonyInfo.Visible = true;

            Array<DataBlock> districts = _Colony.Data.GetSub("Districts").GetSubs("District");

            while (Districts.Count < districts.Count)
            {
                UISelectedPlanetItem newItem = Districts[0].Duplicate(7) as UISelectedPlanetItem;
                Districts[0].GetParent().AddChild(newItem);
                Districts.Add(newItem);
            }
            for (int idx = 0; idx < Districts.Count; idx++)
            {
                if (idx < districts.Count)
                {
                    Districts[idx].Visible = true;
                    Districts[idx].RefreshDistrict(districts[idx]);
                }
                else
                {
                    Districts[idx].Visible = false;
                }
            }
        }
        else
        {
            Uncolonized.Visible = true;
            Colonized.Visible = false;
            ColonyInfo.Visible = false;

            for (int idx = 0; idx < Districts.Count; idx++)
            {
                if (idx == 0)
                {
                    Districts[idx].Visible = true;
                    Districts[idx].RefreshDistrict(_Planet);
                }
                else
                {
                    Districts[idx].Visible = false;
                }
            }
        }
    }

    public void ShowBuildDistrictSelector()
    {
        BuildDistrictSelector.Visible = true;
        QueueControl.Visible = false;
        PossibleDistrictsBg.Visible = true;

        for (int idx = 0; idx < PossibleDistricts.Count; idx++)
        {
            PossibleDistricts[idx].Visible = false;
        }
        //if (_District != null)
        //{
            if (_System != null)
            {
                // empty district on exiting colony
                int districtIdx = 0;
                for (int possibleIdx = 0; possibleIdx < _System.ActionsBuildPossible_PerTurn.Count; possibleIdx++)
                {
                    if (_System.ActionsBuildPossible_PerTurn[possibleIdx]._Planet == _Planet)
                    {
                        if (districtIdx >= PossibleDistricts.Count)
                        {
                            UISelectedPlanetItem newItem = PossibleDistricts[0].Duplicate(7) as UISelectedPlanetItem;
                            PossibleDistricts[0].GetParent().AddChild(newItem);
                            PossibleDistricts.Add(newItem);
                        }
                        PossibleDistricts[districtIdx].Visible = true;
                        PossibleDistricts[districtIdx].RefreshPossibleDistrict(_System.ActionsBuildPossible_PerTurn[possibleIdx]);
                        districtIdx++;
                    }
                }
                PossibleDistrictsScrollBg.CustomMinimumSize = new Vector2(320, Mathf.Min(92 * districtIdx, 512));
            }
        //}
    }
    public void HideBuildDistrictSelector()
    {
        BuildDistrictSelector.Visible = false;
    }
}