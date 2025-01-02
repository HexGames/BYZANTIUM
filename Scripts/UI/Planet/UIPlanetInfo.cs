using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIPlanetInfo : Control
{
    [ExportCategory("Links")]
    [Export]
    private UIText TitleLabel = null;
    [Export]
    public UIPlanetInfoPlanet Planet;
    [Export]
    public Array<UIPlanetInfoFeature> Features = new Array<UIPlanetInfoFeature>();
    [Export]
    public Array<UIPlanetInfoDistrict> Districts = new Array<UIPlanetInfoDistrict>();

    [Export]
    public Control ChooseDistrictWindow;
    [Export]
    public UIText ChooseDistrictKeepText;
    [Export]
    public Array<UIPlanetInfoOtherDistrict> ChooseDistrictItem = new Array<UIPlanetInfoOtherDistrict>();

    [Export]
    public Control PopInfo;
    [Export]
    public UIText PopInfoSpecies;
    [Export]
    public UIText PopInfoEthics;
    [Export]
    public UIText PopInfoWealthName;
    [Export]
    public UIText PopInfoWealthValue;
    [Export]
    public UIText PopInfoHappinessName;
    [Export]
    public UIText PopInfoHappinessValue;

    [ExportCategory("Runtime")]
    [Export]
    public PlanetData _Planet = null;
    [Export]
    public UIPlanetInfoDistrict SelectedDistrict = null;
    //[Export]
    //public DataBlock _Layout = null;

    public void Refresh(PlanetData planet, string forceTitle = "")
    {
        _Planet = planet;

        string name = forceTitle;
        if (name == "") _Planet.Data.ValueToString();
        if (name == "") name = _Planet.Data.ValueS;

        TitleLabel.SetTextWithReplace("$name", name);

        Planet.Refresh(_Planet);

        // grow
        while (Features.Count < planet.Features.Count)
        {
            UIPlanetInfoFeature newItem = Features[0].Duplicate(7) as UIPlanetInfoFeature;
            Features[0].GetParent().AddChild(newItem);
            Features.Add(newItem);
        }

        for (int idx = 0; idx < Features.Count; idx++)
        {
            if (idx < planet.Features.Count && planet.Features[idx].Data.ToUIShow())
            {
                Features[idx].Refresh(planet.Features[idx]);
                Features[idx].Visible = true;
            }
            else
            {
                Features[idx].Visible = false;
            }
        }

        if (planet.Colony != null)
        {
            // grow
            while (Districts.Count < planet.Colony.Districts.Count)
            {
                UIPlanetInfoDistrict newItem = Districts[0].Duplicate(7) as UIPlanetInfoDistrict;
                Districts[0].GetParent().AddChild(newItem);
                Districts.Add(newItem);
            }

            for (int idx = 0; idx < Districts.Count; idx++)
            {
                if (idx < planet.Colony.Districts.Count)
                {
                    Districts[idx].RefreshDistrict(planet.Colony.Districts[idx]);
                    Districts[idx].Visible = true;
                }
                else
                {
                    Districts[idx].Visible = false;
                }
            }
        }
        else
        {
            for (int idx = 0; idx < Districts.Count; idx++)
            {
                Districts[idx].Visible = false;
            }
        }

        ChooseDistrictWindow.Visible = false;
        PopInfo.Visible = false;
    }

    public void OnSelectPlanet()
    {

    }

    public void OnSelectFeature(UIPlanetInfoFeature feature)
    {
        CloseChooseDistrictWindow();
        ClosePopInfoWindow();

        for (int idx = 0; idx < Districts.Count; idx++)
        {
            Districts[idx].CloseActions();
        }

        for (int idx = 0; idx < Features.Count; idx++)
        {
            if (Features[idx] == feature)
            {
                Features[idx].OpenActions();
            }
            else
            {
                Features[idx].CloseActions();
            }
        }
    }

    public void SelectDistrict(UIPlanetInfoDistrict district)
    {
        for (int idx = 0; idx < Features.Count; idx++)
        {
            Features[idx].CloseActions();
        }

        for (int idx = 0; idx < Districts.Count; idx++)
        {
            if (Districts[idx] == district)
            {
                //if (Districts[idx] != null && Districts[idx]._District._Data.GetSubValueI("Change_Cooldown") == 0 && Districts[idx]._District._Data.GetSubValueI("Control_Cooldown") == 0)
                //{
                //    if (Districts[idx]._District.Pop.GetProgress() < 1000) OpenChooseDistrictWindow(district, false);
                //    else OpenChooseDistrictWindow(district, district._District.DistrictDef.Control_Type != "Private");
                //}
                //else
                //{
                CloseChooseDistrictWindow();
                //}
                ClosePopInfoWindow();
                Districts[idx].OpenActions(true);

                SelectedDistrict = Districts[idx];
            }
            else
            {
                Districts[idx].CloseActions();
            }
        }
    }

    public void SelectPop(UIPlanetInfoDistrict district)
    {
        for (int idx = 0; idx < Features.Count; idx++)
        {
            Features[idx].CloseActions();
        }

        for (int idx = 0; idx < Districts.Count; idx++)
        {
            if (Districts[idx] == district)
            {
                CloseChooseDistrictWindow();
                OpenPopInfoWindow(district);
                Districts[idx].OpenActions(false);
            }
            else
            {
                Districts[idx].CloseActions();
            }
        }
    }

    public void OpenChooseDistrictWindowUpgrade(UIPlanetInfoDistrict district)
    {
        ChooseDistrictWindow.Visible = true;
        ActionDistrict.RefreshUpgradeDistricts(district._District);

        ChooseDistrictKeepText.SetTextWithReplace("$name", district._District.DistrictDef.Name);
        // grow
        GrowChooseDistrictList(district, district._District.ActionsChangeDistricts_OnRefresh);

    }
    public void OpenChooseDistrictWindowChangeControl(UIPlanetInfoDistrict district)
    {
        ChooseDistrictWindow.Visible = true;
        ActionDistrict.RefreshChangeControlDistricts(district._District);

        ChooseDistrictKeepText.SetTextWithReplace("$name", district._District.DistrictDef.Name);

        // grow
        GrowChooseDistrictList(district, district._District.ActionsChangeDistricts_OnRefresh);
    }

    public void OpenChooseDistrictWindowChangeType(UIPlanetInfoDistrict district)
    {
        ChooseDistrictWindow.Visible = true;
        ActionDistrict.RefreshChangeTypeDistricts(district._District);

        ChooseDistrictKeepText.SetTextWithReplace("$name", district._District.DistrictDef.Name);

        // grow
        GrowChooseDistrictList(district, district._District.ActionsChangeDistricts_OnRefresh);
    }

    private void GrowChooseDistrictList(UIPlanetInfoDistrict district, List<DistrictNew> newDistricts)
    {
        // grow
        while (ChooseDistrictItem.Count < newDistricts.Count)
        {
            UIPlanetInfoOtherDistrict newItem = ChooseDistrictItem[0].Duplicate(7) as UIPlanetInfoOtherDistrict;
            ChooseDistrictItem[0].GetParent().AddChild(newItem);
            ChooseDistrictItem.Add(newItem);
        }

        for (int idx = 0; idx < ChooseDistrictItem.Count; idx++)
        {
            if (idx < newDistricts.Count)
            {
                ChooseDistrictItem[idx].RefreshDistrict(district._District, newDistricts[idx]);
                ChooseDistrictItem[idx].Visible = true;
            }
            else
            {
                ChooseDistrictItem[idx].Visible = false;
            }
        }
    }


    public void CloseChooseDistrictWindow()
    {
        ChooseDistrictWindow.Visible = false;
        if (SelectedDistrict != null)
        {
            SelectedDistrict.RefreshDistrict(SelectedDistrict._District);
            SelectedDistrict.DeselectBtn();
        }
    }

    public void SelectChosenDistrict(UIPlanetInfoOtherDistrict chosenDistrict)
    {
        ActionDistrict.ChangeDistrict(SelectedDistrict._District, chosenDistrict._DistrictNew);

        SelectedDistrict.RefreshDistrict(SelectedDistrict._District);
        Game.self.GalaxyUI.Stockpiles.Refresh();

        CloseChooseDistrictWindow();
    }

    public void OpenPopInfoWindow(UIPlanetInfoDistrict district)
    {
        PopInfo.Visible = true;
    }

    public void ClosePopInfoWindow()
    {
        PopInfo.Visible = false;
    }
}